using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using _Scripts.Firebase;
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
                    Debug.Log("Signed  oUT");
                    AuthenticationUIManager.Instance.ShowStartPanel();
                }
            }
            else
            {
                AuthenticationUIManager.Instance.ShowNoConnectionPanel();
            }
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

            await LoadSceneAsync(2);
            // await MenuLoading(2);
            StartScreenAddictionShieldAsync();
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
            Destroy(this);
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


        public LearningStyle FindLearningStyle()
        {
            return LearningStyle.Visual;
            //Todo find learning style logic should be here 
        }

        public async void StartScreenAddictionShieldAsync()
        {
            while (player.lastActiveDatePlaytimeInSeconds <= 3600)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                player.lastActiveDatePlaytimeInSeconds++;
            }

            Debug.Log("Time REACHED"); //todo add a block panel
            Application.Quit();
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
            
            // loads the fist question in the level
            currentQuestionIndex = 0;
            LoadScene(currentLevelQuestions[currentQuestionIndex]);
            
            currentQuestionIndex++;
        }

        public void PlayNextQuestion()
        {
            if (currentQuestionIndex < currentLevelQuestions.Length)
            {
                LoadScene(currentLevelQuestions[currentQuestionIndex]);
            }
            else
            {
                //means that player has successfully passed the level
                Debug.Log("U HAVE COMPLETED THE LEVeEL");
                // currentLevelProgress = null;
                // currentLevelQuestions = null;
                return;
            }
            currentQuestionIndex++;
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