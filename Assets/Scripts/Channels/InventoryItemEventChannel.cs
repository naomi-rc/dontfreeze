using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InventoryItemEventChannel", menuName = "Channels/InventoryItemEventChannel", order = 0)]
public class InventoryItemEventChannel : ScriptableObject
{
    public UnityAction<InventoryItem> OnEventRaised;

    public void Raise(InventoryItem value)
    {
        OnEventRaised?.Invoke(value);
    }
}