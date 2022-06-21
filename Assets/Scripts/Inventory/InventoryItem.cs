using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string title;
    public Texture2D icon;
    public string description;
}