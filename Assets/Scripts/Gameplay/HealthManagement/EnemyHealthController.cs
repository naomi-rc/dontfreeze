using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    private int enemytHealth;

    [SerializeField] private HealthBarController healthBarC;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject healthbar;

    private void Start()
    {
        enemytHealth = maxHealth;
        healthBarC.SetMaxHealth(maxHealth);
    }


    // Update is called once per frame
    void Update()
    {
        healthBarC.SetHealth(enemytHealth);
    }

    
    public void TakeDamage(int damage)
    {
        enemytHealth -= damage;

        if(enemytHealth < 0)
        {
            OnDied();
        }
    }
    private void OnDied()
    {
        Destroy(gameObject, 1f);
        Destroy(healthbar.gameObject, 1f);
    }

    public void SetupHealthBar(Canvas Canvas, Camera Camera)
    {
        healthBarC.transform.SetParent(Canvas.transform);
        if (healthBarC.TryGetComponent<FaceCamera>(out FaceCamera faceCamera))
        {
            faceCamera.Camera = Camera;
        }
    }
}
