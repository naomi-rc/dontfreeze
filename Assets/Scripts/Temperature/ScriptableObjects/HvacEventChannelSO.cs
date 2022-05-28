using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "HvacEventChannelSO", menuName = "Channels/HvacEventChannelSO", order = 0)]
public class HvacEventChannelSO : ScriptableObject
{
    public UnityAction<HvacSystem, Receptor> OnHvacSystemEntered;
    public UnityAction<HvacSystem, Receptor> OnHvacSystemExited;

    public void RaiseHvacSystemEntered(HvacSystem system, Receptor receptor)
    {
        if (OnHvacSystemEntered is not null)
        {
            OnHvacSystemEntered.Invoke(system, receptor);
        }
        else
        {
            Debug.LogWarning("Cannot invoke \"OnHvacSystemEntered\" since no manager is listening to it.", this);
        }
    }

    public void RaiseHvacSystemExited(HvacSystem system, Receptor receptor)
    {
        if (OnHvacSystemExited is not null)
        {
            OnHvacSystemExited.Invoke(system, receptor);
        }
        else
        {
            Debug.LogWarning("Cannot invoke \"OnHvacSystemExited\" since no manager is listening to it.", this);
        }
    }
}
