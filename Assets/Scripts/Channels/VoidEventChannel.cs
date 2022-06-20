using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "VoidEventChannel", menuName = "Channels/VoidEventChannel", order = 0)]
public class VoidEventChannel : ScriptableObject
{
    public UnityAction OnEventRaised;

    public void Raise()
    {
        OnEventRaised?.Invoke();
    }
}
