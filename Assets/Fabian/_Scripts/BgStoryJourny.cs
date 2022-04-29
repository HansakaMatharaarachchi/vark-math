using _Scripts;
using UnityEngine;

public class BgStoryJourny : MonoBehaviour
{
    [SerializeField] private Transform playerContainer;
    [SerializeField] private Transform spaceShipContainer;
    
    [SerializeField] private GameObject destinationPlanet;
    [SerializeField] private Material[] destinationPlanetMats;

    private void Awake()
    {
        DisplayEquippedItems();
        ChangeDestinationPlanet();
    }
    private void DisplayEquippedItems()
    {
        // displays the equipped spaceShip
        if (spaceShipContainer.childCount > 0)
            Destroy(spaceShipContainer.GetChild(0).gameObject);
        GameObject spaceShip = ((SpaceShipObject)GameManager.Instance.Store.GetItemBuyId(GameManager.Instance.Player.Inventory.GetEquippedSpaceShipId())).journeyPrefab;
        Instantiate(spaceShip, spaceShipContainer);
        
        // displays the equipped costume
        if (playerContainer.childCount > 0)
            Destroy(playerContainer.GetChild(0).gameObject);
        GameObject character = ((CostumeObject)GameManager.Instance.Store.GetItemBuyId(GameManager.Instance.Player.Inventory.GetEquippedCostumeId())).journeyPrefab;
        Instantiate(character, playerContainer);
    }
    
    private void ChangeDestinationPlanet()
    {
        destinationPlanet.GetComponent<MeshRenderer>().material = destinationPlanetMats[GameManager.Instance.CurrentLevel - 1];
    }
}
