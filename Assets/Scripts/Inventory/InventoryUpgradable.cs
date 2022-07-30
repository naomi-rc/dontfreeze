using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryUpgradable : InventoryItem
{
    [SerializeField] private List<CraftingIngredient> ingredients;
    public List<CraftingIngredient> Ingredients
    {
        get => ingredients;
    }
}

[System.Serializable]
public class CraftingIngredient
{
    [SerializeField] private InventoryItem item;
    public InventoryItem Item
    {
        get => item;
    }

    [SerializeField] private int amount;
    public int Amount
    {
        get => amount;
    }
}
