using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] GameObject[] lootPrefabs;
    public void GenerateEnemyWaypoints(List<Vector3> pathPositions)
    {
        GameObject enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
        LineSpawner lineSpawner = enemySpawner.GetComponent<LineSpawner>();
        List<GameObject> waypoints = new List<GameObject>();
        for (int i = 1000; i < pathPositions.Count; i += 1200)
        {
            GameObject waypoint = new GameObject("Waypoint");
            waypoint.transform.parent = enemySpawner.transform;
            waypoint.transform.position = pathPositions[i];
            waypoints.Add(waypoint);
        }
        if (waypoints.Count % 2 != 0)
        {
            waypoints.RemoveAt(waypoints.Count - 1);
        }
        lineSpawner.AddWaypoints(waypoints);

        int numberOfEnemies = FindObjectOfType<LevelManager>().level.numberOfEnemies;
        int enemy = numberOfEnemies, prefab = 0;
        while (enemy > 0 && prefab < enemyPrefabs.Length - 1)
        {
            int numberAdded = Random.Range(1, enemy);
            lineSpawner.AddEnemy(enemyPrefabs[prefab], numberAdded);
            enemy -= numberAdded;
            prefab++;
            
        }
        lineSpawner.AddEnemy(enemyPrefabs[enemyPrefabs.Length - 1], enemy);
        lineSpawner.Spawn();
    }

    /*public void GenerateCollectibleSpawnpoints(List<Vector3> pathShoulderPositions)
    {
        ItemSpawner itemSpawner = FindObjectOfType<ItemSpawner>();
        int levelDifficulty = FindObjectOfType<LevelManager>().level.difficulty;
        List<GameObject> spawnpoints = new List<GameObject>();
        int maxItems = levelDifficulty * 3;
        for (int i = 0; i < pathShoulderPositions.Count; i+=90)
        {
            GameObject spawnpoint = new GameObject("Spawnpoint");
            spawnpoint.transform.parent = itemSpawner.transform;
            spawnpoint.transform.position = pathShoulderPositions[i];
            spawnpoints.Add(spawnpoint);
        }

        itemSpawner.AddSpawnpoints(spawnpoints);

        int item = maxItems, prefab = 0;
        while (item > 0 && prefab < lootPrefabs.Length - 1)
        {
            int numberAdded = Random.Range(1, item);
            itemSpawner.AddLoot(lootPrefabs[prefab], numberAdded);
            item -= numberAdded;
            prefab++;

        }
        itemSpawner.AddLoot(lootPrefabs[lootPrefabs.Length - 1], item);
        itemSpawner.Spawn();
    }*/
}
