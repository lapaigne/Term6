using System;
using UnityEngine;

[Serializable]
public struct AmountItemPair
{
    public int amount;
#nullable enable
    public ItemData? item;
#nullable disable
}

public class Inventory : MonoBehaviour
{
    /*
    byte amount, 
    ItemData? 
    */

    public int defaultSize; // perhaps, use items.Length instead?
    [SerializeField]
    public AmountItemPair[] items;
    public void AddItems(ItemData newItem, int amount)
    {
        int leftover = amount;
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].item?.completeTag == newItem.completeTag)
            {
                int free = newItem.maxQuantity - items[i].amount;
                int difference = Math.Min(free, amount);
                leftover -= difference;
                items[i].amount += difference;
                if (leftover == 0) { return; }
            }
        }

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].amount == 0)
            {
                int difference = Math.Min(newItem.maxQuantity, leftover);
                items[i].amount = difference;
                leftover -= difference;
                items[i].item = newItem;
                if (leftover == 0) { return; }
            }
        }

        if (leftover != 0) 
        {
            Debug.Log("failed to add items");
        }
    }

    private void Awake()
    {
        items = new AmountItemPair[defaultSize];
    }

    private void OnValidate()
    {
        
    }
}
