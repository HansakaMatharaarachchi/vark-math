using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace _Scripts
{
    public class Store
    {
        public Dictionary<string, StoreItemObject[]> Items { get; } = new Dictionary<string, StoreItemObject[]>();

        public Store()
        {
            //fills the store items using the scriptable objects
            Items.Add("Costumes", Resources.LoadAll<StoreItemObject>("ScriptableObjects/Store/Costumes"));
            Items.Add("SpaceShips", Resources.LoadAll<StoreItemObject>("ScriptableObjects/Store/SpaceShips"));
            Items.Add("Equipments", Resources.LoadAll<StoreItemObject>("ScriptableObjects/Store/Equipments"));
        }


        public StoreItemObject GetItemBuyId(string id)
        {
            switch (id.Substring(0, 1))
            {
                case "C":
                    foreach (var storeItemObject in Items["Costumes"])
                    {
                        if (storeItemObject.id == id)
                        {
                            return storeItemObject;
                        }
                    }
                    break;
                case "S":
                    foreach (var storeItemObject in Items["SpaceShips"])
                    {
                        if (storeItemObject.id == id)
                        {
                            return storeItemObject;
                        }
                    }
                    break;
                case "E":
                    foreach (var storeItemObject in Items["Equipments"])
                    {
                        if (storeItemObject.id == id)
                        {
                            return storeItemObject;
                        }
                    }
                    break;
            }
            return null;
        }
    }
}