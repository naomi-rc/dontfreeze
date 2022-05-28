using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HvacManager : MonoBehaviour
{
    [SerializeField] private TemperatureEventChannelSO _temperatureEventChannelSO;
    [SerializeField] private HvacEventChannelSO _hvacEventChannelSO;

    [SerializeField] private float _interval = 1f;

    private Dictionary<HvacSystem, List<Receptor>> _hvacPool;

    private IEnumerator _hvacCoroutine;

    private void Awake()
    {
        _hvacPool = new Dictionary<HvacSystem, List<Receptor>>();

        _hvacEventChannelSO.OnHvacSystemEntered += AddListener;
        _hvacEventChannelSO.OnHvacSystemExited += RemoveListener;

        _hvacCoroutine = HvacCoroutine();
        StartCoroutine(_hvacCoroutine);
    }

    private void AddListener(HvacSystem system, Receptor receptor)
    {
        // hvacPool should be filled at launch.
        // This function should be refactored => High risk of bugs.
        if (!_hvacPool.ContainsKey(system))
        {
            _hvacPool.Add(system, new List<Receptor>() { receptor });
        }
        else if (!_hvacPool[system].Contains(receptor))
        {
            _hvacPool[system].Add(receptor);
        }
        else
        {
            Debug.Log("A receptor was not added since it is already attached to a system.");
        }
    }

    private void RemoveListener(HvacSystem system, Receptor receptor)
    {
        // hvacPool should be filled at launch
        if (_hvacPool.ContainsKey(system))
        {
            _hvacPool[system].Remove(receptor);
        }
        else
        {
            Debug.Log("A system could not be found when removing a receptor.");
        }
    }

    IEnumerator HvacCoroutine()
    {
        for (; ; )
        {
            foreach (KeyValuePair<HvacSystem, List<Receptor>> kv in _hvacPool)
            {
                var system = kv.Key;

                foreach (Receptor receptor in kv.Value)
                {
                    _temperatureEventChannelSO.Raise(receptor.temperatureData, system.temperatureChangeValue);
                }
            }
            yield return new WaitForSeconds(_interval);
        }
    }
}
