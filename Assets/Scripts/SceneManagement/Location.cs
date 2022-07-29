using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    [SerializeField]
    private SceneObject location;

    [SerializeField]
    private SceneEventChannel sceneEventChannel = default;

    public void Load()
    {
        if (location is not null)
        {
            sceneEventChannel.Raise(location);
        }
    }

    public void SetNextLocation(SceneObject nextLocation)
    {
        location = nextLocation;
    }
}
