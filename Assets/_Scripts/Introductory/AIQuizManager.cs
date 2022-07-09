using System.Collections.Generic;
using System.Threading.Tasks;
using _Scripts;
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
    [SerializeField] private GameObject skipPanel;

    [SerializeField] private GameObject endPanel;
    [SerializeField] private GameObject findingTheStylePanel;

    [SerializeField] private TMP_Text questionTxt;
    [SerializeField] private Button[] choices;

    [SerializeField] private AudioSource audioSource;
    
    [SerializeField] private TMP_Text detectedLearningStyleTxt;

    private int selectedQuestionIndex;
    private int correctAnswersCount;

    public void StartQuiz()
    {
        PlayAuditoryQuestion();
    }

    private async void PlayAuditoryQuestion()
    {
        envContainer.SetActive(true);
        skipPanel.SetActive(true);
        while (audioSource.isPlaying)
        {
            await Task.Delay(1000);
        }

        envContainer.SetActive(false);
        skipPanel.SetActive(false);

        questionPanel.SetActive(true);
        DisplayQuestion();
        foreach (Button button in choices)
        {
            button.onClick.AddListener(delegate { CheckAnswer(button); });
        }
    }

    private async void DisplayQuestion()
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
            PlayerPrefs.SetFloat("CA", (float)correctAnswersCount / parentsQuestionnaire.Count);
            PlayerPrefs.Save();            
            findingTheStylePanel.SetActive(true);
            await Task.Delay(2000);
            findingTheStylePanel.SetActive(false);
            endPanel.SetActive(true);
            detectedLearningStyleTxt.text =  "You have been detected as a <br> <br>" + GameManager.Instance.FindLearningStyle() + " learner";
            
            // delete saved values for learning style detection
            PlayerPrefs.DeleteKey("PV");
            PlayerPrefs.DeleteKey("PA");
            PlayerPrefs.DeleteKey("PK");
                
            PlayerPrefs.DeleteKey("CV");
            PlayerPrefs.DeleteKey("CA");
            PlayerPrefs.DeleteKey("CK");
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

    public void GoToLobby()
    {
        GameManager.Instance.InitGame();
    }

    public void SkipIntro() {
        envContainer.SetActive(false);
    }
}