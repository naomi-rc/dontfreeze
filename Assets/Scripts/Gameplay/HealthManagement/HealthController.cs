using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private IntVariable playerHealth;

    [SerializeField] private HealthBarController healthBar;
    [SerializeField] private int maxHealth = 100;

    private void Start()
    {
        playerHealth.value = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }


    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(playerHealth.value);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Enemy")
        {
            playerHealth.value --;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            playerHealth.value = (playerHealth.value + 10 <= maxHealth) ? playerHealth.value + 10 : maxHealth;
          
            Destroy(other.gameObject);
        }
    }
}
