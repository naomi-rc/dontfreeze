using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Hvac : MonoBehaviour
{
    [SerializeField] private HvacEventChannel _hvacEventChannelSO;

    public float temperatureChangeValue = 0f;

    private void OnTriggerEnter(Collider other)
    {
        var receptor = other.GetComponent<Receptor>();

        if (receptor is not null)
        {
            _hvacEventChannelSO.RaiseHvacEntered(this, receptor);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var receptor = other.GetComponent<Receptor>();

        if (receptor is not null)
        {
            _hvacEventChannelSO.RaiseHvacExited(this, receptor);
        }
    }
}