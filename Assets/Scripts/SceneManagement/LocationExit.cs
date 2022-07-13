using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class LocationExit : MonoBehaviour
{
    [SerializeField]
    private SceneObject scene;

    [SerializeField]
    private LocationLoader locationLoader;

    private void Awake()
    {
        var collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            locationLoader.Load(scene);
        }
    }
}
