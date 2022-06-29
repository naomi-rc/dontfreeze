using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DPSStatus", menuName = "StatusEffect/DPSStatus", order = 0)]
public class DPSStatus : StatusEffect
{
    [SerializeField]
    private float _duration = 0;

    [SerializeField]
    private float _value = 0;

    private IEnumerator EffectCoroutine;

    public float duration
    {
        get => _duration;
    }

    public float value
    {
        get => _value;
    }
}
