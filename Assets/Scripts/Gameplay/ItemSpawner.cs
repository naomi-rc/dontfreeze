using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
class Loot
{
    public GameObject prefab;
    public int count;
}


public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private bool drawGizmos = true;

    [SerializeField]
    private Color gizmosColor = Color.green;

    [SerializeField]
    private List<GameObject> spawnpoints = new List<GameObject>();

    [SerializeField]
    private List<Loot> lootPool = new List<Loot>();

    [SerializeField]
    private GameObject spawnParent = default;


    void Start()
    {
        var totalItems = lootPool.Sum(loot => loot.count);
        if (totalItems > spawnpoints.Count)
        {
            Debug.LogError("Not enough spawnpoints to spawn all items");
            return;
        }

        Spawn();
    }

    void Spawn()
    {
        var selectedSpawnpointsIndex = new List<int>();

        foreach (var loot in lootPool)
        {
            for (int i = 0; i < loot.count; i++)
            {
                var index = Random.Range(0, spawnpoints.Count);
                while (selectedSpawnpointsIndex.Contains(index))
                {
                    index = Random.Range(0, spawnpoints.Count);
                }
                selectedSpawnpointsIndex.Add(index);
                var spawnpoint = spawnpoints[index];
                var item = Instantiate(loot.prefab, spawnpoint.transform.position, Quaternion.identity);
                if (spawnParent != null)
                {
                    item.transform.parent = spawnParent.transform;
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if (!drawGizmos)
            return;

        Gizmos.color = gizmosColor;

        foreach (var point in spawnpoints)
        {
            Gizmos.DrawCube(point.transform.position, Vector3.one);
        }
    }
}
