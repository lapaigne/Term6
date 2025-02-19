using System;
using UnityEngine;

[Serializable]
public struct CountItemPair
{
    public int count;
#nullable enable
    public ItemData? item;
#nullable disable
}

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public CountItemPair[] items;
    public void AddItems(ItemData newItem, int count)
    {
        int leftover = count;
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].item?.completeTag == newItem.completeTag)
            {
                int free = newItem.maxQuantity - items[i].count;
                int difference = Math.Min(free, count);
                leftover -= difference;
                items[i].count += difference;
                if (leftover == 0) { return; }
            }
        }

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].count == 0)
            {
                int difference = Math.Min(newItem.maxQuantity, leftover);
                items[i].count = difference;
                leftover -= difference;
                items[i].item = newItem;
                if (leftover == 0) { return; }
            }
        }

        if (leftover != 0) 
        {
            Debug.Log($"failed to add items --- {leftover}");
        }
    }

    private void Awake()
    {
        //Debug.Log(float.PositiveInfinity - 1);
    }

    private void OnValidate()
    {

    }
}
