 
   // Start is called before the first frame update
    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnAction : MonoBehaviour
{
    // Start is called before the first frame update
    public Text evaluate;

    public void CheckAnswerCorrect()
    {
        {
            evaluate.text = "Yay! It's the big animal";
        }


    }
    public void CheckAnswerInCorrect()
    {
        {
            evaluate.text = "Wrong! It's the small animail";

        }


    }


}

