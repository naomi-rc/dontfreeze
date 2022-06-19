using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryItemType
{
    Weapon,
    Food,
    Resource,
}

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string title;
    public Texture2D icon;
    public InventoryItemType type;
    public string description;
}