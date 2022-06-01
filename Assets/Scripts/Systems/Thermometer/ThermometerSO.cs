using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ThermometerSO", menuName = "Data/ThermometerSO", order = 0)]
public class ThermometerSO : ScriptableObject, ISerializationCallbackReceiver
{
    public float _initialTemperature;

    [NonSerialized]
    private float temperature;

    public float max; // todo: Implement max temperature logic

    public float min; // todo: Implement min temperature logic


    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        temperature = _initialTemperature;
    }

    public void Increment(float value)
    {
        temperature += value;
    }
}
