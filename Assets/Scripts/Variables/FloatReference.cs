using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "FloatReference", menuName = "References/FloatReference", order = 0)]
public class FloatReference : ScriptableObject
{
    public bool UseConstant;

    [SerializeField]
    private float Literal;
    public FloatVariable Variable;

    public float value
    {
        get
        {
            return UseConstant ? Literal : Variable.Value;
        }
    }
}