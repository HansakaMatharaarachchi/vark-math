using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answerbutton : MonoBehaviour

{
    public static int value1;
    public static int value2;
    public static int random2;
    public Text Textans;
    public Button button1x;                    //used to attach Unity button component to script
    public Button button2x;



    // Start is called before the first frame update
    void Start()
    {
        Question();
    }

    // Update is called once per frame
    void Update()
    {

    }




    public void Question()
    {
        // Get a re 
        value1 = 1;       //give random values for question and answers
        value2 = 2;

        button1x.GetComponentInChildren<Text>().text = value1.ToString();    //used to display random values in the text
        button2x.GetComponentInChildren<Text>().text = value2.ToString();     //attached to a button

        button1x.onClick.AddListener(CheckAnswer1);
        button2x.onClick.AddListener(CheckAnswer2);


    }
    public void CheckAnswer2()
    {

        Textans.text = "your answer is wrong, dear..the correct answer is 2";



    }
    public void CheckAnswer1()
    {

        Textans.text = "wow!!you won";
    }
}
