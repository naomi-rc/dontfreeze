using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Thermometer))]
public class HvacHandler : MonoBehaviour
{
    private Thermometer thermometer = default;

    /* Todo
    * Use hashcodes as key
    * i.e: Set<(Hashcode, Hvac config)>
    */
    private SortedSet<HvacBehavior> hvacSet;

    // Implement a config for the handler

    [SerializeField]
    private float interval = 1f;

    private IEnumerator coroutine;

    private void Awake()
    {
        hvacSet = new SortedSet<HvacBehavior>(new HvacComparer());

        thermometer = GetComponent<Thermometer>();

        coroutine = HvacCoroutine();
        StartCoroutine(coroutine);
    }

    public void Add(HvacBehavior hvac)
    {
        hvacSet.Add(hvac);
    }

    public void Remove(HvacBehavior hvac)
    {
        hvacSet.Remove(hvac);
    }

    IEnumerator HvacCoroutine()
    {
        for (; ; )
        {
            HvacBehavior hvac;

            if (hvacSet.TryGetValue(hvacSet.Min, out hvac))
            {
                thermometer.UpdateTemperature(hvac.temperature.value, hvac.intensity.value);

                // For demo purposes
                Debug.Log("Update using " + hvac.name, this);
            }

            yield return new WaitForSeconds(interval);
        }
    }
}
