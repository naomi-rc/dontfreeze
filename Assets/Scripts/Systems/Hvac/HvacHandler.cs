using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HvacHandler : MonoBehaviour
{
    public ThermometerSO thermometer = default;

    private SortedSet<HvacBehavior> hvacList; // todo: Create ordered list

    [SerializeField]
    private float interval = 1f;

    private IEnumerator coroutine;

    private void Awake()
    {
        hvacList = new SortedSet<HvacBehavior>(new HvacComparer());

        coroutine = HvacCoroutine();
        StartCoroutine(coroutine);
    }

    private void Update()
    {
        // Update the temperature
    }

    public void Add(HvacBehavior hvac)
    {
        hvacList.Add(hvac);
    }

    public void Remove(HvacBehavior hvac)
    {
        hvacList.Remove(hvac);
    }

    IEnumerator HvacCoroutine()
    {
        for (; ; )
        {
            var firstHvac = hvacList.Min;

            if (hvacList.Count > 0 && firstHvac is not null)
            {
                thermometer.temperature += firstHvac.temperatureChangeValue;

                if (firstHvac.description.Length > 0)
                {
                    Debug.Log("Update using " + firstHvac.description, this);
                }
            }

            yield return new WaitForSeconds(interval);
        }
    }
}
