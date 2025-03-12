using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] Vector2Int size;
    
    /*
    public int xSize = 20;
    public int zSize = 20;
    */
    
    public Gradient gradient;
    
    float _minTerrainHeight;
    float _maxTerrainHeight;

    public void MainAction()
    {
        Generate();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Generate()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = CreateVertices();
        mesh.triangles = CreateTriangles();
        
        mesh.RecalculateNormals();
        meshFilter.sharedMesh = mesh;
        // meshFilter.mesh.colors = Colors();
    }

    private Vector3[] CreateVertices()
    {
        Vector3[] vertices = new Vector3[(size.x + 1) * (size.y + 1)];

        for (int i = 0, z = 0; z <= size.y; z++)
        {
            for (int x= 0; x <= size.x ; x++)
            {
                // float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                vertices[i] = new Vector3(x, 0, z);
                i++;
            }
        }

        return vertices;
    }

    private int[] CreateTriangles()
    {
        int[] triangles = new int[size.x * size.y * 6];
        
        for (int z = 0, vert = 0, tris = 0; z < size.y ; z++)
        {
            for (int x = 0; x < size.x; x++)
            {

                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + size.x + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + size.x + 1;
                triangles[tris + 5] = vert + size.x + 2;

                vert++;
                tris += 6;

                
            }
            vert++;
        }
        
        return triangles;
    }


    private void Start()
    {
        // Generate();
    }

    private void Update()
    {
        Generate();
       
    }

    private Color[] Colors()
    {
        
        
        
        
        Color[] colors = new Color[meshFilter.mesh.vertices.Length];
        
        for (int i = 0, z = 0; z <= size.y; z++)
        {
            for (int x= 0; x <= size.x ; x++)
            {

                float height = Mathf.InverseLerp(_minTerrainHeight, _maxTerrainHeight, meshFilter.mesh.vertices[i].y);
                colors[i] = gradient.Evaluate(height);
                i++;
            }
        }

        return colors;
    }

    private void OnDrawGizmos()
    {
        if (meshFilter.mesh.vertices == null)
            return;


        for (int i = 0; i < meshFilter.mesh.vertices.Length; i++)
        {
            Gizmos.DrawSphere(meshFilter.mesh.vertices[i], 0.1f);
        }
        
    }
}

