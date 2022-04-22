using UnityEngine;
[CreateAssetMenu(fileName = "SpaceShipObject", menuName = "ScriptableObjects/StoreItem/SpaceShip")]
public class SpaceShipObject : StoreItemObject
{
    public GameObject lobbyPrefab;
    public GameObject journeyPrefab;
    private void Awake()
    {
        type = StoreItemType.SpaceShip;
    }
}
