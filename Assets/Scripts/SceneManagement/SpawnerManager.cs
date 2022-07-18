using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] GameObject[] lootPrefabs;
    public void GenerateEnemies(List<Vector3> pathPositions, Transform parent, int numberOfEnemies)
    {
        GameObject enemies = new GameObject("Enemies");
        enemies.transform.parent = parent;
        enemies.transform.position = parent.position;

        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 position = pathPositions[Random.Range(0, pathPositions.Count)];
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], position, Quaternion.identity, enemies.transform);
        }
    }

    public void GenerateCollectibles(List<Vector3> pathShoulderPositions, Transform parent, int numberOfItems)
    {
        GameObject items = new GameObject("Items");
        items.transform.parent = parent;
        items.transform.position = parent.position;

        for (int i = 0; i < numberOfItems; i++)
        {
            Vector3 position = pathShoulderPositions[Random.Range(0, pathShoulderPositions.Count)];
            Instantiate(lootPrefabs[Random.Range(0, lootPrefabs.Length)], position, Quaternion.identity, items.transform);
        }
    }
}
