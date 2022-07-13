using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "IntEventChannel", menuName = "Channels/IntEventChannel", order = 0)]
public class IntEventChannel : ScriptableObject
{
    public UnityAction<int> OnEventRaised;

    public void Raise(int value)
    {
        OnEventRaised?.Invoke(value);
    }
}