using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "StatusEventChannel", menuName = "Channels/StatusEventChannel", order = 0)]
public class StatusEventChannel : ScriptableObject
{
    public UnityAction<StatusEffect> OnStatusAppliedEvent;
    public UnityAction<StatusEffect> OnStatuRemovedEvent;

    public void RaiseStatusApplied(StatusEffect value)
    {
        OnStatusAppliedEvent?.Invoke(value);
    }

    public void RaiseStatusRemoved(StatusEffect value)
    {
        OnStatuRemovedEvent?.Invoke(value);
    }

}
