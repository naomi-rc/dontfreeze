using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TemperatureSO", menuName = "Data/Temperature", order = 0)]
public class TemperatureSO : ScriptableObject, ISerializationCallbackReceiver
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
