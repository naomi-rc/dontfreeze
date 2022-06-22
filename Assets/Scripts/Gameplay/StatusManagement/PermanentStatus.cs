using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PermanentStatus", menuName = "StatusEffect/PermanentStatus", order = 0)]
public class PermanentStatus : StatusEffect
{
    [SerializeField]
    private float _value = 0;

    public float value
    {
        get => _value;
    }
}
