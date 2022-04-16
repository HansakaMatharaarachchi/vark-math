using System;
using _Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Question : MonoBehaviour
{

    [Header("Question Base")]
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TMP_Text questionResultText;
    private GameObject hUD;

    private void Awake()
    {
        hUD = GameObject.Find("HUD");
    }

    private void ShowResultPanel()
    {
        resultPanel.SetActive(true);
    }
    
    protected virtual void SetAnswerCorrect()
    {
        // GameManager.Instance.currentLevelProgress.correctAnswersCount++;
        questionResultText.text = "YAY!";
        ShowResultPanel();
        // GameManager.Instance.PlayNextQuestion();
    }
    
    protected virtual void SetAnswerWrong()
    {
        // GameManager.Instance.currentLevelProgress.noOfWrongAnswers++;
        questionResultText.text = "OOPS!";
        ShowResultPanel();
    }

    protected virtual void RetryQuestion()
    {
        // reloads the current scene (Question)
        // GameManager.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}