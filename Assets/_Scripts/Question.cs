using System;
using _Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Question : MonoBehaviour
{
    [Header("Question Base")] [SerializeField]
    private InGameUIManager inGameUIManager;

    private void Awake()
    {
        inGameUIManager = GameObject.Find("InGameUI").GetComponent<InGameUIManager>();
    }

    protected virtual void SetAnswerCorrect()
    {
        inGameUIManager.IsQuestionCorrect(true);
        GameManager.Instance.currentLevelProgress.correctAnswersCount++;
    }

    protected virtual void SetAnswerWrong()
    {
        inGameUIManager.IsQuestionCorrect(false);
        GameManager.Instance.currentLevelProgress.noOfWrongAnswers++;
    }
}