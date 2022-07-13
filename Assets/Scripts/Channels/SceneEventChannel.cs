using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SceneEventChannel", menuName = "Channels/SceneEventChannel", order = 0)]
public class SceneEventChannel : ScriptableObject
{
    public UnityAction<SceneObject> OnEventRaised;

    public void Raise(SceneObject value)
    {
        OnEventRaised?.Invoke(value);
    }
}
