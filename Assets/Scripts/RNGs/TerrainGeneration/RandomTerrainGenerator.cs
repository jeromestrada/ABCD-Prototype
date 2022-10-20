using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTerrainGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _terrainPrefabs;

    [Header("Terrain Size refers to the number of prefabs along an axis")]
    [SerializeField] Vector2Int _terrainSize;
    [SerializeField] private MeshFilter _meshFilter;

    [ContextMenu("Generate terrain")]
    public void Generate()
    {
        Debug.Log($"Max array size is {System.Int32.MaxValue}");
        Mesh mesh = new Mesh();
        mesh.vertices = CreateVertices();
        mesh.triangles = CreateTriangles();

        mesh.RecalculateNormals();
        _meshFilter.sharedMesh = mesh;
    }

    private Vector3[] CreateVertices()
    {
        Vector3[] vertices = new Vector3[(_terrainSize.x + 1) * (_terrainSize.y + 1)];
        for(int z = 0, i = 0; z <= _terrainSize.y; z++)
        {
            for(int x = 0; x <= _terrainSize.x; x++)
            {
                vertices[i] = new Vector3(x, 0, z);
                i++;
            }
        }
        return vertices;
    }

    private int[] CreateTriangles()
    {
        int[] triangles = new int[_terrainSize.x * _terrainSize.y * 6];

        for(int z = 0, vert = 0, tris = 0; z < _terrainSize.y; z++)
        {
            for(int x = 0; x < _terrainSize.x; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + _terrainSize.x + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + _terrainSize.x + 1;
                triangles[tris + 5] = vert + _terrainSize.x + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
        return triangles;
    }
}
