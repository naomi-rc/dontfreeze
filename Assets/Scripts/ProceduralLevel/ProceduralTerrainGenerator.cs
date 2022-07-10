using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ProceduralTerrainGenerator : MonoBehaviour
{
    float baseDepth = 2f;

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        //StartCoroutine(CreateShape());
        CreateShape();

    }

    private void Update()
    {
        UpdateMesh();
    }
    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int index = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = CalculateHeight(x,z); 
                vertices[index] = new Vector3(x, y, z);
                index++;
            }
        }

        int vert = 0;
        int tris = 0;

        triangles = new int[xSize * zSize * 6];
        for (int z = 0; z < xSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris] = vert;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
               // yield return new WaitForSeconds(0f);
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
                //yield return new WaitForSeconds(0f);
            }
            vert++;
        }

    }

    float CalculateHeight(int x, int z)
    {
        /*float heightMultiplier = 5f;
        if (6 < x && x < 14)
            heightMultiplier = 2f;
        */

        float heightMultiplier = 0.04f * Mathf.Pow(x - (float)xSize / 2, 2) + baseDepth;
        return Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    public void OnDrawGizmos()
    {
        if (vertices == null)
            return;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }
}
