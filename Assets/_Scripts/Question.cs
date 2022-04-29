using _Scripts;
using UnityEngine;

public abstract class Question : MonoBehaviour
{
    [Header("Question Base")] private InGameUIManager inGameUIManager;

    private void Awake()
    {
        inGameUIManager = GameObject.Find("InGameUI").GetComponent<InGameUIManager>();
    }

    protected virtual void SetAnswerCorrect()
    {
        GameManager.Instance.CurrentLevelProgress.NoOfCorrectAnswers++;
        if (GameManager.Instance.CurrentLevelProgress.NoOfCorrectAnswers ==
            GameManager.Instance.CurrentLevelQuestions.Length)
        {
            GameManager.Instance.SaveLastAttemptInCurrentLvl(true);
        }

        inGameUIManager.IsQuestionCorrect(true);
    }

    protected virtual void SetAnswerWrong()
    {
        GameManager.Instance.CurrentLevelProgress.NoOfWrongAnswers++;
        if (GameManager.Instance.CurrentLevelProgress.NoOfWrongAnswers ==
            GameManager.Instance.CurrentLevelQuestions.Length)
        {
            GameManager.Instance.SaveLastAttemptInCurrentLvl(false);
        }

        inGameUIManager.IsQuestionCorrect(false);
    }
}