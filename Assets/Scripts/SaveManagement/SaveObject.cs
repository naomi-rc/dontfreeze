using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveObject
{
    public List<InventoryEntry> inventoryEntries = new List<InventoryEntry>();

    public SceneObject playerLocation = default;

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void FromJsonOverwrite(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }
}
