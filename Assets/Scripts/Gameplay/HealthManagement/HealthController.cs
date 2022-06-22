using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// todo: Create controller class/interface

public class HealthController : MonoBehaviour
{
    [SerializeField] private IntVariable playerHealth;

    [SerializeField] private HealthBarController healthBar;
    [SerializeField] private int maxHealth = 100;

    [SerializeField] private DPSStatus bleedEffect = default;

    private void Awake()
    {
        playerHealth.value = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        bleedEffect.OnActivateEvent += ApplyBleed;
    }

    private void OnDisable()
    {
        bleedEffect.OnActivateEvent -= ApplyBleed;
    }


    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(playerHealth.value);
    }

    public void Decrease(int value)
    {
        playerHealth.value -= value;
    }

    private void ApplyBleed()
    {
        StartCoroutine(BleedCoroutine(bleedEffect));
    }

    private IEnumerator BleedCoroutine(DPSStatus bleedEffect)
    {
        for (int time = 0; time < bleedEffect.duration; ++time)
        {
            playerHealth.value -= (int)bleedEffect.value;

            yield return new WaitForSeconds(1f); // Wait for 1 second.
        }

        bleedEffect.Deactivate();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Decrease(10);
            bleedEffect.Activate();
        }

        if (other.gameObject.layer == 3)
        {
            playerHealth.value = (playerHealth.value + 10 <= maxHealth) ? playerHealth.value + 10 : maxHealth;

            Destroy(other.gameObject);
        }
    }
}
