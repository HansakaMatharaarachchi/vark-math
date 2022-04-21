using UnityEngine;
[CreateAssetMenu(fileName = "SpaceShipObject", menuName = "ScriptableObjects/StoreItem/SpaceShip")]
public class SpaceShipObject : StoreItemObject
{
    public GameObject lobbyPrefab;
    private void Awake()
    {
        type = StoreItemType.SpaceShip;
    }
}
