using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private int health = 3;
    [SerializeField] private Image[] filledHearts;

    private void Start()
    {
        for (int i = 0; i < GameManager.Instance.currentLevelProgress.noOfWrongAnswers; i++)
        {
            DecreaseHealth();
        }
    }

    private void DecreaseHealth()
    {
        if (health == 0) return;
        --health;
        filledHearts[health].enabled = false;
    }
}