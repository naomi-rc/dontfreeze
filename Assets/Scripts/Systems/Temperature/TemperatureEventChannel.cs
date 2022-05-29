using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TemperatureEventChannel", menuName = "Channels/TemperatureEventChannel", order = 0)]
public class TemperatureEventChannel : ScriptableObject
{
    public UnityAction<TemperatureSO, float> OnTemperatureChangeRequested;

    public void Raise(TemperatureSO data, float value)
    {
        OnTemperatureChangeRequested.Invoke(data, value);
    }
}
