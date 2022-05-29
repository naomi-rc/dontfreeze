using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HvacManager : MonoBehaviour
{
    [SerializeField] private TemperatureEventChannel _temperatureEventChannel;

    [SerializeField] private HvacEventChannel _hvacEventChannel;

    [SerializeField] private float _interval = 1f;

    private Dictionary<Hvac, List<Receptor>> _hvacPool;

    private IEnumerator _hvacCoroutine;

    private void Awake()
    {
        _hvacPool = new Dictionary<Hvac, List<Receptor>>();

        _hvacEventChannel.OnHvacEntered += AddListener;
        _hvacEventChannel.OnHvacExited += RemoveListener;

        _hvacCoroutine = HvacCoroutine();
        StartCoroutine(_hvacCoroutine);
    }

    private void AddListener(Hvac system, Receptor receptor)
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

    private void RemoveListener(Hvac system, Receptor receptor)
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
            foreach (KeyValuePair<Hvac, List<Receptor>> kv in _hvacPool)
            {
                var hvac = kv.Key;
                var receptors = kv.Value;

                foreach (Receptor receptor in receptors)
                {
                    _temperatureEventChannel.Raise(receptor.temperatureSO, hvac.temperatureChangeValue);
                }
            }
            yield return new WaitForSeconds(_interval);
        }
    }
}
