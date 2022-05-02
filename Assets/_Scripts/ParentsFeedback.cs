using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class ParentsFeedback : MonoBehaviour
    {
        [SerializeField] private Button[] levelSelections;

        [SerializeField] private TMP_Text learningStyle;
        [SerializeField] private TMP_Text noOfAttempts;
        [SerializeField] private Button levelPassed;

        [SerializeField] private TMP_Text levelInfo;
        [SerializeField] private TMP_Text correctAnsCount;
        [SerializeField] private TMP_Text wrongAnsCount;
        [SerializeField] private TMP_Text levelProgress;

        void Start()
        {
            for (int i = 0; i < GameManager.Instance.Player.LevelStats.Length; i++)
            {
                if (GameManager.Instance.Player.LevelStats[i].NoOfAttempts > 0)
                {
                    levelSelections[i].interactable = true;
                }
            }

            learningStyle.text = GameManager.Instance.Player.LearningStyle.ToString();
            int lvlToLoadOnStart = GameManager.Instance.Player.Level - 1; // loads info for last unlocked level
            if (GameManager.Instance.Player.Level - 1 == 0)
                lvlToLoadOnStart = 1;
            ShowInfoForALevel(lvlToLoadOnStart);
        }

        public void ShowInfoForALevel(int level)
        {
            if (GameManager.Instance.Player.LevelStats[level - 1].NoOfAttempts > 0)
            {
                //todo change according to the level title and assign relevant index values in build settings
                levelInfo.text = level switch
                {
                    1 => "Level " + level + " Counting",
                    2 => "Level " + level + " Addition",
                    3 => "Level " + level + " Subtraction",
                    4 => "Level " + level + " Comparision",
                    5 => "Level " + level + " Counting",
                    _ => levelInfo.text
                };
                Level currentLevel = GameManager.Instance.Player.LevelStats[level - 1];
                noOfAttempts.text = currentLevel.NoOfAttempts.ToString();
                levelPassed.interactable = currentLevel.IsPassed;
                correctAnsCount.text = currentLevel.LastAttemptProgress.NoOfCorrectAnswers.ToString();
                wrongAnsCount.text = currentLevel.LastAttemptProgress.NoOfWrongAnswers.ToString();
                levelProgress.text = currentLevel.LastAttemptProgress.CalculateLevelProgress() + " %";
            }
        }

        public void BackToLobby()
        {
            GameManager.Instance.LoadScene(1);
        }
    }
}