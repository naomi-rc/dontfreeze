using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// todo: Create controller class/interface

public class HealthController : MonoBehaviour
{
    [SerializeField] private IntVariable playerHealth;
    [SerializeField] private VoidEventChannel onPlayerDeathEvent;

    [SerializeField] private HealthBarController healthBar;
    [SerializeField] private int maxHealth = 100;

    [SerializeField] private DPSStatus bleedEffect = default;

    private bool isDead = false;
    [SerializeField] private Animator animator;
    private bool isDefending;




    private void Start()
    {
        playerHealth.value = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }

        bleedEffect.OnActivateEvent += ApplyBleed;
    }

    private void OnDisable()
    {
        bleedEffect.OnActivateEvent -= ApplyBleed;
    }


    // Update is called once per frame
    void Update()
    {
        if (healthBar != null)
        {
            healthBar.SetHealth(playerHealth.value);
        }

        if (isDefending)
        {
            animator.SetBool("isDefending", true);
            isDefending = false;
        }
        else
        {
            animator.SetBool("isDefending", false);

        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isDead) { return; }

        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }

        if (playerHealth.value <= 0)
        {
            isDead = true;
            onPlayerDeathEvent.Raise();
            animator.SetBool("isDead", true);
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

    public void TakeDamage()
    {
        Decrease(10);
        bleedEffect.Activate();

        isDefending = true;
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


}
