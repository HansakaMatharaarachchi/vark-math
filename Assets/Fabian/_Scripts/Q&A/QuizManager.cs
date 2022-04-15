using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public List<QandA> QnA ;
    public GameObject[] options;
    public int currentQuestion;
    
    public Text questionTxt;
    public int score;

    private void Start()
    {
        generateQuestions();
    }

    public void Gameover() 
    {
       // manager.next_btn();

    }


    public void currect()
    {
        
        QnA.RemoveAt(currentQuestion);
        generateQuestions();
        score++;

    }

    public void wrong()
    {
        QnA.RemoveAt(currentQuestion);
        generateQuestions();
       
    }

    void SetAnswers()
    {
        for (int i=0; i< options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Aswers[i];

            if(QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    public void generateQuestions()
    {
        Debug.Log(QnA.Count);
        Debug.Log("dncisnds");
        if (QnA.Count > 0) 
        {
            currentQuestion = Random.Range(0,QnA.Count);
            questionTxt.text = QnA[currentQuestion].Questions;
            // SetAnswers();

        }
        else if (QnA.Count==0)
        {
            //Debug.Log(currentQuestion);
            Gameover();
        }
        else
        {
            //Debug.Log("out of question");
            //Gameover();
        }
    }

}
