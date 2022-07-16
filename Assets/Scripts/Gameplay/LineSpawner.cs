using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
class Enemy
{
    public GameObject prefab;
    public int count;
}

public class LineSpawner : MonoBehaviour
{
    [SerializeField]
    private bool drawGizmos = true;

    [SerializeField]
    private Color gizmosColor = Color.black;

    [SerializeField]
    private List<GameObject> waypoints = new List<GameObject>();

    [SerializeField]
    private List<Enemy> enemies = default;

    [SerializeField]
    private GameObject spawnParent = default;

    void Start()
    {
        Spawn();
    }

    public IEnumerable<(T, T)> Pairwise<T>(IEnumerable<T> source)
    {
        var previous = default(T);
        using (var it = source.GetEnumerator())
        {
            if (it.MoveNext())
                previous = it.Current;

            while (it.MoveNext())
                yield return (previous, previous = it.Current);
        }
    }

    void Spawn()
    {
        var pairs = Pairwise(waypoints).ToList();
        foreach (var enemy in enemies)
        {
            int enemiesSpawned = 0;
            while (enemiesSpawned != enemy.count)
            {
                var pair = pairs[Random.Range(0, pairs.Count)];
                var start = pair.Item1.transform.position;
                var end = pair.Item2.transform.position;

                var xRange = Mathf.Abs(start.x - end.x);
                var yRange = Mathf.Abs(start.y - end.y);
                var zRange = Mathf.Abs(start.z - end.z);

                Vector3 spawnLocation = new Vector3(
                    start.x + Random.Range(-xRange, xRange),
                    start.y + Random.Range(-yRange, yRange),
                    start.z + Random.Range(-zRange, zRange)
                );

                var instance = Instantiate(enemy.prefab, spawnLocation, Quaternion.identity);
                if (spawnParent != null)
                {
                    instance.transform.parent = spawnParent.transform;
                }
                enemiesSpawned++;
            }
        }

    }

    public void AddWaypoint(GameObject waypoint)
    {
        waypoints.Add(waypoint);
    }


    void OnDrawGizmos()
    {
        if (!drawGizmos)
            return;

        Gizmos.color = gizmosColor;

        var pairs = Pairwise(waypoints);
        foreach (var pair in pairs)
        {
            var start = pair.Item1.transform.position;
            var end = pair.Item2.transform.position;
            Gizmos.DrawCube(start, Vector3.one * 0.5f);
            Gizmos.DrawCube(end, Vector3.one * 0.5f);
            Gizmos.DrawLine(start, end);
        }
    }
}
