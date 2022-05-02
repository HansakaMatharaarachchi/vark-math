using System.Collections.Generic;
using System.Threading.Tasks;
using _Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct VIQuestion
{
    public string question;
    public List<string> options;
    public int correctAnswer;
}

public class VIQuizManager : MonoBehaviour
{
    [SerializeField] private List<VIQuestion> parentsQuestionnaire = new List<VIQuestion>();
    [SerializeField] private GameObject envContainer;
    [SerializeField] private GameObject questionPanel;
    [SerializeField] private GameObject endPanel;

    [SerializeField] private TMP_Text questionTxt;
    [SerializeField] private Button[] choices;

    private int selectedQuestionIndex;
    private int correctAnswersCount;

    public void StartQuiz()
    {
        PlayVisualQuestion();
    }

    private async void PlayVisualQuestion()
    {
        envContainer.SetActive(true);
        await Task.Delay(10000);
        envContainer.SetActive(false);
        questionPanel.SetActive(true);
        DisplayQuestion();
        foreach (Button button in choices)
        {
            button.onClick.AddListener(delegate { CheckAnswer(button); });
        }
    }

    private void DisplayQuestion()
    {
        if (parentsQuestionnaire.Count > selectedQuestionIndex)
        {
            questionTxt.text = parentsQuestionnaire[selectedQuestionIndex].question;
            for (int i = 0; i < choices.Length; i++)
            {
                choices[i].GetComponentInChildren<TMP_Text>().text =
                    parentsQuestionnaire[selectedQuestionIndex].options[i];
            }
        }
        else
        {
            questionPanel.SetActive(false);
            PlayerPrefs.SetFloat("CV", (float) correctAnswersCount / parentsQuestionnaire.Count);
            PlayerPrefs.Save();
            endPanel.SetActive(true);
            Debug.Log(correctAnswersCount);
        }
    }

    void CheckAnswer(Button btn)
    {
        if (btn.GetComponentInChildren<TMP_Text>().text == parentsQuestionnaire[selectedQuestionIndex]
                .options[parentsQuestionnaire[selectedQuestionIndex].correctAnswer - 1])
        {
            ++correctAnswersCount;
        }

        ++selectedQuestionIndex;
        DisplayQuestion();
    }

    public void LoadAuditoryQuestions()
    {
        GameManager.Instance.LoadNextScene();
    }
}