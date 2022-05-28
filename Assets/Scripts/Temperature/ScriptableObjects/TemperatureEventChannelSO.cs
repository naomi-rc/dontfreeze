using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TemperatureEventChannelSO", menuName = "Channels/TemperatureEventChannelSO", order = 0)]
public class TemperatureEventChannelSO : ScriptableObject
{
    public UnityAction<TemperatureData, float> OnTemperatureChangeRequested;

    public void Raise(TemperatureData data, float value)
    {
        OnTemperatureChangeRequested.Invoke(data, value);
    }
}
