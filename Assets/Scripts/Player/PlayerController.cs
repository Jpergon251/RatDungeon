using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public float life;
        public int currentLevel = 1; // Nivel actual del jugador
        public int currentXP = 0; // Experiencia actual
        public int xpToNextLevel = 100; // Experiencia necesaria para el siguiente nivel

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

        // Método para agregar experiencia
        public void AddXP(int xpAmount)
        {
            currentXP += xpAmount;
            Debug.Log($"¡Has ganado {xpAmount} de experiencia! Experiencia total: {currentXP}");

            // Verificar si el jugador sube de nivel
            if (currentXP >= xpToNextLevel)
            {
                LevelUp();
            }
        }

        // Método para subir de nivel
        void LevelUp()
        {
            currentLevel++;
            currentXP = 0; // Reiniciar la experiencia para el siguiente nivel
            xpToNextLevel = CalculateXPForNextLevel(); // Calcular la experiencia necesaria para el siguiente nivel

            Debug.Log($"¡Has subido al nivel {currentLevel}!");
        }

        // Método para calcular la experiencia necesaria para el siguiente nivel
        int CalculateXPForNextLevel()
        {
            // Fórmula simple: incremento exponencial
            return xpToNextLevel * 2; // Por ejemplo, duplicar la experiencia necesaria cada nivel
        }
    }
}