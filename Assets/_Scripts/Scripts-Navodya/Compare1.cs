using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compare1 : Question
{
    // Start is called before the first frame update

    public Button btn1Anna;
    public Button btn2;

    public Text evaluate;

    void Start()
    {
        btn1Anna.onClick.AddListener(CheckAnswerCorrect);
        btn2.onClick.AddListener(CheckAnswer);
        btn1Anna.onClick.AddListener(SetAnswerCorrect);
        btn2.onClick.AddListener(SetAnswerWrong);
    }

 public void CheckAnswer()
    {
        evaluate.text = "incorrect";
    }
    public void CheckAnswerCorrect() {
        evaluate.text = "correct";
    }
 
 
}
