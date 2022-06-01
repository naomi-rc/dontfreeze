using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ThermometerSO", menuName = "Data/ThermometerSO", order = 0)]
public class ThermometerSO : ScriptableObject, ISerializationCallbackReceiver
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
