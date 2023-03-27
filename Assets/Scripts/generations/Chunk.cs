using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
public class Chunk : MonoBehaviour
{

    private Mesh mesh;

    private Vector3[] vertices;
    private int[] triangles;
    private Color[] colors;
    private int chunkSize = Global.chunkSize + 1;

    public Gradient gradient;


    private void Start()
    {

        mesh = new Mesh();

        GetComponent<MeshFilter>().mesh = mesh;

        vertices = new Vector3[chunkSize * chunkSize];
        triangles = new int[chunkSize * chunkSize * 6];


        GenerateMesh();
        UpdateMesh();

        MeshCollider meshCollider = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        meshCollider.sharedMesh = mesh;
    }


    private void GenerateMesh()
    {
        int vertexIndex = 0;

        for (int i = 0; i < chunkSize; i++)
        {
            for (int j = 0; j < chunkSize; j++)
            {
                float vertexPosX = j * Global.chunkResolution;
                float vertexPosY = i * Global.chunkResolution;

                float xSample = (vertexPosX + transform.position.x + 1000) * 0.1f;
                float ySample = (vertexPosY + transform.position.z + 1000) * 0.1f;
                
                float noise = PerlinNoiseUtils.GetFractalNoise(xSample, ySample) * Global.worldMaxHeight;

                vertices[vertexIndex] = new Vector3(vertexPosX, noise, vertexPosY);

                vertexIndex++;
            }
        }

        int triangleIndex = 0;


        for (int i = 0; i < chunkSize - 1; i++)
        {
            for (int j = 0; j < chunkSize - 1; j++)
            {
                triangles[triangleIndex * 6] = triangleIndex;
                triangles[triangleIndex * 6 + 1] = triangleIndex + chunkSize;
                triangles[triangleIndex * 6 + 2] = triangleIndex + 1;

                triangles[triangleIndex * 6 + 3] = triangleIndex + chunkSize;
                triangles[triangleIndex * 6 + 4] = triangleIndex + chunkSize + 1;
                triangles[triangleIndex * 6 + 5] = triangleIndex + 1;

                triangleIndex++;
            }

            triangleIndex++;
        }

        colors = new Color[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            float height = Mathf.InverseLerp(0, Global.worldMaxHeight, vertices[i].y);

            colors[i] = gradient.Evaluate(height);
        }
    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;

        mesh.RecalculateNormals();
    }

    public string GetID()
    {
        return (int)(transform.position.x) + "," + (int)(transform.position.z);
    }
}
