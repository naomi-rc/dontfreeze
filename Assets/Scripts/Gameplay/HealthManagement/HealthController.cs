using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// todo: Create controller class/interface

public class HealthController : MonoBehaviour
{
    [SerializeField] private FloatVariable playerHealth;
    [SerializeField] private VoidEventChannel onPlayerDeathEvent;

    [SerializeField] private int maxHealth = 100;

    [SerializeField] private DPSStatus bleedEffect = default;

    private bool isDead = false;
    [SerializeField] private Animator animator;
    private bool isDefending;

    private void Start()
    {
        playerHealth.value = maxHealth;
        bleedEffect.OnActivateEvent += ApplyBleed;
    }

    private void OnDisable()
    {
        bleedEffect.OnActivateEvent -= ApplyBleed;
    }


    // Update is called once per frame
    void Update()
    {
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Life"))
        {
            playerHealth.value = (playerHealth.value + 10 <= maxHealth) ? playerHealth.value + 10 : maxHealth;

            Destroy(other.gameObject);
        }
    }

    public void TakeDamage()
    {
        if (isDead)
            return;

        bleedEffect.Activate();

        isDefending = true;

        if (playerHealth.value <= 0)
        {
            isDead = true;
            animator.SetBool("isDead", true);
            onPlayerDeathEvent.Raise();
        }
        Decrease(1);
    }

    public void Decrease(float value)
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
            playerHealth.value -= bleedEffect.value;

            yield return new WaitForSeconds(1f); // Wait for 1 second.
        }

        bleedEffect.Deactivate();
    }


}
