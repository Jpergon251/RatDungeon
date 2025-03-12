using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public Vector2Int terrainSize = new Vector2Int(1000, 1000); // Tamaño total del terreno
    public Vector2Int chunkSize = new Vector2Int(250, 250); // Tamaño de cada chunk

    public float noiseScale = 20f;
    public float heightMultiplier = 10f;
    public int octaves = 4;
    public float persistence = 0.5f;
    public float lacunarity = 2f;
    public Gradient gradient;

    void Start()
    {
        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        int chunksX = terrainSize.x / chunkSize.x;
        int chunksY = terrainSize.y / chunkSize.y;

        for (int x = 0; x < chunksX; x++)
        {
            for (int y = 0; y < chunksY; y++)
            {
                GameObject chunkObject = new GameObject($"Chunk_{x}_{y}");
                chunkObject.transform.position = new Vector3(x * chunkSize.x, 0, y * chunkSize.y);

                TerrainChunk chunk = chunkObject.AddComponent<TerrainChunk>();
                chunk.chunkSize = chunkSize;
                chunk.chunkPosition = new Vector2Int(x, y);

                chunk.GenerateChunk(noiseScale, heightMultiplier, octaves, persistence, lacunarity, gradient);
            }
        }
        Debug.Log("Terrain generation finished");
    }
}
