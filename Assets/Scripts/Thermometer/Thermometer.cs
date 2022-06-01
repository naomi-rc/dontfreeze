using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thermometer : MonoBehaviour
{
    [SerializeField]
    private FloatVariable temperature;

    public FloatReference temperatureMax;

    public FloatReference temperatureMin;

    public void UpdateTemperature(float target, float intensity)
    {
        int direction = target.CompareTo(temperature.value);

        float result = temperature.value + (intensity * direction);

        if (result > temperatureMax.value) temperature.value = temperatureMax.value;

        else if (result < temperatureMin.value) temperature.value = temperatureMin.value;

        else temperature.value = result;

        Debug.Log("Temperature is " + temperature.value);
    }
}
