using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Camera mainCamera;

    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
    }
    private void Update()
    {
        transform.LookAt(mainCamera.transform, Vector3.up);
    }
}
