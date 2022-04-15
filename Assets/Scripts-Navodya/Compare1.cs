using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compare1 : MonoBehaviour
{
    // Start is called before the first frame update

    public Button btn1Anna;
    public Button btn2;

    public Text evaluate;

    void Start()
    {
        btn1Anna.onClick.AddListener(CheckAnswerCorrect);
        btn2.onClick.AddListener(CheckAnswer);
    }

 public void CheckAnswer()
    {
        evaluate.text = "incorrect";
    }
    public void CheckAnswerCorrect() {
        evaluate.text = "correct";
    }
 
 
}
