using System.Collections;
using System.Collections.Generic;
using _Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class V_QuizManager : MonoBehaviour
{
    public List<V_QandA> QnA;
    public Button[] options;
    public int currentQuestion;
    public GameObject[] assets;

    public TMP_Text questionTxt;

    private void Start()
    {
        generateQuestions();
    }

    public void currect()
    {
        // QnA.RemoveAt(currentQuestion);
        assets[currentQuestion - 1].SetActive(false);
        generateQuestions();
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<V_AnswerScript>().isCorrect = false;
            Debug.Log(currentQuestion);
            options[i].GetComponentInChildren<TMP_Text>().text = QnA[currentQuestion].Aswers[i];
            
            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<V_AnswerScript>().isCorrect = true;
            }
        }
    }

    public void generateQuestions()
    {
        // for (int i = 0; i < QnA.Count; i++)
        // {
        //     currentQuestion = i;
        // }
        //currentQuestion = Random.Range(0, QnA.Count);
        if (QnA.Count > 0)
        {
            assets[currentQuestion].SetActive(true);
            questionTxt.text = QnA[currentQuestion].Questions;
            SetAnswers();
            currentQuestion++;
        }
        else
        {
            Debug.Log("out of question");
        }
        
    }
}
