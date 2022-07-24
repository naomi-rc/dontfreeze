using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Clothing", menuName = "Inventory/Clothing")]
public class InventoryClothes : InventoryCraftable
{
    [Range(0, 0.9f)] public float coldResistance;

    [Range(0, 0.9f)] public float damageResistance;
}
