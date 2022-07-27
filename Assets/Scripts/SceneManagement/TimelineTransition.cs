using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimelineTransition : MonoBehaviour
{
    [SerializeField] Location location = default;

    void Start()
    {
        Debug.Log("Loading scene...");
        //location.Load();
        SceneManager.LoadScene("SafeHouse", LoadSceneMode.Additive);
    }
}
