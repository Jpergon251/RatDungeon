using UnityEngine;

public class TerrainChunk : MonoBehaviour
{
    public Vector2Int chunkSize = new Vector2Int(250, 250);
    public Vector2Int chunkPosition; // Posición del chunk en la cuadrícula de chunks

    public void GenerateChunk(float noiseScale, float heightMultiplier, int octaves, float persistence, float lacunarity, Gradient gradient)
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        Vector3[] GenerateVertices()
        {
            Vector3[] vertices = new Vector3[(chunkSize.x + 1) * (chunkSize.y + 1)];
            float minHeight = float.MaxValue;
            float maxHeight = float.MinValue;

            for (int i = 0, z = 0; z <= chunkSize.y; z++)
            {
                for (int x = 0; x <= chunkSize.x; x++)
                {
                    // Calcular la altura usando múltiples octaves
                    float amplitude = 1f;
                    float frequency = 1f;
                    float noiseHeight = 0f;

                    for (int o = 0; o < octaves; o++)
                    {
                        // Coordenadas de ruido
                        float sampleX = (float)x / chunkSize.x * noiseScale * frequency;
                        float sampleZ = (float)z / chunkSize.y * noiseScale * frequency;

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
            int[] triangles = new int[chunkSize.x * chunkSize.y * 6];

            for (int z = 0, vert = 0, tris = 0; z < chunkSize.y; z++)
            {
                for (int x = 0; x < chunkSize.x; x++)
                {
                    triangles[tris + 0] = vert + 0;
                    triangles[tris + 1] = vert + chunkSize.x + 1;
                    triangles[tris + 2] = vert + 1;
                    triangles[tris + 3] = vert + 1;
                    triangles[tris + 4] = vert + chunkSize.x + 1;
                    triangles[tris + 5] = vert + chunkSize.x + 2;

                    vert++;
                    tris += 6;
                }
                vert++;
            }

            return triangles;
        }
       

        // Generar vértices y colores (similar a tu código actual)
        // Asegúrate de ajustar las coordenadas para que cada chunk se coloque correctamente en la escena.

        mesh.vertices = GenerateVertices();
        mesh.triangles = GenerateTriangles();
        
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        Color[] colors = new Color[mesh.vertices.Length];
        mesh.colors = colors;
        // Añadir MeshCollider
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
    }
}