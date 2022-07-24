using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryCraftable : InventoryItem
{
    [SerializeField] private List<CraftingIngredient> ingredients;
}

[System.Serializable]
public class CraftingIngredient
{
    [SerializeField] private InventoryItem item;
    [SerializeField] private int amount;
}
