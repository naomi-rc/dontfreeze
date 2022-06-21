using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thermometer : MonoBehaviour
{
    [SerializeField]
    private FloatVariable temperature;

    [SerializeField]
    private FloatReference defaultTemperature;

    [SerializeField]
    private FloatReference coldTemperature;

    [SerializeField]
    private BoolEventChannel onColdEvent = default;

    private bool hasColdStatus = false;

    private void Awake()
    {
        temperature.OnValueChanged += ManageStatus;
    }

    private void ManageStatus(float value)
    {
        if (temperatureIsCold(value) && !hasColdStatus
        || !temperatureIsCold(value) && hasColdStatus)
        {
            hasColdStatus = !hasColdStatus;
            onColdEvent.Raise(hasColdStatus);
        }
    }

    public bool temperatureIsCold(float value)
    {
        return value <= coldTemperature.value;
    }

    public void Change(float intensity)
    {
        temperature.value += intensity;
    }

    public void ChangeTowards(float target, float intensity)
    {
        int direction = target.CompareTo(temperature.value);

        float result = temperature.value + (intensity * direction);

        if (result > target && direction > 0 || result < target && direction < 0)
        {
            result = target;
        }

        temperature.value = result;

        Debug.Log("Temperature is " + temperature.value);
    }

    public void Reset()
    {
        temperature.value = defaultTemperature.value;
    }
}
