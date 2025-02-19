using Unity.Collections;
using UnityEngine;

public enum ItemCategory
{
    None = -1,
    Food,
    Ammo,
    Weapon,
    Melee,
    Material
}

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    private const int UNIVERSAL_MAX_STACK_SIZE = 32;

    public string globalSpace = "default";
    public string shortTag;
    public string completeTag;
    
    public Sprite image;
    public int variation;
    public int maxQuantity = 1;

    // use category for sorting, potentially combine it with tags
    // define possible behaviour and actions
    public ItemCategory category; 
    
    // use tags for generalized crafting recipes
    public string[] tags;

    /* TODO: figure out localization methods */
    public string displayName;
    public string description;

    private void OnValidate()
    {
        if (maxQuantity < 1) { maxQuantity = 1; }

        if (maxQuantity > UNIVERSAL_MAX_STACK_SIZE) { maxQuantity = UNIVERSAL_MAX_STACK_SIZE; }

        if (variation < 0) { variation = 0; }

        if (shortTag.Length > 0)
        {
            completeTag = $"{globalSpace}:{shortTag}";
            if (variation > 0)
            {
                completeTag += $":{variation}";
            }
        }
        else
        {
            completeTag = string.Empty;
        }
    }
}