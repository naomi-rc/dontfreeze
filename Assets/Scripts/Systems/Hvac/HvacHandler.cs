using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HvacHandler : MonoBehaviour
{
    public ThermometerSO thermometer = default;


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

        coroutine = HvacCoroutine();
        StartCoroutine(coroutine);
    }

    private void Update()
    {
        // Update the temperature
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
                thermometer.Increment(hvac.temperatureChangeValue);

                // For demo purposes
                Debug.Log("Update using " + hvac.name, this);
            }

            yield return new WaitForSeconds(interval);
        }
    }
}
