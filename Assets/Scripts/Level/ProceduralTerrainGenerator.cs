using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProceduralLevel {
    [RequireComponent(typeof(MeshFilter))]
    public class ProceduralTerrainGenerator : MonoBehaviour
    {
        Transform worldParent;

        // Player data
        public GameObject[] enemyPrefabs;

        //Level data
        LevelManager levelManager;
        public GameObject snow;

        // Mesh generation
        Mesh mesh;
        List<Vector3> vertices;
        int[] triangles;

        public int xSize = 20;
        public int zSize = 20;
        public float pathCurveLevel = 20f;

        float pathPositionZ;
        float pathCurveOffset;
        float xSizeMidpoint;
        int pathSize = 5;
        int pathHalfLength = 200;

        public float offsetX = 100f;
        public float offsetY = 100f;

        // Terrain generation 
        List<Vector3> nonPathPositions;
        List<Vector3> pathPositions;

        // Tree generation 
        public GameObject[] treePrefabs;
        public int numberOfTrees = 1000;

        //Testing
        public float defaultHeightMultiplier = 3f;
        public float perlinNoiseX = 0.3f;
        public float perlinNoiseY = 0.3f;

        void Start()
        {
            worldParent = transform.parent;
            levelManager = FindObjectOfType<LevelManager>();

            mesh = new Mesh();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            GetComponent<MeshFilter>().mesh = mesh;
            
            vertices = new List<Vector3>();
            pathPositionZ = (float)zSize / 4 * 3;
            xSizeMidpoint = xSize / 2;
            pathCurveOffset = 0f;

            offsetX = Random.Range(0f, 99999f);
            offsetY = Random.Range(0f, 99999f);

            //Tree generation
            nonPathPositions = new List<Vector3>();
            pathPositions = new List<Vector3>();

            snow.SetActive(levelManager.level.snow);

            defaultHeightMultiplier = levelManager.level.level * levelManager.level.numberOfEnemies;
            perlinNoiseX = (float)levelManager.level.difficulty/ defaultHeightMultiplier;
            perlinNoiseY = (float)levelManager.level.difficulty/defaultHeightMultiplier;

            CreateTerrain();
            UpdateMesh();
            GenerateTrees();
            GenerateEnemies();
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
                /*if (x % pathCurveLevel == 0)
                {
                    //float perlinValue = Mathf.PerlinNoise(x * .3f, x * .3f);
                    //pathCurveOffset = (perlinValue < 0.5)? perlinValue : -perlinValue;
                    pathCurveOffset = Random.Range(-0.5f, 0.5f) -  0.2f;
                }
               
                pathPositionZ += pathCurveOffset;*/
            }
            
        }

        
        float CalculateHeight(int x, int z)
        {

            //float heightMultiplier = 0.04f * (x - (float)xSize / 2) * (x - (float)xSize / 2) + baseDepth;
            float heightMultiplier = defaultHeightMultiplier;// 3f;
            float height = Mathf.PerlinNoise(x * perlinNoiseX, z * perlinNoiseY);
            float vertexHeight;
            if (pathPositionZ - pathSize < z && z < pathPositionZ + pathSize && xSizeMidpoint - pathHalfLength < x && x < xSizeMidpoint + pathHalfLength)
            {
                //Level travel path
                //height = Mathf.PerlinNoise(x * .1f, z * .1f) * 0.6f;
                height = Mathf.PerlinNoise(x * perlinNoiseX, z * perlinNoiseY);
                heightMultiplier = 0.6f;
                vertexHeight = height * heightMultiplier;
                pathPositions.Add(new Vector3(x, vertexHeight, z));
            }
            else
            {
                vertexHeight = height * heightMultiplier;
                nonPathPositions.Add(new Vector3(x, vertexHeight, z));
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

        delegate void CleanUp();
                
        void GenerateTrees()
        {            
            GameObject parent = Instantiate(new GameObject("Trees"), worldParent);
            for (int i = 0; i < numberOfTrees; i++)
            {
                Vector3 position = nonPathPositions[Random.Range(0, nonPathPositions.Count)];
                Instantiate(treePrefabs[Random.Range(0, treePrefabs.Length)], position, Quaternion.identity, parent.transform);
            }            
        }
        void GenerateEnemies()
        {
            GameObject parent = Instantiate(new GameObject("Enemies"), worldParent);
            for (int i = 0; i < levelManager.level.numberOfEnemies; i++)
            {
                Vector3 position = pathPositions[Random.Range(0, pathPositions.Count)];
                Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], position, Quaternion.identity, parent.transform);
            }
            AgentManager agentManager = FindObjectOfType<AgentManager>();
            agentManager.UpdateEnemyTarget();
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
            checkpoint.transform.position = pathPositions[pathPositions.Count - 1];
        }
    }
}