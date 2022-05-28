using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TemperatureData", menuName = "Data/TemperatureData", order = 0)]
public class TemperatureData : ScriptableObject, ISerializationCallbackReceiver
{
    public float _initialTemperature;

    [NonSerialized] public float temperature;

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        temperature = _initialTemperature;
    }
}
