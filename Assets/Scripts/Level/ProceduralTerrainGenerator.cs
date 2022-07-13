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
        Vector3 playerSpawnPosition;

        //Level data
        LevelManager levelManager;

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
        TerrainData terrain;
        private int resolution = 512;
        private Vector3 addTerrain;
        int bottomTopRadioSelected = 0;
        private float shiftHeight = 0f;
        GameObject terrainObject;
        Terrain terrainComponent;
        List<Vector3> nonPathPositions;
        List<Vector3> pathPositions;

        // Tree generation 
        public GameObject[] treePrefabs;
        public int numberOfTrees = 1000;

       
       
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
            terrain = new TerrainData();

            //Tree generation
            nonPathPositions = new List<Vector3>();
            pathPositions = new List<Vector3>();

            CreateTerrainShape();
            UpdateMesh();
            CreateTerrain();
            GenerateTrees();
            GenerateEnemies();
            GenerateNavMesh();
            PlacePlayer();
            PlaceCheckpoint();
        }

        void CreateTerrainShape()
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
            float heightMultiplier = 3f;
            float height = Mathf.PerlinNoise(x * .3f, z * .3f);
            float vertexHeight;
            if (pathPositionZ - pathSize < z && z < pathPositionZ + pathSize && xSizeMidpoint - pathHalfLength < x && x < xSizeMidpoint + pathHalfLength)
            {
                //Level travel path
                //height = Mathf.PerlinNoise(x * .1f, z * .1f) * 0.6f;
                height = Mathf.PerlinNoise(x * .3f, z * .3f);
                heightMultiplier = 0.6f;
                vertexHeight = height * heightMultiplier;
                pathPositions.Add(new Vector3(x, vertexHeight, z));
            }
            else
            {
                vertexHeight = height * heightMultiplier;
                nonPathPositions.Add(new Vector3(x, vertexHeight, z));
            }
            /*else if ((pathPositionZ - pathSize >= z || z >= pathPositionZ + pathSize))
            {
                height = Mathf.PerlinNoise(x * .3f, z * .3f);
                //height = Mathf.PerlinNoise(x * .5f, z * .5f);
                //heightMultiplier = 0.2f * Mathf.Abs(pathPositionZ - z);
                heightMultiplier = 0.2f * Mathf.Abs((pathPositionZ - pathSize) - z);
            }
            else if ((xSizeMidpoint - pathHalfLength >= x))
            {
                height = Mathf.PerlinNoise(x * .3f, z * .3f);
                //height = Mathf.PerlinNoise(x * .5f, z * .5f);
                //heightMultiplier = 0.2f * Mathf.Abs(pathPositionZ - z);
                heightMultiplier = 0.2f * Mathf.Abs((xSizeMidpoint - pathHalfLength) - x);
            }
            else if ((x >= xSizeMidpoint + pathHalfLength))
            {
                height = Mathf.PerlinNoise(x * .3f, z * .3f);
                //height = Mathf.PerlinNoise(x * .5f, z * .5f);
                heightMultiplier = 0.2f * Mathf.Abs((xSizeMidpoint + pathHalfLength) - x);
            }*/

            
            return vertexHeight;
        }

        void UpdateMesh()
        {
            mesh.Clear();

            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
        }

        delegate void CleanUp();

        void CreateTerrain()
        {

            terrain.heightmapResolution = resolution;
            terrainObject = Terrain.CreateTerrainGameObject(terrain);

            //Terrain terrainComponent  = terrainObject.GetComponent<Terrain>();
            terrainComponent = terrainObject.GetComponent<Terrain>();
            terrainComponent.drawHeightmap = false;


            MeshCollider collider = gameObject.GetComponent<MeshCollider>();
            CleanUp cleanUp = null;

            //Add a collider to our source object if it does not exist.
            //Otherwise raycasting doesn't work.
            if (!collider)
            {
                collider = gameObject.AddComponent<MeshCollider>();
                cleanUp = () => DestroyImmediate(collider);
            }

            Bounds bounds = collider.bounds;
            float sizeFactor = collider.bounds.size.y / (collider.bounds.size.y + addTerrain.y);
            terrain.size = collider.bounds.size + addTerrain;
            bounds.size = new Vector3(terrain.size.x, collider.bounds.size.y, terrain.size.z);

            // Do raycasting samples over the object to see what terrain heights should be
            float[,] heights = new float[terrain.heightmapResolution, terrain.heightmapResolution];
            Ray ray = new Ray(new Vector3(bounds.min.x, bounds.max.y + bounds.size.y, bounds.min.z), -Vector3.up);
            RaycastHit hit = new RaycastHit();
            float meshHeightInverse = 1 / bounds.size.y;
            Vector3 rayOrigin = ray.origin;

            int maxHeight = heights.GetLength(0);
            int maxLength = heights.GetLength(1);

            Vector2 stepXZ = new Vector2(bounds.size.x / maxLength, bounds.size.z / maxHeight);

            for (int zCount = 0; zCount < maxHeight; zCount++)
            {
                for (int xCount = 0; xCount < maxLength; xCount++)
                {

                    float height = 0.0f;

                    if (collider.Raycast(ray, out hit, bounds.size.y * 3))
                    {
                        height = (hit.point.y - bounds.min.y) * meshHeightInverse;
                        height += shiftHeight;

                        //bottom up
                        if (bottomTopRadioSelected == 0)
                        {

                            height *= sizeFactor;
                        }

                        //clamp
                        if (height < 0)
                        {
                            height = 0;
                        }
                    }

                    heights[zCount, xCount] = height;
                    rayOrigin.x += stepXZ[0];
                    ray.origin = rayOrigin;
                }

                rayOrigin.z += stepXZ[1];
                rayOrigin.x = bounds.min.x;
                ray.origin = rayOrigin;
            }

            terrain.SetHeights(0, 0, heights);

            terrainObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

            if (cleanUp != null)
            {
                cleanUp();
            }
        }

        void GenerateTrees()
        {            
            GameObject treesParent = Instantiate(new GameObject("Trees"), transform);
            for (int i = 0; i < numberOfTrees; i++)
            {
                Vector3 worldTreePos = nonPathPositions[Random.Range(0, nonPathPositions.Count)] + transform.position;
                Instantiate(treePrefabs[Random.Range(0, treePrefabs.Length)], worldTreePos, Quaternion.identity, treesParent.transform);
            }            
        }
        void GenerateEnemies()
        {
            for(int i = 0; i < levelManager.level.numberOfEnemies; i++)
            {
                Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], pathPositions[20], Quaternion.identity, transform);
            }            
        }

        void GenerateNavMesh()
        {
            NavMeshSurface navMeshSurface = terrainObject.AddComponent<NavMeshSurface>();
            navMeshSurface.BuildNavMesh();
        }

        void PlacePlayer()
        {
            GameObject target = GameObject.FindGameObjectWithTag("Player");
            target.transform.position = pathPositions[0];
            GameObject[] agents = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject agent in agents)
            {
                agent.GetComponent<EnemyBehavior>().UpdateTarget(target);
            }
        }

        void PlaceCheckpoint()
        {
            GameObject checkpoint = GameObject.FindGameObjectWithTag("Checkpoint");
            checkpoint.transform.position = pathPositions[pathPositions.Count - 1];            
        }
    }
}