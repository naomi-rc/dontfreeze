using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TransformEventChannel", menuName = "Channels/TransformEventChannel", order = 0)]
public class TransformEventChannel : ScriptableObject
{
    public UnityAction<Transform> OnEventRaised;

    public void Raise(Transform value)
    {
        OnEventRaised?.Invoke(value);
    }
}
