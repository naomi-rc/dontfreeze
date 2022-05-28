using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HvacListener : MonoBehaviour
{
  [SerializeField] private BodyTemperatureSO bodyTemperatureSO;

  public void HvacUpdate(float hvacTemperature)
  {
    bodyTemperatureSO.UpdateTemperature(hvacTemperature);
    Debug.Log("HvacListener: Body temperature is now " + bodyTemperatureSO.RuntimeTemperature);
  }
}
