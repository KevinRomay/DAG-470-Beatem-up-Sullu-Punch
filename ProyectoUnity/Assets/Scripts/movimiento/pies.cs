using UnityEngine;

public class LimitarPies : MonoBehaviour
{
    [SerializeField] private float limiteY = -0.1f; // Límite superior opcional
    [SerializeField] private Transform groundCheck; // Referencia a los pies

    void LateUpdate()
    {
        Vector3 pos = transform.position;

        // Límites visibles de la cámara
        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        // Offset entre el pivot del personaje y sus pies
        float offset = transform.position.y - groundCheck.position.y;

        // --- Limitar en X ---
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);

        // --- Limitar en Y con los pies ---
        float pies = groundCheck.position.y;
        pies = Mathf.Clamp(pies, min.y, Mathf.Min(max.y, limiteY));

        // Ajustar la posición del personaje en base a los pies
        pos.y = pies + offset;

        // Aplicar la posición corregida
        transform.position = pos;
    }
}
