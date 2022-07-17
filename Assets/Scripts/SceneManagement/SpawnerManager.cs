using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * get #enemies from levelmanager 
 * get enemyspawnmanager
 * set enemies and random number of enemies
 * set items and random number of items
 */
public class SpawnerManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    public void GenerateEnemySpawnpoints(List<Vector3> pathPositions)
    {
        Debug.Log(pathPositions);
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
            Debug.Log($"Spawning {numberAdded} {enemyPrefabs[prefab].name}");
            enemy -= numberAdded;
            prefab++;
            
        }
        lineSpawner.AddEnemy(enemyPrefabs[enemyPrefabs.Length - 1], enemy);
        Debug.Log($"Spawning {enemy} {enemyPrefabs[prefab].name}");
        /* for (int i = numberOfEnemies, prefab = 0; i > 0 && prefab < enemyPrefabs.Length-1; prefab++)
         {            
             int numberAdded = Random.Range(1, i);
             lineSpawner.AddEnemy(enemyPrefabs[prefab], numberAdded);
             i -= numberAdded;
             Debug.Log($"Spawning {numberAdded} {enemyPrefabs[prefab].name}");
         }*/

        /*lineSpawner.AddEnemy(enemyPrefabs[0], 0);
        lineSpawner.AddEnemy(enemyPrefabs[1], 2);
        lineSpawner.AddEnemy(enemyPrefabs[2], 2);*/
        lineSpawner.Spawn();
    }
}
