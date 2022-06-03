using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FloatVariable", menuName = "Variable/FloatVariable", order = 0)]
public class FloatVariable : ScriptableObject
{
    public UnityAction<float> OnValueChanged;

    [SerializeField]
    private float _value;

    public float value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        }
    }
}

[Serializable]
public class FloatReference
{
    [SerializeField]
    private bool useConstant = true;

    [SerializeField]
    private float constant;

    [SerializeField]
    private FloatVariable variable;

    public float value => (useConstant) ? constant : variable.value;
}