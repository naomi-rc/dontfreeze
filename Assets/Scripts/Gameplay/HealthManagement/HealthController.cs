using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private IntVariable playerHealth;
    [SerializeField] private VoidEventChannel onPlayerDeathEvent;

    [SerializeField] private HealthBarController healthBar;
    [SerializeField] private int maxHealth = 100;

    private bool isDead = false;

    private void Start()
    {
        playerHealth.value = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (healthBar != null)
        {
            healthBar.SetHealth(playerHealth.value);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (isDead) { return; }

        if (hit.transform.tag == "Enemy")
        {
            playerHealth.value--;
        }

        if (playerHealth.value <= 0)
        {
            isDead = true;
            onPlayerDeathEvent.Raise();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            playerHealth.value = (playerHealth.value + 10 <= maxHealth) ? playerHealth.value + 10 : maxHealth;

            Destroy(other.gameObject);
        }
    }
}
