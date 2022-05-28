using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Body Temperature")]
public class BodyTemperatureSO : ScriptableObject, ISerializationCallbackReceiver
{
  public float _initialTemperature;

  [NonSerialized] public float RuntimeTemperature;

  public float _initialRate;

  [NonSerialized] public float RuntimeRate;

  public void OnBeforeSerialize()
  {
  }

  public void OnAfterDeserialize()
  {
    RuntimeTemperature = _initialTemperature;
    RuntimeRate = _initialRate;
  }

  public void UpdateTemperature(float other)
  {
    if (CompareEqual(other))
    {
      return;
    }

    else if (CompareWithinRate(other))
    {
      RuntimeTemperature = other;
    }

    else if (CompareGreater(other))
    {
      RuntimeTemperature -= RuntimeRate;
    }

    else if (CompareLesser(other))
    {
      RuntimeTemperature += RuntimeRate;
    }
  }

  public bool CompareGreater(float other)
  {
    return RuntimeTemperature > other;
  }

  public bool CompareLesser(float other)
  {
    return RuntimeTemperature < other;
  }

  public bool CompareEqual(float other)
  {
    return RuntimeTemperature == other;
  }

  public bool CompareWithinRate(float other)
  {
    return (RuntimeTemperature > other && RuntimeTemperature - RuntimeRate <= other)
     || (RuntimeTemperature < other && RuntimeTemperature + RuntimeRate >= other);
  }


}
