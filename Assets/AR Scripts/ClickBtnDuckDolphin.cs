using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickBtnDuckDolphin : MonoBehaviour
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
        //give random values for question and answers
 
        answerBtnArr[0].onClick.AddListener(CheckAnswerCorrect);
        answerBtnArr[1].onClick.AddListener(CheckAnswerRight);

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
