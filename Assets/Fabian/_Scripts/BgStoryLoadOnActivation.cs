using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;

public class BgStoryLoadOnActivation : MonoBehaviour
{
    void OnEnable()
    {
        GameManager.Instance.LoadScene(3);
    }
}
