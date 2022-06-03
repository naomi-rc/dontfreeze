using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrop : MonoBehaviour
{
    [SerializeField]
    private InventoryDatabase playerInventory;

    [SerializeField]
    private List<InventoryItem> lootTable;

    void Start()
    {
        int randomAmount = Random.Range(1, 5);

        for (int i = 0; i < randomAmount; i++)
        {
            int randomIndex = Random.Range(0, lootTable.Count);
            InventoryItem loot = lootTable[randomIndex];
            playerInventory.AddItem(loot);
        }
    }
}
