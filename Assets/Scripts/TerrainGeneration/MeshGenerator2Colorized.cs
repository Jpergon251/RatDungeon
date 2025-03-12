using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGenerator2Colorized : MonoBehaviour
{
    [Header("Terrain Settings")]
    public Vector2Int size = new Vector2Int(100, 100); // Tamaño del terreno (ancho y largo)

    [Header("Noise Settings")]
    public float noiseScaleMin = 10f; // Escala mínima del ruido
    public float noiseScaleMax = 30f; // Escala máxima del ruido
    public float heightMultiplierMin = 0.2f; // Mínimo multiplicador de altura
    public float heightMultiplierMax = 0.8f; // Máximo multiplicador de altura
    public int octavesMin = 2; // Mínimo número de octaves
    public int octavesMax = 4; // Máximo número de octaves
    public float persistenceMin = 0.4f; // Mínimo valor de persistencia
    public float persistenceMax = 0.6f; // Máximo valor de persistencia
    public float lacunarityMin = 1f; // Mínimo valor de lacunarity
    public float lacunarityMax = 2f; // Máximo valor de lacunarity

    public Gradient gradient; // Gradiente para colorear el terreno según la altura

    private float noiseScale; // Escala base del ruido (aleatoria)
    private float heightMultiplier; // Multiplicador de altura (aleatorio)
    private int octaves; // Número de octaves (aleatorio)
    private float persistence; // Persistencia (aleatoria)
    private float lacunarity; // Lacunarity (aleatoria)

    private Mesh _mesh;
    
    private void Start()
    {
        // GenerateRandomParameters();
    }

    private void Update()
    {
        // GenerateRandomParameters(); // Genera valores aleatorios para los parámetros
        // GenerateTerrain(); // Genera el terreno con los valores aleatorios

    }

    void mainAction()
    {
       GenerateRandomParameters(); 
       GenerateTerrain();
    }
    
    void GenerateRandomParameters()
    {
        // Asigna valores aleatorios a los parámetros
        noiseScale = Random.Range(noiseScaleMin, noiseScaleMax);
        heightMultiplier = Random.Range(heightMultiplierMin, heightMultiplierMax);
        octaves = Random.Range(octavesMin, octavesMax + 1); // +1 porque Random.Range excluye el máximo en enteros
        persistence = Random.Range(persistenceMin, persistenceMax);
        lacunarity = Random.Range(lacunarityMin, lacunarityMax);
    }

    void GenerateTerrain()
    {
        // Crear una nueva malla
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;

        // Generar vértices y colores
        Vector3[] vertices = GenerateVertices();
        Color[] colors = GenerateColors(vertices);

        // Asignar vértices, triángulos y colores a la malla
        _mesh.vertices = vertices;
        _mesh.triangles = GenerateTriangles();
        _mesh.colors = colors;

        // Recalcular normales y límites de la malla
        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();
        
        
        // Añadir o actualizar el MeshCollider
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        if (meshCollider == null)
        {
            // Si no hay un MeshCollider, añadir uno nuevo
            meshCollider = gameObject.AddComponent<MeshCollider>();
        }
        // Asignar la malla al MeshCollider
        meshCollider.sharedMesh = _mesh;
    }

    Vector3[] GenerateVertices()
    {
        Vector3[] vertices = new Vector3[(size.x + 1) * (size.y + 1)];
        float minHeight = float.MaxValue;
        float maxHeight = float.MinValue;

        for (int i = 0, z = 0; z <= size.y; z++)
        {
            for (int x = 0; x <= size.x; x++)
            {
                // Calcular la altura usando múltiples octaves
                float amplitude = 1f;
                float frequency = 1f;
                float noiseHeight = 0f;

                for (int o = 0; o < octaves; o++)
                {
                    // Coordenadas de ruido
                    float sampleX = (float)x / size.x * noiseScale * frequency;
                    float sampleZ = (float)z / size.y * noiseScale * frequency;

                    // Aplicar Perlin Noise y ajustar por amplitud
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleZ) * 2f - 1f; // Rango [-1, 1]
                    noiseHeight += perlinValue * amplitude;

                    // Actualizar amplitud y frecuencia para el siguiente octave
                    amplitude *= persistence;
                    frequency *= lacunarity;
                }

                // Asignar la altura al vértice
                vertices[i] = new Vector3(x, noiseHeight * heightMultiplier, z);

                // Actualizar las alturas mínima y máxima
                if (noiseHeight < minHeight) minHeight = noiseHeight;
                if (noiseHeight > maxHeight) maxHeight = noiseHeight;

                i++;
            }
        }

        return vertices;
    }

    int[] GenerateTriangles()
    {
        int[] triangles = new int[size.x * size.y * 6];

        for (int z = 0, vert = 0, tris = 0; z < size.y; z++)
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

    Color[] GenerateColors(Vector3[] vertices)
    {
        Color[] colors = new Color[vertices.Length];
        float minHeight = float.MaxValue;
        float maxHeight = float.MinValue;

        // Encontrar las alturas mínima y máxima
        foreach (Vector3 vertex in vertices)
        {
            if (vertex.y < minHeight) minHeight = vertex.y;
            if (vertex.y > maxHeight) maxHeight = vertex.y;
        }

        // Asignar colores basados en la altura
        for (int i = 0; i < vertices.Length; i++)
        {
            float height = Mathf.InverseLerp(minHeight, maxHeight, vertices[i].y);
            colors[i] = gradient.Evaluate(height);
        }

        return colors;
    }
    
    // private void OnDrawGizmos()
    // {
    //     if (_mesh.vertices == null)
    //         return;
    //
    //
    //     for (int i = 0; i < _mesh.vertices.Length; i++)
    //     {
    //         Gizmos.DrawSphere(_mesh.vertices[i], 0.1f);
    //     }
    //     
    // }
}