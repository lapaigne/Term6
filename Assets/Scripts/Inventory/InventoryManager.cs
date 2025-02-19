using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public ItemData[] items;
    private Dictionary<string, ItemData> _lookup;
    public void AddItems(Inventory inventory, string tag, int count)
    {
        string _tag = tag.Contains("default:") ? tag : $"default:{tag}";

        if (_lookup.TryGetValue(_tag, out ItemData item))
        {
            inventory.AddItems(item, count);
        }
    }

    private void Awake()
    {
        _lookup = new Dictionary<string, ItemData>();
        for (int i = 0; i < items.Length; i++)
        {
            if (!_lookup.TryAdd(items[i].completeTag, items[i]))
            {
                Debug.Log($"Failed to add '{items[i].completeTag}' to lookup dictionary");
            }
        }
    }
    private void OnValidate()
    {
        items = items.ToHashSet().ToArray();
    }
}
