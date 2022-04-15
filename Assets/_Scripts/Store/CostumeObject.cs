using UnityEngine;
[CreateAssetMenu(fileName = "CostumeObject", menuName = "ScriptableObjects/StoreItem/Costume")]
public class CostumeObject : StoreItemObject
{
    public GameObject lobbyPrefab;
    private void Awake()
    {
        type = StoreItemType.Costume;
    }
}
