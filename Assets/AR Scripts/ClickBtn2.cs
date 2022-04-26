using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using _Scripts;

public class ClickBtn2 : MonoBehaviour { 
    //kins 2 introduction


    public Text evaluate;
     

    public Button[] answerBtnArr;
     
    // Start is called before the first frame update
    void Start()
    {
        RandQuest();
    }

    public void RandQuest()
    {


        answerBtnArr[0].GetComponentInChildren<Text>().text = "cat";

        answerBtnArr[1].GetComponentInChildren<Text>().text = "tiger";
         
        //give random values for question and answers


        answerBtnArr[0].onClick.AddListener(CheckAnswerCorrect);
        answerBtnArr[1].onClick.AddListener(CheckAnswerRight);
        
         
/*        answerBtnArr[0].onClick.AddListener(SetAnswerWrong);
        answerBtnArr[1].onClick.AddListener(SetAnswerCorrect);*/

    }
    public void CheckAnswerCorrect()
    {

        evaluate.text = "wrong answer";
    GameManager.Instance.LoadNextScene();
    PlayerPrefs.SetFloat("CK", 0);
    GameManager.Instance.LoadNextScene();
}
    public void CheckAnswerRight()
    {
        evaluate.text = "correct answer";
        PlayerPrefs.SetFloat("CK", 1);
        PlayerPrefs.Save();
        GameManager.Instance.LoadNextScene();
    }
}
