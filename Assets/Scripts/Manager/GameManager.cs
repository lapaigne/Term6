using UnityEngine;

public class GameManager : MonoBehaviour
{
    public IntVariable playerHP;
    public IntVariable playerMaxHP;
    public Inventory playerInventory;
    public InventoryManager inventoryManager;
    //public ItemData[] items;

    private void Awake()
    {
        playerInventory = GetComponent<Inventory>();
        inventoryManager = GetComponent<InventoryManager>();
    }
    private void Start()
    {
        inventoryManager.AddItems(playerInventory, "apple", 3);
    }
}
