using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class HvacSystem : MonoBehaviour
{
    [SerializeField] private HvacEventChannelSO _hvacEventChannelSO;

    public float temperatureChangeValue = 0f;

    private void OnTriggerEnter(Collider other)
    {
        var receptor = other.GetComponent<Receptor>();

        if (receptor is not null)
        {
            _hvacEventChannelSO.RaiseHvacSystemEntered(this, receptor);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var receptor = other.GetComponent<Receptor>();

        if (receptor is not null)
        {
            _hvacEventChannelSO.RaiseHvacSystemExited(this, receptor);
        }
    }
}