using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FloatEventChannel", menuName = "Channels/FloatEventChannel", order = 0)]
public class FloatEventChannel : ScriptableObject
{
    public UnityAction<float> OnEventRaised;

    public void Raise(float value)
    {
        OnEventRaised?.Invoke(value);
    }
}
