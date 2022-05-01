using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace _Scripts
{
    public class InGameUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject hUd;
        [SerializeField] private GameObject lvlResultPanel;
        [SerializeField] private GameObject questionResultPanel;
        [SerializeField] private TMP_Text questionCorrectResultTxt;
        [SerializeField] private GameObject nxtQBtnContainer;

        [Header("Level Result Panel")] [SerializeField]
        private GameObject lvlPassedPanel;

        [SerializeField] private TMP_Text passedLevelInfoText;
        [SerializeField] private GameObject rewardContainer;
        [SerializeField] private GameObject unlockedItemsContainer;
        [SerializeField] private GameObject unlockedItemsCatPreview;


        private void Start()
        {
            hUd.SetActive(true);
        }

        private void ShowResultPanel(bool isLvlPassed)
        {
            questionResultPanel.SetActive(false);
            lvlResultPanel.SetActive(true);
            if (isLvlPassed)
            {
                lvlPassedPanel.SetActive(true);
                ShowRewards();
            }
            else
            {
                lvlResultPanel.transform.GetChild(1).gameObject.SetActive(true);
            }
        }

        public async void IsQuestionCorrect(bool state)
        {
            questionResultPanel.SetActive(true);
            if (state)
            {
                if (GameManager.Instance.CurrentLevelProgress.NoOfCorrectAnswers ==
                    GameManager.Instance.CurrentLevelQuestions.Length)
                {
                    questionCorrectResultTxt.text = "All fuel tanks are collected";
                    nxtQBtnContainer.SetActive(false);
                    questionResultPanel.transform.GetChild(0).gameObject.SetActive(true);
                    await Task.Delay(3000);
                    ShowResultPanel(true);
                    return;
                }

                questionResultPanel.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                if (GameManager.Instance.CurrentLevelProgress.NoOfWrongAnswers ==
                    GameManager.Instance.CurrentLevelQuestions.Length)
                {
                    ShowResultPanel(false);
                    return;
                }

                questionResultPanel.transform.GetChild(1).gameObject.SetActive(true);
            }
        }

        public void RetryCurrentQuestion()
        {
            GameManager.Instance.PlayQuestion(GameManager.Instance.CurrentQuestionIndex);
        }

        public void PlayNextQuestion()
        {
            GameManager.Instance.PlayQuestion(GameManager.Instance.CurrentQuestionIndex + 1);
        }

        private async void ShowRewards()
        {
            passedLevelInfoText.text =
                "You have successfully completed <br> Level " + GameManager.Instance.CurrentLevel;
            await Task.Delay(3000);
            rewardContainer.SetActive(false);
            unlockedItemsContainer.SetActive(true);
            foreach (StoreItemObject[] itemObjects in GameManager.Instance.Store.Items.Values)
            {
                foreach (StoreItemObject itemObject in itemObjects)
                {
                    if (itemObject.levelToBeUnlocked == GameManager.Instance.CurrentLevel + 1)
                    {
                        switch (itemObject.type)
                        {
                            case StoreItemType.Costume:
                                unlockedItemsCatPreview.transform.GetChild(0).gameObject.SetActive(true);
                                break;
                            case StoreItemType.SpaceShip:
                                unlockedItemsCatPreview.transform.GetChild(1).gameObject.SetActive(true);
                                break;
                            case StoreItemType.Equipment:
                                unlockedItemsCatPreview.transform.GetChild(2).gameObject.SetActive(true);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        break;
                    }
                }
            }
        }

        public void OpenStoreOnClick()
        {
            GameManager.Instance.LoadScene(2);
        }

        public void PlayNextLvlOnClick()
        {
            GameManager.Instance.PlayLevel(GameManager.Instance.CurrentLevel + 1);
        }

        public void RetryLevelOnClick()
        {
            GameManager.Instance.PlayLevel(GameManager.Instance.CurrentLevel);
        }

        public void LoadLobbyOnClick()
        {
            GameManager.Instance.LoadScene(1);
        }

        public void OpenSettings()
        {
            GameManager.Instance.OpenSettings();
        }
    }
}