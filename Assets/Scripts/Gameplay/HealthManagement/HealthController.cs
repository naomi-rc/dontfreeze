using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private HealthBarController healthBar;
    [SerializeField] private int maxHealth = 100;

    private void Start()
    {
        playerHealth.Health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }


    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(playerHealth.Health);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Enemy")
        {
            playerHealth.Health --;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            playerHealth.Health += 10;
            Destroy(other.gameObject);
        }
    }
}
