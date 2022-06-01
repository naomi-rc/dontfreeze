using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private InputReader inputReader;

    private void Awake()
    {
        inputReader.EnableGameplayInput();
        inputReader.JumpEvent += Kill;

    }

    void Kill()
    {
        playerHealth.Health--;
    }

    // Update is called once per frame
    void Update()
    {        
    }
}
