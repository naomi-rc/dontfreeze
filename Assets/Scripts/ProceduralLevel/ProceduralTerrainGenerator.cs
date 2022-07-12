using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ProceduralTerrainGenerator : MonoBehaviour
{
    float baseDepth = 2f;

    Mesh mesh;
    List<Vector3> verticesList;
    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;
    float pathPositionZ;
    float xSizeMidpoint;
    int pathSize = 5;
    int pathHalfLength = 200;

    void Start()
    {
        mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        GetComponent<MeshFilter>().mesh = mesh;
        verticesList = new List<Vector3>();
        pathPositionZ = (float)zSize / 4 * 3;
        xSizeMidpoint = xSize / 2;
        //StartCoroutine(CreateShape());
        //StartCoroutine(CreateSharpShape());
        CreateSharpShape();
        UpdateMesh();
    }

    void CreateSharpShape()
    {
        int index = 0;
        triangles = new int[xSize * zSize * 6];
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                

                float height0 = CalculateHeight(x, z);
                float height1 = CalculateHeight(x, z + 1);
                float height3 = CalculateHeight(x + 1, z);
                float height4 = CalculateHeight(x + 1, z + 1);

                verticesList.Add(new Vector3(x, height0, z));
                verticesList.Add(new Vector3(x, height1, z+1));
                verticesList.Add(new Vector3(x+1, height3, z));

                triangles[index] = index++;
                triangles[index] = index++;
                triangles[index] = index++;
                /*
                 verticesList.Add(new Vector3(0, CalculateHeight(0,0), 0));
                verticesList.Add(new Vector3(0, CalculateHeight(0,1), 1));
                verticesList.Add(new Vector3(1, CalculateHeight(1,0), 0));
                 */



                verticesList.Add(new Vector3(x+1, height3, z));
                verticesList.Add(new Vector3(x, height1, z+1));
                verticesList.Add(new Vector3(x+1, height4, z+1));
                triangles[index] = index++;
                triangles[index] = index++;
                triangles[index] = index++;
                //yield return new WaitForSeconds(0.0001f);
                /*
                 verticesList.Add(new Vector3(1, CalculateHeight(0, 0), 0));
                verticesList.Add(new Vector3(0, CalculateHeight(0, 1), 1));
                verticesList.Add(new Vector3(1, CalculateHeight(1,1), 1));
                */
            }            
        }
    }

    float CalculateHeight(int x, int z)
    {
        //float heightMultiplier = 0.04f * (x - (float)xSize / 2) * (x - (float)xSize / 2) + baseDepth;
        float heightMultiplier = 3f;
        if (pathPositionZ - pathSize < z && z < pathPositionZ + pathSize && xSizeMidpoint - pathHalfLength < x && x < xSizeMidpoint + pathHalfLength)
            heightMultiplier = 0.6f;
        return Mathf.PerlinNoise(x * .3f, z * .3f) * heightMultiplier;
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = verticesList.ToArray(); //vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
