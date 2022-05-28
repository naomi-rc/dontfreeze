using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class HvacSystem : MonoBehaviour
{
  [SerializeField] private float _temperature = 0f;

  [SerializeField] private float _interval = 1f;

  private List<HvacListener> _listeners = new List<HvacListener>();

  private IEnumerator coroutine;

  private void Start()
  {
    coroutine = EventRoutine();
  }

  private void OnTriggerEnter(Collider other)
  {
    var listener = other.GetComponent<HvacListener>();

    if (listener is not null)
    {
      RegisterListener(listener);
    }
  }

  private void OnTriggerExit(Collider other)
  {
    var listener = other.GetComponent<HvacListener>();

    if (listener is not null)
    {
      UnregisterListener(listener);
    }
  }

  // Refactor into unity event
  public void Raise()
  {
    foreach (HvacListener listener in _listeners)
    {
      listener.HvacUpdate(_temperature);
    }
  }

  public void RegisterListener(HvacListener listener)
  {
    if (_listeners.Count == 0)
    {
      StartCoroutine(coroutine);
    }

    _listeners.Add(listener);
  }
  public void UnregisterListener(HvacListener listener)
  {
    _listeners.Remove(listener);

    if (_listeners.Count == 0)
    {
      StopCoroutine(coroutine);
    }
  }

  public IEnumerator EventRoutine()
  {
    for (; ; )
    {
      Raise();
      yield return new WaitForSeconds(_interval);
    }
  }
}