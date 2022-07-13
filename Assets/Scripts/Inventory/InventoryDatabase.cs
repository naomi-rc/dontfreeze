using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventoryEntry
{
    public int index;
    public InventoryItem item;
    public int count;
}

[CreateAssetMenu(fileName = "Database", menuName = "Inventory/Database")]
public class InventoryDatabase : ScriptableObject
{
    public UnityAction OnDatabaseChanged = delegate { };
    public List<InventoryEntry> entries = new List<InventoryEntry>();

    private int maxInventorySize = 50;

    public int Length
    {
        get { return maxInventorySize; }
    }

    private void OnEnable()
    {
        entries.Clear();
    }

    private int FindNextIndex()
    {
        for (int i = 0; i < maxInventorySize; i++)
        {
            if (GetEntry(i) == null)
            {
                return i;
            }
        }

        return -1;
    }

    public InventoryEntry GetEntry(int index)
    {
        return entries.FirstOrDefault(x => x.index == index);
    }

    public void AddItem(InventoryItem item)
    {
        if (entries.Count >= maxInventorySize)
            return;

        entries.Add(new InventoryEntry() { index = FindNextIndex(), item = item, count = 1 });
        OnDatabaseChanged.Invoke();
    }

    public void MoveItems(int fromIndex, int toIndex)
    {
        if (fromIndex == toIndex)
            return;

        var fromEntry = GetEntry(fromIndex);
        var toEntry = GetEntry(toIndex);

        if (fromEntry != null && toEntry == null)
        {
            fromEntry.index = toIndex;
        }
        else if (fromEntry.item == toEntry.item)
        {

            toEntry.count += fromEntry.count;
            entries.Remove(fromEntry);
        }
        else
        {
            fromEntry.index = toIndex;
            toEntry.index = fromIndex;
        }

        OnDatabaseChanged.Invoke();
    }
}

