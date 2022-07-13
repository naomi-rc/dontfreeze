using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootTable", menuName = "Inventory/Loot Table")]
public class LootTable : ScriptableObject
{
    [SerializeField]
    private List<LootTableEntry> entries = new List<LootTableEntry>();

    public void DropLoot(Vector3 position)
    {
        foreach (var entry in entries)
        {
            foreach (var item in entry.items)
            {
                float roll = Random.value;
                if (roll <= entry.dropRate)
                {
                    float randAngle = Random.value * Mathf.PI * 2;
                    Vector3 direction = Mathf.Cos(randAngle) * Vector3.forward + Mathf.Sin(randAngle) * Vector3.right;
                    GameObject.Instantiate(item, position + direction, Quaternion.identity);
                }
            }
        }
    }
}
