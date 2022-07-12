using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    public float enemytHealth;

    [SerializeField] private HealthBarController healthBarC;
    public float maxHealth = 100f;

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
    }

    public void SetupHealthBar(Canvas Canvas, Camera Camera)
    {
        healthBarC.transform.SetParent(Canvas.transform);
        if (healthBarC.TryGetComponent<FaceCamera>(out FaceCamera faceCamera))
        {
            faceCamera.mainCamera = Camera;
        }
    }
}
