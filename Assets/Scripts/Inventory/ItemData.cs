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
    public string globalSpaceName = "default";
    public string globalTagName;
    public string completeTag;
    
    public Sprite image;

    public int variation;
    public int maxQuantity;

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
        
        if (globalTagName.Length > 0)
        {
            completeTag = $"{globalSpaceName}:{globalTagName}";
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