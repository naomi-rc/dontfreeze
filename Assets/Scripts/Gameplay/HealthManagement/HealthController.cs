using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private InputReader inputReader;

    [SerializeField] private HealthBarController healthBar;


    [SerializeField] private int maxHealth = 100;
    private void Awake()
    {
        inputReader.JumpEvent += Damage; // TODO modifier
    }

    private void Start()
    {
        playerHealth.Health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Damage()
    {
        playerHealth.Health -= 10;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(playerHealth.Health);
    }
}
