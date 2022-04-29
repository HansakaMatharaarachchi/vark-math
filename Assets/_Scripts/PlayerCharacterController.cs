using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField] private GameObject weaponContainer;

    [SerializeField] private bool isInLobby;
    [SerializeField] private bool isInStore;

    private Animator characterAnimator;

    private void Start()
    {
        characterAnimator = GetComponent<Animator>();
        if (isInLobby)
        {
            InLobby();
        }

        if (isInStore)
        {
            InStore();
        }
    }

    private void InLobby()
    {
        characterAnimator.SetTrigger("IsInLobby");
        weaponContainer.transform.Find(GameManager.Instance.Player.Inventory.GetEquippedEquipmentId()).gameObject
            .SetActive(true);
    }

    private void InStore()
    {
        characterAnimator.SetTrigger("IsInStore");
        weaponContainer.transform.Find(GameManager.Instance.Player.Inventory.GetEquippedEquipmentId()).gameObject
            .SetActive(true);
    }
}