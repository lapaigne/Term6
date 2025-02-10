using UnityEngine;

public class GameManager : MonoBehaviour
{
    public IntVariable playerHP;
    public IntVariable playerMaxHP;
    public Inventory inventory;
    [SerializeField]
    public ItemData[] items;

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }
    private void Start()
    {
        // passes basic tests

        inventory.AddItems(items[0], 9);
    }
}
