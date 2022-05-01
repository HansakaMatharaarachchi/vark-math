using System;
using _Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [Header("Player Profile details")] [SerializeField]
    private TMP_Text userLevel;

    [SerializeField] private TMP_Text userName;
    [SerializeField] private TMP_Text goldCoinAmount;
    [SerializeField] private Transform playerContainer;
    [SerializeField] private Transform spaceShipContainer;
    [SerializeField] private Button[] levelChoices;

    [SerializeField] private Transform rewardCollectedStatus;
    [SerializeField] private GameObject rewardPanel;
    [SerializeField] private TMP_Text timeRemainingForNextDay;

    [SerializeField] private Image rewardAvailableImage;
    private bool inRewardsCollected;

    private void Awake()
    {
        if (!GameManager.Instance.Player.IsDailyRewardCollected)
        {
            rewardAvailableImage.enabled = true;
        }

        userName.text = GameManager.Instance.Player.Name;
        userLevel.text = (GameManager.Instance.Player.Level).ToString();
        DisplayEquippedItems();
    }

    private void DisplayEquippedItems()
    {
        DisplaySpaceShip();
        DisplayCharacter();
    }

    private void DisplaySpaceShip()
    {
        if (spaceShipContainer.childCount > 0)
            Destroy(playerContainer.GetChild(0).gameObject);
        GameObject spaceShip =
            ((SpaceShipObject) GameManager.Instance.Store.GetItemBuyId(GameManager.Instance.Player.Inventory
                .GetEquippedSpaceShipId())).lobbyPrefab;
        Instantiate(spaceShip, spaceShipContainer);
    }

    private void DisplayCharacter()
    {
        if (playerContainer.childCount > 0)
            Destroy(playerContainer.GetChild(0).gameObject);
        GameObject character =
            ((CostumeObject) GameManager.Instance.Store.GetItemBuyId(GameManager.Instance.Player.Inventory
                .GetEquippedCostumeId())).lobbyPrefab;
        Instantiate(character, playerContainer.position, playerContainer.rotation, playerContainer);
    }

    private void Update()
    {
        goldCoinAmount.text = GameManager.Instance.Player.GoldCoinAmount.ToString();
        if (inRewardsCollected)
        {
            timeRemainingForNextDay.text = (TimeSpan.FromHours(24) - DateTime.Now.TimeOfDay).ToString("h'H 'm'M'");
        }
    }

    public void OpenRewardCollector()
    {
        rewardPanel.SetActive(true);
        if (!GameManager.Instance.Player.IsDailyRewardCollected)
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
        GameManager.Instance.LoadScene(2);
    }

    public void OpenReport()
    {
        GameManager.Instance.LoadScene(3);
    }

    public void OpenRateUs()
    {
        GameManager.Instance.LoadScene(4);
    }

    public void DisplayLevelSelection()
    {
        for (int i = 0; i < GameManager.Instance.Player.Level; i++)
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

    public void OpenSettings()
    {
        GameManager.Instance.OpenSettings();
    }
}