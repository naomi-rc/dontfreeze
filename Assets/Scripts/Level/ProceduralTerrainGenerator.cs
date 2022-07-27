using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProceduralLevel {
    [RequireComponent(typeof(MeshFilter))]
    public class ProceduralTerrainGenerator : MonoBehaviour
    {
        Transform worldParent;

        // Level data
        LevelManager levelManager;        

        // Terrain mesh generation
        Mesh mesh;
        List<Vector3> vertices;
        List<Vector3> nonPathPositions;
        List<Vector3> pathPositions;
        List<Vector3> pathShoulderPositions;
        int[] triangles;
        public float defaultHeightMultiplier = 3f;
        public float perlinNoiseX = 0.3f;
        public float perlinNoiseY = 0.3f;

        public int xSize = 20;
        public int zSize = 20;
        public int pathLength = 16;
        int pathHalfLength;
        int pathWidth = 5;
        
        float pathPositionZ;
        float xSizeMidpoint;

        // Tree generation 
        public GameObject[] treePrefabs;
        public int numberOfTrees = 1000;

        void Start()
        {
            worldParent = transform.parent;
            levelManager = FindObjectOfType<LevelManager>();
            SpawnerManager spawnerManager = FindObjectOfType<SpawnerManager>();

            // Terrain mesh generation
            mesh = new Mesh();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            GetComponent<MeshFilter>().mesh = mesh;            
            vertices = new List<Vector3>();
            pathPositionZ = (float)zSize / 4 * 3;
            xSizeMidpoint = xSize / 2;
            pathHalfLength = pathLength / 2;

            defaultHeightMultiplier = levelManager.GetCurrentLevel().level * levelManager.GetCurrentLevel().numberOfEnemies;
            perlinNoiseX = levelManager.GetCurrentLevel().difficulty / defaultHeightMultiplier;
            perlinNoiseY = levelManager.GetCurrentLevel().difficulty / defaultHeightMultiplier;

            //Tree generation
            nonPathPositions = new List<Vector3>();
            pathPositions = new List<Vector3>();                   
            pathShoulderPositions = new List<Vector3>();                   

            CreateTerrain();
            UpdateMesh();
            GenerateTrees();
            spawnerManager.GenerateEnemies(pathPositions, worldParent, levelManager.GetCurrentLevel().numberOfEnemies);
            spawnerManager.GenerateCollectibles(pathShoulderPositions, worldParent, levelManager.GetCurrentLevel().numberOfCollectibles);
            GenerateNavMesh();
            PlacePlayer();
            PlaceCheckpoint();
        }

        void CreateTerrain()
        {
            int index = 0;
            triangles = new int[xSize * zSize * 6];
            for (int x = 0; x < xSize; x++)
            {
                for (int z = 0; z < zSize; z++)
                {
                    float height0 = CalculateHeight(x, z);
                    float height1 = CalculateHeight(x, z + 1);
                    float height3 = CalculateHeight(x + 1, z);
                    float height4 = CalculateHeight(x + 1, z + 1);

                    vertices.Add(new Vector3(x, height0, z));
                    vertices.Add(new Vector3(x, height1, z + 1));
                    vertices.Add(new Vector3(x + 1, height3, z));

                    triangles[index] = index++;
                    triangles[index] = index++;
                    triangles[index] = index++;

                    vertices.Add(new Vector3(x + 1, height3, z));
                    vertices.Add(new Vector3(x, height1, z + 1));
                    vertices.Add(new Vector3(x + 1, height4, z + 1));
                    triangles[index] = index++;
                    triangles[index] = index++;
                    triangles[index] = index++;
                }
            }            
        }

        
        float CalculateHeight(int x, int z)
        {
            float heightMultiplier = defaultHeightMultiplier;
            float height = Mathf.PerlinNoise(x * perlinNoiseX, z * perlinNoiseY);
            float vertexHeight;
            if (pathPositionZ - pathWidth < z && z < pathPositionZ + pathWidth && xSizeMidpoint - pathHalfLength < x && x < xSizeMidpoint + pathHalfLength)
            {
                //Level travel path
                height = Mathf.PerlinNoise(x * perlinNoiseX, z * perlinNoiseY);
                heightMultiplier = 0.6f;
                vertexHeight = height * heightMultiplier;
                pathPositions.Add(new Vector3(x, vertexHeight, z));
            }
            else
            {
                vertexHeight = height * heightMultiplier;
                nonPathPositions.Add(new Vector3(x, vertexHeight, z));
                if ((pathPositionZ - pathWidth - 2 < z && z < pathPositionZ - pathWidth  ||
                    pathPositionZ + pathWidth < z && z < pathPositionZ + pathWidth + 2) &&
                    (xSizeMidpoint - pathHalfLength < x && x < xSizeMidpoint + pathHalfLength))
                {
                    pathShoulderPositions.Add(new Vector3(x, vertexHeight, z));
                }
            }
            return vertexHeight;
        }

        void UpdateMesh()
        {
            mesh.Clear();
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            gameObject.AddComponent<MeshCollider>().sharedMesh = mesh;
        }
                        
        void GenerateTrees()
        {
            GameObject parent = new GameObject("Trees");
            parent.transform.parent = worldParent;
            for (int i = 0; i < numberOfTrees; i++)
            {
                Vector3 position = nonPathPositions[Random.Range(0, nonPathPositions.Count)];
                Instantiate(treePrefabs[Random.Range(0, treePrefabs.Length)], position, Quaternion.identity, parent.transform);
            }            
        }

        void GenerateNavMesh()
        {
            NavMeshSurface navMeshSurface = gameObject.AddComponent<NavMeshSurface>();
            navMeshSurface.collectObjects = CollectObjects.Children;
            navMeshSurface.voxelSize = 0.125f;
            navMeshSurface.buildHeightMesh = true;
            navMeshSurface.useGeometry = NavMeshCollectGeometry.PhysicsColliders;
            navMeshSurface.BuildNavMesh();
        }

        void PlacePlayer()
        {
            GameObject target = GameObject.FindGameObjectWithTag("Player");
            target.transform.position = pathPositions[0];            
        }

        void PlaceCheckpoint()
        {            
            GameObject checkpoint = GameObject.FindGameObjectWithTag("Checkpoint");
            checkpoint.transform.position = pathPositions[pathPositions.Count - 100];
        }
    }
}