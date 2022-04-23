using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct AIQuestion
{
    public string question;
    public List<string> options;
    public int correctAnswer;
}

public class AIQuizManager : MonoBehaviour
{
    [SerializeField] private List<AIQuestion> parentsQuestionnaire = new List<AIQuestion>();
    [SerializeField] private GameObject envContainer;
    [SerializeField] private GameObject questionPanel;
    [SerializeField] private GameObject endPanel;

    [SerializeField] private TMP_Text questionTxt;
    [SerializeField] private Button[] choices;

    [SerializeField] private AudioSource audioSource;

    private int selectedQuestionIndex;
    private int correctAnswersCount;

    public void StartQuiz()
    {
        PlayAuditoryQuestion();
    }

    private async void PlayAuditoryQuestion()
    {
        envContainer.SetActive(true);
        while (audioSource.isPlaying)
        {
            await Task.Delay(1000);
        }

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
            endPanel.SetActive(true);
            Debug.Log(correctAnswersCount + " " + selectedQuestionIndex);
        }
    }

    void CheckAnswer(Button btn)
    {
        if (btn.GetComponentInChildren<TMP_Text>().text == parentsQuestionnaire[selectedQuestionIndex]
                .options[parentsQuestionnaire[selectedQuestionIndex].correctAnswer - 1])
        {
            ++correctAnswersCount;
        }
        else
        {
            Debug.Log("YA STUPID");
        }

        ++selectedQuestionIndex;
        DisplayQuestion();
    }
}