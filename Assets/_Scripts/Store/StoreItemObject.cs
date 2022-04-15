using System;
using UnityEngine;

public enum StoreItemType
{
    Costume,
    SpaceShip,
    Equipment
}

public abstract class StoreItemObject : ScriptableObject
{
    public string id;
    public new string name;
    public StoreItemType type;
    public int price;
    public GameObject prefab;
    public int levelToBeUnlocked;
}