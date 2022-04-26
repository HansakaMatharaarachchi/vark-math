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
        for (int i = 0; i < GameManager.Instance.player.levelStats.Length; i++)
        {
            if (GameManager.Instance.player.levelStats[i].noOfAttempts > 0)
            {
                levelSelections[i].interactable = true;
            }
        }
        learningStyle.text = GameManager.Instance.player.learningStyle.ToString();
        int lvlToLoadOnStart = GameManager.Instance.player.level - 1; // loads info for last unlocked level
        if (GameManager.Instance.player.level - 1 == 0)
            lvlToLoadOnStart = 1;
        ShowInfoForALevel(lvlToLoadOnStart); 
    }

    public void ShowInfoForALevel(int level)
    {
        if (GameManager.Instance.player.levelStats[level - 1].noOfAttempts > 0)
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
            Level currentLevel = GameManager.Instance.player.levelStats[level - 1];
            noOfAttempts.text = currentLevel.noOfAttempts.ToString();
            levelPassed.interactable = currentLevel.isPassed;
            correctAnsCount.text = currentLevel.lastAttemptProgress.noOfCorrectAnswers.ToString();
            wrongAnsCount.text = currentLevel.lastAttemptProgress.noOfWrongAnswers.ToString();
            levelProgress.text = currentLevel.lastAttemptProgress.CalculateLevelProgress() + " %";
        }
    }

    public void BackToLobby()
    {
        GameManager.Instance.LoadScene(2);
    }
}