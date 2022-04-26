using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickBtn3 : Question
{



    public Text evaluate;
     

    public Button[] answerBtnArr;
     
    // Start is called before the first frame update
    void Start()
    {
        RandQuest();
    }

    public void RandQuest()
    {


        answerBtnArr[0].GetComponentInChildren<Text>().text = "green";

        answerBtnArr[1].GetComponentInChildren<Text>().text = "red";
         
        //give random values for question and answers


        answerBtnArr[0].onClick.AddListener(CheckAnswerCorrect);
        answerBtnArr[1].onClick.AddListener(CheckAnswerCorrect);
        
         
        answerBtnArr[0].onClick.AddListener(SetAnswerWrong);
        answerBtnArr[1].onClick.AddListener(SetAnswerCorrect);

    }
    public void CheckAnswerCorrect()
    {

        evaluate.text = "wrong answer";
    }
    public void CheckAnswerRight()
    {
        evaluate.text = "correct answer";
    }
}
