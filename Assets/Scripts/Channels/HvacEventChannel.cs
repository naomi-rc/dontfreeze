using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "HvacEventChannel", menuName = "Channels/HvacEventChannel", order = 0)]
public class HvacEventChannel : ScriptableObject
{
    public UnityAction<HvacBehavior> OnHvacDestroyed;

    public void RaiseHvacDestroyed(HvacBehavior hvac)
    {

        OnHvacDestroyed?.Invoke(hvac);

    }
}
