using System;
using _Scripts;
using _Scripts.Firebase;
using Firebase.Auth;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class Bg : MonoBehaviour
{
    [Header("Player Profile details")]
    [SerializeField] private Transform playerContainer;

    

    private void Awake()
    {
        DisplayEquippedItems();
    }

    private void DisplayEquippedItems()
    {
        if (playerContainer.childCount > 0)
            Destroy(playerContainer.GetChild(0).gameObject);
        // var character = ((CostumeObject)GameManager.Instance.store.GetItemBuyId(GameManager.Instance.player.inventory.GetEquippedCostumeId())).bgPrefab;
        // Instantiate(character, playerContainer.position, playerContainer.rotation, playerContainer);
    }
}
