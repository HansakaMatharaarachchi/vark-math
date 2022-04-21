using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using _Scripts;
using UnityEngine;
using UnityEngine.UI;
public class CountingVisual3 : Question
{

    // public Text evaluate;
    public Button[] answerBtnArr;
    //public int current;

    // Start is called before the first frame update
    void Start()
    {
        RandQuest();
    }

    public void RandQuest()
    {
        int value1 = 7;
        int value2 = 8;
        int value3 = 9;
        int value4 = 10;


        answerBtnArr[0].GetComponentInChildren<Text>().text = value1.ToString();

        answerBtnArr[1].GetComponentInChildren<Text>().text = value2.ToString();

        answerBtnArr[2].GetComponentInChildren<Text>().text = value3.ToString();
        answerBtnArr[3].GetComponentInChildren<Text>().text = value4.ToString();
        //give random values for question and answers


        answerBtnArr[0].onClick.AddListener(SetAnswerWrong);
        answerBtnArr[1].onClick.AddListener(SetAnswerWrong);
        answerBtnArr[2].onClick.AddListener(SetAnswerWrong);
        answerBtnArr[3].onClick.AddListener(SetAnswerCorrect);

    }

}