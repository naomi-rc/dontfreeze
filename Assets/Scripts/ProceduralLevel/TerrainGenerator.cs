using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public int depth = 20;

    public float scale = 20;

    public float offsetX = 100f;
    public float offsetY = 100f;

    Terrain terrain;

    private void Start()
    {
        terrain = GetComponent<Terrain>();
        offsetX = Random.Range(0f, 99999f);
        offsetY = Random.Range(0f, 99999f);
    }
    
    private void Update()
    {
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0,0, GenerateHeights());

        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }

        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        float xPerlinCoord = (float)x / width * scale + offsetX;
        float yPerlinCoord = (float)y / height * scale + offsetY;

        return Mathf.PerlinNoise(xPerlinCoord, yPerlinCoord);
    }
}
