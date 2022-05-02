using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Toggle = UnityEngine.UI.Toggle;

[Serializable]
public struct VisualQuestion
{
    public string question;
    public List<string> choices;
}

public class V_Inro : MonoBehaviour
{
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private ToggleGroup choices;
    [SerializeField] private Button nextQuestionBtn;

    [SerializeField] private List<VisualQuestion> v_Inro = new List<VisualQuestion>();

    private int visual, auditory, kinesthetic;
    private int selectedIndex;

    private void Start()
    {
        DisplayQuestionsAndAnswers(selectedIndex);
        ++selectedIndex;
        nextQuestionBtn.onClick.AddListener(delegate { ChangeQuestion(selectedIndex); });
    }

    private void ChangeQuestion(int index)
    {
        DisplayQuestionsAndAnswers(index);
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

        Debug.Log(visual + " " + auditory + " " + kinesthetic);
        choices.SetAllTogglesOff();
        ++selectedIndex;
    }

    private void DisplayQuestionsAndAnswers(int index)
    {
        questionText.text = index + 1 + ". " + v_Inro[index].question;
        foreach (var choice in choices.GetComponentsInChildren<Toggle>())
        {
        }

        for (int i = 0; i < choices.transform.childCount; i++)
        {
            choices.transform.GetChild(i).gameObject.GetComponentInChildren<Text>().text = v_Inro[index].choices[i];
        }
    }

    private void Update()
    {
        nextQuestionBtn.interactable = choices.AnyTogglesOn();
    }
}

