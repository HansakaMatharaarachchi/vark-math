using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
        public Player Player { get; private set; }
        public Store Store { get; private set; }
        private LevelManager levelManager;
        private FirebaseManager firebaseManager;
        public AudioManager AudioManager { get; private set; }
        private NotificationManager notificationManager;
        public bool IsSignedIn { get; private set; }
        public int CurrentLevel { get; private set; }
        public LevelProgress CurrentLevelProgress { get; private set; }
        public int[] CurrentLevelQuestions { get; private set; }
        public int CurrentQuestionIndex { get; private set; }

        [SerializeField] private GameObject screenGuardWarningCanvas;
        [SerializeField] private GameObject settingsCanvas;


        protected override async void Awake()
        {
            base.Awake();
            if (CheckForInternetConnection())
            {
                firebaseManager = await FirebaseManager.Start();
                notificationManager = new NotificationManager();
                Store = new Store();
                levelManager = new LevelManager();
                AudioManager = new AudioManager();
                IsSignedIn = await firebaseManager.IsSignedInAsync();
                if (IsSignedIn)
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

        public async void InitGame()
        {
            Player = await firebaseManager.RetrieveUserDataAsync();
            if (Player.LearningStyle != LearningStyle.NotSet)
            {
                // resets daily stats for a new day
                if (DateTime.Compare(DateTime.Now.Date, Player.LastActiveDate) > 0)
                {
                    Player.LastActiveDate = DateTime.Now.Date;
                    Player.LastActiveDatePlaytimeInSeconds = 0.0f;
                    Player.IsDailyRewardCollected = false;
                }

                StartScreenAddictionShieldAsync();
                await LoadSceneAsync(1);
            }
            else
            {
                //delete saved values for learning style detection
                PlayerPrefs.DeleteKey("PV");
                PlayerPrefs.DeleteKey("PA");
                PlayerPrefs.DeleteKey("PK");
                
                PlayerPrefs.DeleteKey("CV");
                PlayerPrefs.DeleteKey("CA");
                PlayerPrefs.DeleteKey("CK");

                // load learning style detecting scenes
                await LoadSceneAsync(6);
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

        public async Task<string> SignInUser(string email, string password)
        {
            try
            {
                IsSignedIn = await firebaseManager.SignInUserAsync(email, password);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            if (IsSignedIn)
            {
                InitGame();
            }

            return null;
        }

        // saves playerData automatically if the user pause, minimize, quit the application
        private void OnApplicationPause(bool pauseStatus)
        {
            if (IsSignedIn)
            {
                firebaseManager.UploadPlayerData(Player);
            }
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus && IsSignedIn)
                firebaseManager.UploadPlayerData(Player);
        }

        private void OnApplicationQuit()
        {
            if (IsSignedIn)
            {
                firebaseManager.UploadPlayerData(Player);
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
                IsSignedIn = await firebaseManager.SignUpUserAsync(email, password, studentName);

                Store = new Store();
                levelManager = new LevelManager();

                Player = new Player(studentName, studentAge);

                //add default items 
                Player.Inventory.AddItem(Store.Items["Costumes"][0]);
                Player.Inventory.AddItem(Store.Items["SpaceShips"][0]);
                Player.Inventory.AddItem(Store.Items["Equipments"][0]);

                firebaseManager.UploadPlayerData(Player);
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
            var results =
                new Dictionary<LearningStyle, float>
                {
                    [LearningStyle.Visual] = (PlayerPrefs.GetFloat("PV") + PlayerPrefs.GetFloat("CV")) / 2,
                    [LearningStyle.Auditory] = (PlayerPrefs.GetFloat("PA") + PlayerPrefs.GetFloat("CA")) / 2,
                    [LearningStyle.Kinesthetic] = (PlayerPrefs.GetFloat("PK") + PlayerPrefs.GetFloat("CK")) / 2
                };
            KeyValuePair<LearningStyle, float> maxValue = results.First();

            // finds which result has the best score
            foreach (KeyValuePair<LearningStyle, float> result in results)
            {
                if (result.Value > maxValue.Value) maxValue = result;
            }

            Player.LearningStyle = maxValue.Key;
            firebaseManager.UploadPlayerData(Player);
            return maxValue.Key;
        }

        private async void StartScreenAddictionShieldAsync()
        {
            while (Player.LastActiveDatePlaytimeInSeconds <= 3600)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                Player.LastActiveDatePlaytimeInSeconds++;
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

        public void LoadNextScene()
        {
            LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public Task LoadSceneAsync(int index)
        {
            SceneManager.LoadSceneAsync(index);
            return Task.CompletedTask;
        }

        public void SignOut()
        {
            firebaseManager.UploadPlayerData(Player);
            FirebaseAuth.DefaultInstance.SignOut();
            IsSignedIn = false;
        }

        public void PlayLevel(int level)
        {
            CurrentLevel = level;
            CurrentLevelQuestions = levelManager.GetQuestionsForALevel(level, Player.LearningStyle);
            CurrentLevelProgress = new LevelProgress(CurrentLevelQuestions.Length);
            CurrentQuestionIndex = 0;
            // loads the BG story - adventure
            LoadScene(5);
        }

        public void PlayQuestion(int index)
        {
            if (index < CurrentLevelQuestions.Length)
            {
                LoadScene(CurrentLevelQuestions[index]);
                CurrentQuestionIndex = index;
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
            if (Player.LevelStats[CurrentLevel - 1].NoOfAttempts > 0)
            {
                // checks if the player has already passed the current level
                // means that the attempt is a retry after completing the level
                if (Player.LevelStats[CurrentLevel - 1].IsPassed)
                {
                    Player.LevelStats[CurrentLevel - 1].LastAttemptProgress = CurrentLevelProgress;
                }
                else
                {
                    if (hasPassed)
                    {
                        Player.Level++;
                        Player.LevelStats[CurrentLevel - 1].IsPassed = true;
                        Player.LevelStats[CurrentLevel - 1].LastAttemptProgress = CurrentLevelProgress;
                    }
                    else
                    {
                        Player.LevelStats[CurrentLevel - 1].IsPassed = false;
                        Player.LevelStats[CurrentLevel - 1].LastAttemptProgress = CurrentLevelProgress;
                    }
                }

                Player.LevelStats[CurrentLevel - 1].NoOfAttempts++;
            }
            // save results of the first time level attempt
            else
            {
                Player.LevelStats[CurrentLevel - 1] = new Level(CurrentLevelProgress, hasPassed);
                Player.LevelStats[CurrentLevel - 1].NoOfAttempts++;
                if (hasPassed)
                {
                    Player.Level++;
                    Player.GoldCoinAmount += 20;
                }
            }
        }

        public void CollectDailyReward()
        {
            if (!Player.IsDailyRewardCollected)
            {
                Player.CollectDailyReward(10);
            }
        }

        public void OpenSettings()
        {
            Instantiate(settingsCanvas);
        }
    }
}