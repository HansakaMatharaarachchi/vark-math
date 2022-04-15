using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory
{
    public Dictionary<string, Dictionary<string, bool>> Items { get; set; } =
        new Dictionary<string, Dictionary<string, bool>>();

    public Inventory()
    {
        Items.Add("Costumes", new Dictionary<string, bool>());
        Items.Add("SpaceShips", new Dictionary<string, bool>());
        Items.Add("Equipments", new Dictionary<string, bool>());
    }

    public void AddItem(StoreItemObject storeItemObject)
    {
        Items[storeItemObject.type + "s"].Add(storeItemObject.id, Items[storeItemObject.type + "s"].Count == 0);
    }
    
    public bool IsItemOwned(StoreItemObject storeItemObject)
    {
        return Items[storeItemObject.type + "s"].ContainsKey(storeItemObject.id);
    }
    
    public bool IsItemEquipped(StoreItemObject storeItemObject)
    {
        return Items[storeItemObject.type + "s"][storeItemObject.id].Equals(true);
    }
    
    public string GetEquippedItemIdForAType(StoreItemType type)
    {
        return type switch
        {
            StoreItemType.Costume => GetEquippedCostumeId(),
            StoreItemType.SpaceShip => GetEquippedSpaceShipId(),
            StoreItemType.Equipment => GetEquippedEquipmentId(),
            _ => null
        };
    }
    
    public void SetEquippedItem(StoreItemObject storeItemObject)
    {
        Items[storeItemObject.type + "s"][GetEquippedItemIdForAType(storeItemObject.type)] = false;
        Items[storeItemObject.type + "s"][storeItemObject.id] = true;
    }

    public string GetEquippedCostumeId()
    {
        foreach (var item in Items["Costumes"])
        {
            if (item.Value.Equals(true))
            {
                return item.Key;
            }
        }
        return null;
    }
    public string GetEquippedSpaceShipId()
    {
        foreach (var item in Items["SpaceShips"])
        {
            if (item.Value.Equals(true))
            {
                return item.Key;
            }
        }
        return null;
    }
    public string GetEquippedEquipmentId()
    {
        foreach (var item in Items["Equipments"])
        {
            if (item.Value.Equals(true))
            {
                return item.Key;
            }
        }
        return null;
    }
}
