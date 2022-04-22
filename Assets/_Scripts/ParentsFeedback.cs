using System.Collections;
using System.Collections.Generic;
using _Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParentsFeedback : MonoBehaviour
{
    [SerializeField] private Button[] levelSelections;

    [SerializeField] private TMP_Text learningStyle;
    [SerializeField] private TMP_Text noOfAttempts;
    [SerializeField] private Button levelPassed;

    [SerializeField] private TMP_Text levelInfo;
    [SerializeField] private TMP_Text correctAnsCount;
    [SerializeField] private TMP_Text wrongAnsCount;
    [SerializeField] private TMP_Text levelProgress;

    void Start()
    {
        for (int i = 1; i <= levelSelections.Length; i++)
        {
            if (GameManager.Instance.player.levelStats[i] != null)
            {
                levelSelections[i].interactable = true;
                levelSelections[i].onClick.AddListener(delegate { ShowInfoForALevel(i); });
            }
            else
            {
                levelSelections[i].interactable = false;
            }
        }

        learningStyle.text = GameManager.Instance.player.learningStyle.ToString();
        ShowInfoForALevel(GameManager.Instance.player.level - 1);
    }

    private void ShowInfoForALevel(int level)
    {
        //todo change according to the level title
        levelInfo.text = level switch
        {
            1 => "Level " + level + " Counting",
            2 => "Level " + level + " Counting",
            3 => "Level " + level + " Counting",
            4 => "Level " + level + " Counting",
            5 => "Level " + level + " Counting",
            _ => levelInfo.text
        };
        Level currentLevel = GameManager.Instance.player.levelStats[level];
        noOfAttempts.text = currentLevel.noOfAttempts.ToString();
        levelPassed.interactable = currentLevel.isPassed;
        correctAnsCount.text = currentLevel.lastAttemptProgress.noOfCorrectAnswers.ToString();
        wrongAnsCount.text = currentLevel.lastAttemptProgress.noOfWrongAnswers.ToString();
        levelProgress.text = currentLevel.lastAttemptProgress.CalculateLevelProgress().ToString();
    }
}