using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    public float enemytHealth;
    public LootTable lootTable;

    [SerializeField] private HealthBarController healthBarController;
    public float maxHealth = 100f;

    FaceCamera faceCamera;

    private void Start()
    {
        enemytHealth = maxHealth;
        faceCamera = FindObjectOfType<FaceCamera>();

        if (healthBarController is not null)
        {
            healthBarController.SetMaxHealth(maxHealth);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (healthBarController is not null)
        {
            healthBarController.SetHealth(enemytHealth);
        }
    }

    void OnDestroy()
    {
        if (lootTable && enemytHealth <= 0)
        {
            lootTable.DropLoot(transform.position);
        }
    }

    public void TakeDamage(int damage)
    {
        enemytHealth -= damage;
    }

    public void SetupHealthBar(Canvas canvas, Camera camera)
    {
        if (healthBarController is not null)
        {
            faceCamera.mainCamera = camera;
        }
    }
}
