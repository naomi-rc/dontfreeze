using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BoolEventChannel", menuName = "Channels/BoolEventChannel", order = 0)]
public class BoolEventChannel : ScriptableObject
{
    public UnityAction<bool> OnEventRaised;

    public void Raise(bool value)
    {
        OnEventRaised?.Invoke(value);
    }
}
