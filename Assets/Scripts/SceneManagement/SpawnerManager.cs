using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    public void GenerateEnemySpawnpoints(List<Vector3> pathPositions)
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
}
