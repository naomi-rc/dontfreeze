using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private IntVariable playerHealth;

    [SerializeField] private HealthBarController healthBar;
    [SerializeField] private int maxHealth = 100;

    [SerializeField] private FloatEventChannel onBleedingEvent = default;

    private void Awake()
    {
        playerHealth.value = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        onBleedingEvent.OnEventRaised += SetBleed;
    }

    private void OnDisable()
    {
        onBleedingEvent.OnEventRaised -= SetBleed;
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

    private void SetBleed(float duration)
    {
        StartCoroutine(BleedCoroutine(duration));
    }

    // Damage per seconds (1)
    private IEnumerator BleedCoroutine(float duration)
    {
        for (int time = 0; time < duration; ++time)
        {
            Decrease(1);

            yield return new WaitForSeconds(1f);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Decrease(10);
            SetBleed(5f);
        }

        if (other.gameObject.layer == 3)
        {
            playerHealth.value = (playerHealth.value + 10 <= maxHealth) ? playerHealth.value + 10 : maxHealth;

            Destroy(other.gameObject);
        }
    }
}
