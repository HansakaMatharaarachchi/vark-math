using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Scripts;
using TMPro;
using UnityEngine;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text passedLevelInfoText;
    [SerializeField] private GameObject rewardContainer;
    [SerializeField] private GameObject unlockedItemsContainer;


    private async void OnEnable()
    {
        passedLevelInfoText.text = "You have successfully completed <br> Level " + 2;
        await Task.Delay(3000);
        rewardContainer.SetActive(false);
        unlockedItemsContainer.SetActive(true);
    }

    public void OpenStoreOnClick()
    {
        GameManager.Instance.LoadScene(4);
    }

    public void PlayNextLvlOnClick()
    {
        GameManager.Instance.PlayLevel(GameManager.Instance.currentLevel + 1);
    }

    public void RetryLevelOnClick()
    {
        GameManager.Instance.PlayLevel(GameManager.Instance.currentLevel);
    }

    public void LoadLobbyOnClick()
    {
        GameManager.Instance.LoadScene(3);
    }
}
