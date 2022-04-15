using UnityEngine;
[CreateAssetMenu(fileName = "EquipmentObject", menuName = "ScriptableObjects/StoreItem/Equipment")]
public class EquepmentObject : StoreItemObject
{
    private void Awake()
    {
        type = StoreItemType.Equipment;
    }
}
