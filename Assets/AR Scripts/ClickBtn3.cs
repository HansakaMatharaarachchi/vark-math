using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Scripts;
using UnityEngine.UI;

public class ClickBtn3 : MonoBehaviour
{
    //kins 3 scene


    public Text evaluate;
     

    public Button[] answerBtnArr;
     
    // Start is called before the first frame update
    void Start()
    {
        RandQuest();
    }

    public void RandQuest()
    {


        answerBtnArr[0].GetComponentInChildren<Text>().text = "blue";

        answerBtnArr[1].GetComponentInChildren<Text>().text = "yellow";
         
        //give random values for question and answers


        answerBtnArr[1].onClick.AddListener(CheckAnswerCorrect);
        answerBtnArr[0].onClick.AddListener(CheckAnswerRight);
        
       /*  
        answerBtnArr[0].onClick.AddListener(SetAnswerWrong);
        answerBtnArr[1].onClick.AddListener(SetAnswerCorrect);*/

    }
    public void CheckAnswerCorrect()
    {

        evaluate.text = "wrong answer";
        GameManager.Instance.LoadNextScene();
    }
    public void CheckAnswerRight()
    {
        evaluate.text = "correct answer";
        PlayerPrefs.SetFloat("CK", (PlayerPrefs.GetFloat("CK") + 1));
        PlayerPrefs.Save();
        GameManager.Instance.LoadNextScene();
    }
}
