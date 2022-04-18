using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button exitBtn;

    // triggers on opening pause menu
    public void OnEnable()
    {
        Time.timeScale = 0f;
    }
    
    //triggers on closing pause menu
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void OpenSettings()
    {
        
    }

    public void ExitLevel()
    {
        GameManager.Instance.LoadScene(2);
    }
}
