using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using _Scripts.Firebase;
using _Scripts.Utils;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    public class GameManager : Singleton<GameManager>
    {
        public Player player;
        public Store store;
        public LevelManager levelManager;
        private FirebaseManager firebaseManager;
        public bool isSignedIn;

        public int currentLevel;
        public LevelProgress currentLevelProgress;
        public int[] currentLevelQuestions;
        public int currentQuestionIndex;

        [SerializeField] private GameObject screenGuardWarningCanvas;

        protected override async void Awake()
        {
            base.Awake();
            if (CheckForInternetConnection())
            {
                firebaseManager = await FirebaseManager.Start();
                isSignedIn = await firebaseManager.IsSignedInAsync();
                if (isSignedIn)
                {
                    Debug.Log("Signed In");
                    InitGame();
                }
                else
                {
                    Debug.Log("Signed Out");
                    AuthenticationUIManager.Instance.ShowStartPanel();
                }
            }
            else
            {
                AuthenticationUIManager.Instance.ShowNoConnectionPanel();
            }
        }

        private async void InitGame()
        {
            store = new Store();
            levelManager = new LevelManager();
            player = await firebaseManager.RetrieveUserDataAsync();
            if (player.learningStyle == 0)
            {
                player.learningStyle = FindLearningStyle();
            }

            // resets daily stats for a new day
            if (DateTime.Compare(DateTime.Now.Date, player.lastActiveDate) > 0)
            {
                player.lastActiveDate = DateTime.Now.Date;
                player.lastActiveDatePlaytimeInSeconds = 0.0f;
                player.isDailyRewardCollected = false;
            }

            StartScreenAddictionShieldAsync(); //todo fix when max === now
            await LoadSceneAsync(2);
            // await MenuLoading(2);
        }

        
        //checks if the internet connectivity is available or not
        //https://stackoverflow.com/questions/2031824/what-is-the-best-way-to-check-for-internet-connectivity-using-net
        private static bool CheckForInternetConnection(int timeoutMs = 10000, string url = null)
        {
            try
            {
                url ??= CultureInfo.InstalledUICulture switch
                {
                    {Name: var n} when n.StartsWith("fa") => // Iran
                        "https://www.aparat.com",
                    {Name: var n} when n.StartsWith("zh") => // China
                        "https://www.baidu.com",
                    _ =>
                        "https://www.gstatic.com/generate_204",
                };

                var request = (HttpWebRequest) WebRequest.Create(url);
                request.KeepAlive = false;
                request.Timeout = timeoutMs;
                using var response = (HttpWebResponse) request.GetResponse();
                return true;
            }
            catch
            {
                return false;
            }
        }



        public async Task<string> SignInUser(string email, string password)
        {
            try
            {
                isSignedIn = await firebaseManager.SignInUserAsync(email, password);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            if (isSignedIn)
            {
                InitGame();
            }

            return null;
        }

        // saves playerData automatically if the user pause, minimize, quit the application
        private void OnApplicationPause(bool pauseStatus)
        {
            if (isSignedIn)
            {
                firebaseManager.UploadPlayerData(player);
            }
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus && isSignedIn)
                firebaseManager.UploadPlayerData(player);
        }

        private void OnApplicationQuit()
        {
            if (isSignedIn)
            {
                firebaseManager.UploadPlayerData(player);
            }
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void RestartGame()
        {
            Destroy(gameObject);
            LoadScene(0);
        }

        public async Task<string> SignUpUser(string email, string password, string studentName, int studentAge)
        {
            try
            {
                isSignedIn = await firebaseManager.SignUpUserAsync(email, password, studentName);

                store = new Store();
                levelManager = new LevelManager();

                player = new Player(studentName, studentAge);
                player.inventory.AddItem(store.Items["Costumes"][0]);
                player.inventory.AddItem(store.Items["SpaceShips"][0]);
                player.inventory.AddItem(store.Items["Equipments"][0]);

                firebaseManager.UploadPlayerData(player);
                InitGame();
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return null;
        }


        private LearningStyle FindLearningStyle()
        {
            return LearningStyle.Visual;
            //Todo find learning style logic should be here 
        }

        private async void StartScreenAddictionShieldAsync()
        {
            while (player.lastActiveDatePlaytimeInSeconds <= 3600)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                player.lastActiveDatePlaytimeInSeconds++;
            }

            Debug.Log("Daily Time REACHED");
            Instantiate(screenGuardWarningCanvas);
            await Task.Delay(3000);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                     Application.Quit();
#endif
        }

        public void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        public Task LoadSceneAsync(int index)
        {
            SceneManager.LoadSceneAsync(index);
            return Task.CompletedTask;
        }

        // public async Task MenuLoading(int i)
        // {
        //     var t= SceneManager.LoadSceneAsync(i);
        //     while (!t.isDone)
        //     {
        //         float p = Mathf.Clamp01(t.progress / .9f);
        //         loadingStatus.SetActive(true);
        //         loadingStatus.GetComponent<Image>().fillAmount = p;
        //         await Task.Yield();
        //     }            
        // }

        public void SignOut()
        {
            firebaseManager.UploadPlayerData(player);
            FirebaseAuth.DefaultInstance.SignOut();
            isSignedIn = false;
        }

        public void PlayLevel(int level)
        {
            currentLevel = level;
            currentLevelQuestions = levelManager.GetQuestionsForALevel(level, player.learningStyle);
            currentLevelProgress = new LevelProgress(currentLevelQuestions.Length);
            currentQuestionIndex = 0;
            // loads the BG story - adventure
            LoadScene(13);
        }

        public void PlayQuestion(int index)
        {
            if (index < currentLevelQuestions.Length)
            {
                LoadScene(currentLevelQuestions[index]);
                currentQuestionIndex = index;
            }
            else
            {
                //means that player has successfully passed the level
                Debug.Log("U HAVE COMPLETED THE LEVeEL");
            }
        }

        public void SaveLastAttemptInCurrentLvl(bool hasPassed)
        {
            // checks if the player has already played the level
            if (player.levelStats[currentLevel] != null)
            {
                // checks if the player has already passed the current level
                // means that the attempt is a retry after completing the level
                if (player.levelStats[currentLevel].isPassed)
                {
                    player.levelStats[currentLevel].lastAttemptProgress = currentLevelProgress;
                    player.levelStats[currentLevel].lastAttemptProgress.noOfAttempts ++;
                }
                else
                {
                    if (hasPassed)
                    {
                        player.level++;
                        player.levelStats[currentLevel].isPassed = true;
                        player.levelStats[currentLevel].lastAttemptProgress = currentLevelProgress;
                    }
                    else
                    {
                        player.levelStats[currentLevel].isPassed = false;
                        player.levelStats[currentLevel].lastAttemptProgress = currentLevelProgress;
                    }
                }
            }
            // save results of the first time level attempt
            else
            {
                player.levelStats[currentLevel] = new Level(currentLevelProgress, hasPassed);
                if (hasPassed)
                {
                    player.level++;
                }
            }
        }

        public void CollectDailyReward()
        {
            if (!player.isDailyRewardCollected)
            {
                player.CollectDailyReward(10);
            }
        }
    }
}