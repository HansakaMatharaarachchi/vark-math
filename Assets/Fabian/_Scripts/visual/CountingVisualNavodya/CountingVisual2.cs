using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
using _Scripts;
using UnityEngine;
using UnityEngine.UI;
public class CountingVisual2 : MonoBehaviour
{
    
    public Text evaluate;
    public Button[] answerBtnArr;
    public Text go;
    // Start is called before the first frame update
    void Start()
    {
        RandQuest();
    }

    public void RandQuest()
    {
        int value1 = 5;
        int value2 = 4;
        int value3 = 2;
        int value4 = 3;

        answerBtnArr[0].GetComponentInChildren<Text>().text = value1.ToString();

        answerBtnArr[1].GetComponentInChildren<Text>().text = value2.ToString();

        answerBtnArr[2].GetComponentInChildren<Text>().text = value3.ToString();
        answerBtnArr[3].GetComponentInChildren<Text>().text = value4.ToString();
        //give random values for question and answers


        answerBtnArr[0].onClick.AddListener(CheckAnswerCorrect);
        answerBtnArr[1].onClick.AddListener(CheckAnswerRight);
        answerBtnArr[2].onClick.AddListener(CheckAnswerCorrect);
        answerBtnArr[3].onClick.AddListener(CheckAnswerCorrect);

    }
    public void CheckAnswerCorrect()
    {
        evaluate.text = "wrong answer, Try again!!";
        go.text = "Try again!";

        //GameManager.Instance.player.levelsProgresses[GameManager.Instance.player.levelsProgresses.Count - 1].AddWrongAnswer();
       
        //GameManager.Instance.PlayNextQuestion();

    }
    public void CheckAnswerRight()
    {
        evaluate.text = "correct answer, You won!!";
        go.text = "Next";
       // GameManager.Instance.player.levelsProgresses[GameManager.Instance.player.levelsProgresses.Count - 1].AddCorrectAnswer();
       // GameManager.Instance.PlayNextQuestion();
    }

}
