using UnityEngine;
using UnityEngine.AI; // Necesario para usar NavMeshAgent

public class PlayerClickMovement : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        // Obtener el componente NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Detectar clic del ratón
        if (Input.GetMouseButtonDown(0)) // 0 es el botón izquierdo del ratón
        {
            // Lanzar un rayo desde la cámara hacia el punto del clic
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Mover al jugador hacia la posición del clic
                agent.SetDestination(hit.point);
            }
        }
    }
}