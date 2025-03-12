using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Terrain))]
public class ProceduralTerrain : MonoBehaviour
{
    [Header("Terrain Settings")]
    public int width = 512; // Ancho del terreno
    public int height = 512; // Largo del terreno
    public float heightScale = 2f; // Escala de altura
    public Material terrainMaterial;

    public Gradient gradient;
    
    [Header("Noise Settings")] 
    public float noiseScaleMin = 50f;
    public float noiseScaleMax = 200f;

    public int octavesMin = 1;
    public int octavesMax = 4;

    public float persistenceMin = 0.5f;
    public float persistenceMax = 1.5f;
    
    public float lacunarityMin = 1f;
    public float lacunarityMax = 2f;
     // Lacunarity

    private Terrain terrain;
    private TerrainData terrainData;
    private Vector3[] vertices;

    private void Start()
    {
        // Obtener el componente Terrain y TerrainData
        terrain = GetComponent<Terrain>();
        terrainData = terrain.terrainData;

        
        // Configurar el tamaño del terreno
        terrainData.size = new Vector3(width, heightScale, height);
        // Asignar el material al Terrain
        if (terrainMaterial != null)
        {
            terrain.materialTemplate = terrainMaterial;
            ConfigureMaterial();
        }
        else
        {
            Debug.LogWarning("No se ha asignado un material al Terrain.");
        }
        GenerateTerrain();
        terrain.GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    void mainAction()
    {
        // Generar el terreno procedural
        // GenerateTerrain();
    }
    void GenerateTerrain()
    {
        float noiseScale = Random.Range(noiseScaleMin, noiseScaleMax); 
        int octaves = Random.Range(octavesMin, octavesMax);
        float persistence = Random.Range(persistenceMin, persistenceMax); 
        float lacunarity = Random.Range(lacunarityMin,lacunarityMax);
        // Obtener la resolución del terreno
        int resolution = terrainData.heightmapResolution;

        // Crear un array para almacenar las alturas
        float[,] heights = new float[resolution, resolution];

        // Generar las alturas usando Perlin Noise
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                float amplitude = 1f;
                float frequency = 1f;
                float noiseHeight = 0f;

                for (int o = 0; o < octaves; o++)
                {
                    float sampleX = (float)x / resolution * noiseScale * frequency;
                    float sampleY = (float)y / resolution * noiseScale * frequency;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2f - 1f; // Rango [-1, 1]
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistence;
                    frequency *= lacunarity;
                }

                heights[x, y] = Mathf.InverseLerp(-1f, 1f, noiseHeight); // Normalizar a [0, 1]
            }
        }

        // Aplicar las alturas al terreno
        terrainData.SetHeights(0, 0, heights);
        

    }
    void ConfigureMaterial()
    {
        if (terrainMaterial != null)
        {
            // Calcular las alturas mínima y máxima del terreno
            float minHeight = 0f; // La altura mínima siempre es 0 en el Terrain
            float maxHeight = heightScale; // La altura máxima es heightScale

            // Pasar las alturas al material
            terrainMaterial.SetFloat("_MinHeight", minHeight);
            terrainMaterial.SetFloat("_MaxHeight", maxHeight);

            // Crear una textura a partir del gradiente y pasarla al material
            Texture2D gradientTexture = CreateGradientTexture(gradient);
            terrainMaterial.SetTexture("_GradientTexture", gradientTexture);
        }
    }
    
    Texture2D CreateGradientTexture(Gradient gradient)
    {
        // Crear una textura 1D a partir del gradiente
        Texture2D texture = new Texture2D(256, 1, TextureFormat.RGBA32, false);
        for (int i = 0; i < 256; i++)
        {
            float t = i / 255f;
            Color color = gradient.Evaluate(t);
            texture.SetPixel(i, 0, color);
        }
        texture.Apply();
        return texture;
    }
}