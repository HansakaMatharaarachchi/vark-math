
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest  : MonoBehaviour
{


    public Text evaluate;
    static int i = -1;
    static int a = 0;
    static int vis = 0;
    static int aud = 0;
    static int kins = 0;
   
    public string[] answerArr;
    public Button QBtn;
    public string[] quesArr;
  
  
  public  Button[] answerBtnArr;


    public Button submit;
    // Start is called before the first frame update

    

    void Start()
    {
     Rand();
        
  }

 

    public void Rand ()
    {
        i = i + 1;

        if (a <= answerArr.Length - 1)
        {
            for (int k = 0; k < answerBtnArr.Length; k++)
            {
                answerBtnArr[k].GetComponentInChildren<Text>().text = answerArr[a].ToString();
                
                a += 1;
            }

            
        }
         

         //give random values for question and answers
       

        if (a ==answerArr.Length - 1)
 {
            GiveStyle();
        }
        if (i < quesArr.Length)
        {
            QBtn.GetComponentInChildren<Text>().text = quesArr[i].ToString();
        }
        else {
            Debug.Log("d");

        }
         

    }
    public void CheckAnswerVisual()
    {
        vis++;




    }
    public void CheckAnswerAudio()
    {
        aud++;

    }
    public void CheckAnswerKines()
    {
        kins++;

    }
    public void GiveStyle()
    {
        int num1, num2, num3, max;
        // set the value of the three numbers
        num1 = vis;
        num2 = aud;
        num3 = kins;
        if (num1 > num2)
        {
            if (num1 > num3)
            {
                max = num1;
                

            }
            else
            {
                max = num3;
               

            }
        }
        else if (num2 > num3)
        {
            max = num2;
            
        }
        else
        {
            max = num3;
 

        }
        evaluate.text = "yay";
      

    }
}
 

