using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Scripts;
using TMPro;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject hUd;
    [SerializeField] private GameObject lvlResultPanel;
    [SerializeField] private GameObject questionResultPanel;
    
    [Header("Level Result Panel")]
    [SerializeField] private GameObject lvlPassedPanel;
    [SerializeField] private TMP_Text passedLevelInfoText;
    [SerializeField] private GameObject rewardContainer;
    [SerializeField] private GameObject unlockedItemsContainer;


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

    public void IsQuestionCorrect(bool state)
    {
        questionResultPanel.SetActive(true);
        if (state)
        {
            questionResultPanel.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            questionResultPanel.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void RetryCurrentQuestion()
    {
        if (GameManager.Instance.currentLevelProgress.noOfWrongAnswers == 3)
        {
            ShowResultPanel(false);
        }
        else
        {
            GameManager.Instance.PlayQuestion(GameManager.Instance.currentQuestionIndex);
        }
    }

    public void PlayNextQuestion()
    {
        GameManager.Instance.PlayQuestion(GameManager.Instance.currentQuestionIndex + 1);
    }
    
    private async void ShowRewards()
    {
        passedLevelInfoText.text = "You have successfully completed <br> Level " + 2;
        await Task.Delay(3000);
        rewardContainer.SetActive(false);
        unlockedItemsContainer.SetActive(true);
    }

    public void OpenStoreOnClick()
    {
        GameManager.Instance.LoadScene(4);
    }

    public void PlayNextLvlOnClick()
    {
        GameManager.Instance.PlayLevel(GameManager.Instance.currentLevel + 1);
    }

    public void RetryLevelOnClick()
    {
        GameManager.Instance.PlayLevel(GameManager.Instance.currentLevel);
    }

    public void LoadLobbyOnClick()
    {
        GameManager.Instance.LoadScene(3);
    }
}