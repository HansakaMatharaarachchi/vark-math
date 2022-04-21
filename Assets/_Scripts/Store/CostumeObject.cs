using UnityEngine;
[CreateAssetMenu(fileName = "CostumeObject", menuName = "ScriptableObjects/StoreItem/Costume")]
public class CostumeObject : StoreItemObject
{
    public GameObject lobbyPrefab;
    public GameObject journeyPrefab;
    private void Awake()
    {
        type = StoreItemType.Costume;
    }
}
