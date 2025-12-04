using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRendererSorter : MonoBehaviour
{
    // --- Variables de Configuración (visibles en el Inspector) ---

    [SerializeField]
    [Tooltip("La base numérica a partir de la cual se calcula el sortingOrder.")]
    private int sortingOrderBase = 5000;

    [SerializeField]
    [Tooltip("Ajuste opcional para mover el objeto ligeramente adelante o atrás en el orden de dibujo.")]
    private int offset = 0;

    [SerializeField]
    [Tooltip("Si es verdadero, el script solo recalcula el orden una vez y luego se destruye. Útil para objetos estáticos.")]
    private bool runOnlyOnce = false;

    // --- Variables Internas ---

    // Temporizador para controlar la frecuencia de la actualización
    private float timer;

    // El intervalo máximo de tiempo entre actualizaciones (0.1 segundos)
    private float timerMax = .1f;

    // Referencia al componente Renderer del objeto
    private Renderer myRenderer;

    // --- Métodos de Ciclo de Vida de Unity ---

    private void Awake()
    {
        // Se llama al inicio. Obtiene el componente Renderer para modificar su orden.
        myRenderer = gameObject.GetComponent<Renderer>();
    }

    private void LateUpdate()
    {
        // Resta el tiempo transcurrido (Time.deltaTime) del temporizador
        timer -= Time.deltaTime;

        // Verifica si es el momento de actualizar el orden (cuando el temporizador llega a cero)
        if (timer <= 0f)
        {
            // Reinicia el temporizador
            timer = timerMax;

            // FÓRMULA CLAVE:
            // El sortingOrder se calcula restando la posición 'y' de la base.
            // Esto asegura que un valor de Y menor (más abajo en la pantalla)
            // resulte en un sortingOrder mayor (dibujado por delante).
            myRenderer.sortingOrder = (int)(sortingOrderBase - transform.position.y - offset);

            // Si está configurado para ejecutarse solo una vez, se destruye el script.
            if (runOnlyOnce)
            {
                Destroy(this);
            }
        }
    }
}
