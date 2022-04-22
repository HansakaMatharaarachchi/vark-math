using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

[Serializable]
public struct ParentsQuestion
{
    public string question;
    public List<string> choices;
}

public class ParentsQuestionnaire : MonoBehaviour
{
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private ToggleGroup choices;
    [SerializeField] private Button nextQuestionBtn;

    [SerializeField] private List<ParentsQuestion> parentsQuestionnaire = new List<ParentsQuestion>();

    private int visual, auditory, kinesthetic;
    private int selectedIndex;

    private void Start()
    {
        DisplayQuestionsAndAnswers(selectedIndex);
        nextQuestionBtn.onClick.AddListener(ChangeQuestion);
    }

    private void ChangeQuestion()
    {
        selectedIndex++;
        CollectResponse();
        if (selectedIndex < 10)
        {
            DisplayQuestionsAndAnswers(selectedIndex);
            if (selectedIndex == 9)
            {
                nextQuestionBtn.GetComponentInChildren<TMP_Text>().text = "Finish";
            }
        }
        Debug.Log("V " + visual + " A " + auditory + " K " + kinesthetic);
    }

    private void CollectResponse()
    {
        switch (choices.GetFirstActiveToggle().name)
        {
            case "Visual":
                ++visual;
                break;
            case "Auditory":
                ++auditory;
                break;

            case "Kinesthetic":
                ++kinesthetic;
                break;
        }

        choices.SetAllTogglesOff();
    }

    private void DisplayQuestionsAndAnswers(int index)
    {
        Debug.Log(index);
        questionText.text = index + 1 + ". " + parentsQuestionnaire[index].question;
        for (int i = 0; i < choices.transform.childCount; i++)
        {
            choices.transform.GetChild(i).gameObject.GetComponentInChildren<Text>().text =
                parentsQuestionnaire[index].choices[i];
        }
    }

    private void Update()
    {
        nextQuestionBtn.interactable = choices.AnyTogglesOn();
    }
}