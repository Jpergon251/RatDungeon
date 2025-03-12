using UnityEngine;

public class MeshModifier : MonoBehaviour
{
    public float heightScale = 2.0f; // Escala de altura
    public float noiseScale = 0.1f; // Escala del ruido (controla la suavidad)

    void Start()
    {
        // Obtener el MeshFilter y su malla
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.mesh;

        // Obtener los vértices de la malla
        Vector3[] vertices = mesh.vertices;
        
        Debug.Log(vertices.Length);
    }
}
