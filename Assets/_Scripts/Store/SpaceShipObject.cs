using UnityEngine;
[CreateAssetMenu(fileName = "SpaceShipObject", menuName = "ScriptableObjects/StoreItem/SpaceShip")]
public class SpaceShipObject : StoreItemObject
{
    private void Awake()
    {
        type = StoreItemType.SpaceShip;
    }
}
