using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Clothing", menuName = "Inventory/Clothing")]
public class InventoryClothes : InventoryUpgradable
{
    [Range(0, 0.9f)] public float coldResistance;

    [Range(0, 0.9f)] public float damageResistance;

    [SerializeField] private InventoryClothes upgrade;
    public InventoryClothes Upgrade
    {
        get => (upgrade is not null) ? upgrade : this;
    }
}
