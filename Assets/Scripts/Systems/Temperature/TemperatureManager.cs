using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureManager : MonoBehaviour
{
    [SerializeField] private TemperatureEventChannel _temperatureEventChannelSO;

    private void Awake()
    {
        _temperatureEventChannelSO.OnTemperatureChangeRequested += UpdateTemperature;
    }

    private void UpdateTemperature(TemperatureSO data, float value)
    {
        data.temperature += value;
        Debug.LogFormat("The temperature of {0} is now {1}", data.ToString(), data.temperature);
    }
}
