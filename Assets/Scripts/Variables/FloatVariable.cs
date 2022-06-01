using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatVariable", menuName = "Variable/FloatVariable", order = 0)]
public class FloatVariable : ScriptableObject
{
    public float value;
}

[Serializable]
public class FloatReference
{
    public bool useConstant = true;

    public float constant;

    public FloatVariable variable;

    public float value => (useConstant) ? constant : variable.value;
}