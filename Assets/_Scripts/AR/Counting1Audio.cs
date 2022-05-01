
// Start is called before the first frame update
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;
using UnityEngine.UI;

public class Counting1Audio : Question
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
        int value1 = 7;
        int value2 = 4;
        int value3 = 5;
        int value4 = 8;

        answerBtnArr[0].GetComponentInChildren<Text>().text = value1.ToString();

        answerBtnArr[1].GetComponentInChildren<Text>().text = value2.ToString();

        answerBtnArr[2].GetComponentInChildren<Text>().text = value3.ToString();
        answerBtnArr[3].GetComponentInChildren<Text>().text = value4.ToString();
        //give random values for question and answers


        answerBtnArr[0].onClick.AddListener(CheckAnswerCorrect);
        answerBtnArr[1].onClick.AddListener(CheckAnswerCorrect);
        answerBtnArr[2].onClick.AddListener(CheckAnswerRight);
        answerBtnArr[3].onClick.AddListener(CheckAnswerCorrect);

    }
    public void CheckAnswerCorrect()
    {
        evaluate.text = "wrong answer";
        SetAnswerWrong();
    }
    public void CheckAnswerRight()
    {
        evaluate.text = "correct answer";
        SetAnswerCorrect();
    }
}

