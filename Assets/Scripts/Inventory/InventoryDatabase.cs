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
    [SerializeField]
    private IntEventChannel restoreHealthEvent;

    [SerializeField]
    private InventoryItemEventChannel onWeaponEquipEvent;

    [SerializeField]
    private InventoryItemEventChannel onClothesEquipEvent;

    public UnityAction OnDatabaseChanged = delegate { };
    public List<InventoryEntry> entries = new List<InventoryEntry>();
    public InventoryWeapon currentWeapon = null;
    public InventoryClothes currentClothes = null;

    private int maxInventorySize = 50;

    public int Length
    {
        get { return maxInventorySize; }
    }

    private void OnEnable()
    {
        entries.Clear();
        currentWeapon = null;
        currentClothes = null;
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

    public void UseItem(int index)
    {
        var entry = GetEntry(index);
        if (entry == null) { return; }

        switch (entry.item)
        {
            case InventoryConsumable consumable:
                restoreHealthEvent.Raise(consumable.healthResorationValue);
                RemoveItem(index);
                break;
            case InventoryWeapon weapon:
                currentWeapon = currentWeapon != weapon ? weapon : null;
                onWeaponEquipEvent.Raise(currentWeapon);
                break;
            case InventoryClothes clothes:
                currentClothes = currentClothes != clothes ? clothes : null;
                break;
            default:
                break;
        }

        OnDatabaseChanged.Invoke();
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

    public void RemoveItem(int index)
    {
        var entry = GetEntry(index);
        if (entry == null) { return; }

        if (entry.count > 1)
        {
            entry.count--;
        }
        else
        {
            entries.RemoveAll(x => x.index == index);
        }

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

    public void UpgradeEntry(InventoryEntry entry)
    {
        if (entry is null || !IsUpgradable(entry.item))
        {
            Debug.Log("not upgradable");
            return;
        }


        foreach (var ingredient in (entry.item as InventoryUpgradable).Ingredients)
        {
            int amount = ingredient.Amount;
            while (0 < amount)
            {
                RemoveItem(entries.FirstOrDefault(x => x.item == ingredient.Item).index);
                amount--;
            }
        }

        if (entry.item is InventoryClothes)
        {
            var upgrade = (entry.item as InventoryClothes).Upgrade;

            if (entry.item == currentClothes)
                currentClothes = upgrade;

            entry.item = upgrade;
            OnDatabaseChanged.Invoke();
        }
    }

    public bool IsUpgradable(InventoryItem item)
    {
        if (item is null || item is not InventoryUpgradable)
            return false;

        foreach (var ingredient in (item as InventoryUpgradable).Ingredients)
        {
            if (entries.Where(x => x.item == ingredient.Item).Select(x => x.count).Sum() < ingredient.Amount)
                return false;
        }

        return true;
    }

    public bool IsEquipable(InventoryItem item)
    {
        return (item is InventoryWeapon && currentWeapon == null
        || item is InventoryClothes && currentClothes == null);
    }

    public bool IsUnequipable(InventoryItem item)
    {
        return (item is InventoryWeapon && currentWeapon != null
        || item is InventoryClothes && currentClothes != null);
    }
}
