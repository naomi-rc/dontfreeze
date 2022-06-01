using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HvacComparer : Comparer<HvacBehavior>
{
    public override int Compare(HvacBehavior x, HvacBehavior y)
    {
        return x.priority.CompareTo(y.priority);
    }
}

[RequireComponent(typeof(Collider))]
public class HvacBehavior : MonoBehaviour
{
    private HvacEventChannel hvacEventChannel;

    public float temperatureChangeValue = 1f;

    [Range(0, 10)]
    public int priority = 0;

    // hvac config

    private void Awake()
    {
        var collider = GetComponent<Collider>();

        collider.isTrigger = true; // Enables OnTrigger events

        hvacEventChannel = ScriptableObject.CreateInstance<HvacEventChannel>();
    }

    private void OnDisable()
    {
        // Raise Exit
    }

    private void OnDestroy()
    {
        hvacEventChannel.RaiseHvacDestroyed(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        var handler = other.GetComponent<HvacHandler>();

        if (handler is not null)
        {
            handler.Add(this);

            hvacEventChannel.OnHvacDestroyed += handler.Remove;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var handler = other.GetComponent<HvacHandler>();

        if (handler is not null)
        {
            handler.Remove(this);

            hvacEventChannel.OnHvacDestroyed -= handler.Remove;
        }
    }
}