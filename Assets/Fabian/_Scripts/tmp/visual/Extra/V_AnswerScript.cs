using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public V_QuizManager V_quizManager;

    public void Answer()
    {
        if (isCorrect)
        {
            Debug.Log("Correct Anwer");
            V_quizManager.currect();
        }
        else
        {
            Debug.Log("wrong Anwer");
            V_quizManager.currect();
        }
    }
}
