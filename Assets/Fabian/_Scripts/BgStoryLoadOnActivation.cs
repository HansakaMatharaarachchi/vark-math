using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;

public class BgStoryLoadOnActivation : MonoBehaviour
{
    void OnEnable()
    {
        //loads the first question of the current level
        GameManager.Instance.PlayQuestion(GameManager.Instance.CurrentQuestionIndex);
    }
}
