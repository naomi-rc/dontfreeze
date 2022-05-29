using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "HvacEventChannel", menuName = "Channels/HvacEventChannel", order = 0)]
public class HvacEventChannel : ScriptableObject
{
    public UnityAction<Hvac, Receptor> OnHvacEntered;
    public UnityAction<Hvac, Receptor> OnHvacExited;

    public void RaiseHvacEntered(Hvac system, Receptor receptor)
    {
        if (OnHvacEntered is not null)
        {
            OnHvacEntered.Invoke(system, receptor);
        }
        else
        {
            Debug.LogWarning("Cannot invoke \"OnHvacEntered\" since no manager is listening to it.", this);
        }
    }

    public void RaiseHvacExited(Hvac system, Receptor receptor)
    {
        if (OnHvacExited is not null)
        {
            OnHvacExited.Invoke(system, receptor);
        }
        else
        {
            Debug.LogWarning("Cannot invoke \"OnHvacExited\" since no manager is listening to it.", this);
        }
    }
}
