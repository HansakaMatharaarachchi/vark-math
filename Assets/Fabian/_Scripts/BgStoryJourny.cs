using System;
using _Scripts;
using _Scripts.Firebase;
using Firebase.Auth;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class BgStoryJourny : MonoBehaviour
{
    [SerializeField] private Transform playerContainer;
    [SerializeField] private Transform spaceShipContainer;

    private void Awake()
    {
        // DisplayEquippedItems();
    }

    private void DisplayEquippedItems()
    {
        if (playerContainer.childCount > 0)
            Destroy(playerContainer.GetChild(0).gameObject);
        GameObject spaceShip = ((SpaceShipObject)GameManager.Instance.store.GetItemBuyId(GameManager.Instance.player.inventory.GetEquippedSpaceShipId())).journeyPrefab;
        Instantiate(spaceShip, spaceShipContainer);
    }
}
