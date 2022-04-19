using System;
using _Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [Header("Player Profile details")]
    [SerializeField] private TMP_Text userLevel;
    [SerializeField] private TMP_Text userName;
    [SerializeField] private TMP_Text goldCoinAmount;
    [SerializeField] private TMP_Text soilSampleAmount;
    [SerializeField] private Transform playerContainer;
    [SerializeField] private Button[] levelChoices;
    
    [SerializeField] private Transform rewardCollectedStatus;
    [SerializeField] private GameObject rewardPanel;
    [SerializeField] private TMP_Text timeRemainingForNextDay;
    
    [SerializeField] private Image rewardAvailableImage;
    private bool inRewardsCollected;

    private void Awake()
    {
        if (!GameManager.Instance.player.isDailyRewardCollected)
        {
            rewardAvailableImage.enabled = true;
        }
        userName.text = GameManager.Instance.player.Name;
        userLevel.text = (GameManager.Instance.player.level).ToString();
        DisplayEquippedItems();
    }

    private void DisplayEquippedItems()
    {
        if (playerContainer.childCount > 0)
            Destroy(playerContainer.GetChild(0).gameObject);
        var character = ((CostumeObject) GameManager.Instance.store.GetItemBuyId(GameManager.Instance.player.inventory.GetEquippedCostumeId())).lobbyPrefab;
        Instantiate(character, playerContainer.position, playerContainer.rotation, playerContainer);
    }

    private void Update()
    {
        goldCoinAmount.text = GameManager.Instance.player.GoldCoinAmount.ToString();
        if (inRewardsCollected)
        {
            timeRemainingForNextDay.text = (TimeSpan.FromHours(24) - DateTime.Now.TimeOfDay).ToString("h'H 'm'M'");
        }
    }

    public void OpenRewardCollector()
    {
        rewardPanel.SetActive(true);
        if (!GameManager.Instance.player.isDailyRewardCollected)
        {
            rewardCollectedStatus.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            inRewardsCollected = true;
            rewardCollectedStatus.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void CloseRewardCollector()
    {
        inRewardsCollected = false;
        rewardPanel.SetActive(false);
    }

    public void OpenStore()
    {
        GameManager.Instance.LoadScene(3);
    }

    public void DisplayLevelSelection()
    {
        for (int i = 0; i < GameManager.Instance.player.level; i++)
        {
            levelChoices[i].interactable = true;
            levelChoices[i].transform.GetChild(1).GetComponentInChildren<Image>().enabled = false;
        }
    }

    public void StartLevel(int level)
    {
        GameManager.Instance.PlayLevel(level);
    }

    public void CollectRewardsOnclick()
    {
        inRewardsCollected = true;
        rewardAvailableImage.enabled = false;
        GameManager.Instance.CollectDailyReward();
    }

    public void Logout()
    {
        GameManager.Instance.SignOut();
    }

}
