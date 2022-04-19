using System;
using System.Collections.Generic;
using _Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreUIManager : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private TMP_Text lockState;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private Button purchaseItemButton;
    [SerializeField] private Button equipItemButton;

    [SerializeField] private GameObject buyMenu;
    [SerializeField] private GameObject categoryMenu;

    [SerializeField] private Transform itemContainer;
    [SerializeField] private TMP_Text goldCoinAmount;

    private bool inSelectedItemCategory;
    private StoreItemObject[] selectedItemCategory;
    private int selectedIndex;

    private void Update()
    {
        goldCoinAmount.text = GameManager.Instance.player.GoldCoinAmount.ToString();
    }

    public void ChangePanels()
    {
        if (inSelectedItemCategory)
        {
            categoryMenu.SetActive(true);
            buyMenu.SetActive(false);
            itemContainer.gameObject.SetActive(false);
            inSelectedItemCategory = false;
            selectedIndex = 0;
        }
        else
        {
            GameManager.Instance.LoadScene(2);
        }
    }

    public void FindAndViewSelectedItemCategory(string categoryName)
    {
        inSelectedItemCategory = true;
        categoryMenu.SetActive(false);
        buyMenu.SetActive(true);
        selectedItemCategory = GameManager.Instance.store.Items[categoryName];
        ChangeItem(0);
    }
    public void ChangeItem(int change)
    {
        selectedIndex += change;
        if (selectedIndex < 0)
            selectedIndex = selectedItemCategory.Length - 1;
        else if (selectedIndex > selectedItemCategory.Length - 1)
            selectedIndex = 0;
        DisplayItem(selectedItemCategory[selectedIndex]);
    }

    private void DisplayItem(StoreItemObject itemObject)
    {
        itemContainer.gameObject.SetActive(true);
        purchaseItemButton.gameObject.SetActive(true);
        purchaseItemButton.interactable = true;
        equipItemButton.gameObject.SetActive(false);
        equipItemButton.interactable = true;
        lockState.gameObject.SetActive(false);
        
        purchaseItemButton.GetComponentInChildren<Text>().text = itemObject.price.ToString();
        itemName.text = itemObject.name;

        //checks if the player has the required level to purchase the item 
        if (itemObject.levelToBeUnlocked <= GameManager.Instance.player.level)
        {
            //checks if the player already has the item
            if (GameManager.Instance.player.inventory.IsItemOwned(itemObject))
            {
                purchaseItemButton.gameObject.SetActive(false);
                equipItemButton.gameObject.SetActive(true);
                equipItemButton.interactable = GameManager.Instance.player.inventory.Items[itemObject.type + "s"].Count != 1;
                
                //checks if the item is equipped by the player
                if (GameManager.Instance.player.inventory.GetEquippedItemIdForAType(itemObject.type) == itemObject.id)
                {
                    equipItemButton.GetComponentInChildren<Text>().text = "EQUIPPED";
                    equipItemButton.interactable = false;
                }
                else
                {
                    equipItemButton.GetComponentInChildren<Text>().text = "EQUIP";
                }
            }
            else
            {
                //checks if the player has the required gold coin amount
                if (itemObject.price > GameManager.Instance.player.GoldCoinAmount)
                {
                    purchaseItemButton.interactable = false;
                    return;
                }
                purchaseItemButton.interactable = true;
            }
        }
        else
        {
            lockState.text = "Unlocks at Level " + itemObject.levelToBeUnlocked;
            lockState.gameObject.SetActive(true);
            purchaseItemButton.interactable = false;
        }
        if (itemContainer.childCount > 0)
            Destroy(itemContainer.GetChild(0).gameObject);
        Instantiate(itemObject.prefab, itemContainer);
    }
    
    public void BuyItemOnclick()
    {
        GameManager.Instance.player.BuyItem(selectedItemCategory[selectedIndex]);
        // purchaseItemButton.interactable = false;
        purchaseItemButton.gameObject.SetActive(false);
        equipItemButton.gameObject.SetActive(true);
        equipItemButton.GetComponentInChildren<Text>().text = "EQUIP";
    }

    public void EquipItemOnClick()
    {
        GameManager.Instance.player.EquipItem(selectedItemCategory[selectedIndex]);
        equipItemButton.GetComponentInChildren<Text>().text = "EQUIPPED";
        equipItemButton.interactable = false;
    }
}