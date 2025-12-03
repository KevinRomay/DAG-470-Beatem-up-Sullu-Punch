using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScroller : MonoBehaviour
{
    public RectTransform creditos;      // El panel de créditos
    public float startY = -4064f;       // Posición inicial Y
    public float endY = 2255f;          // Posición final Y
    public float scrollSpeed = 80f;     // Velocidad de scroll (ajustable)

    public GameObject creditosObj;      // El panel de créditos (para apagar)
    public GameObject menuPrincipal;    // El menú (para encender)

    private bool scrolling = false;

    void OnEnable()
    {
        // Iniciar posición
        Vector2 pos = creditos.anchoredPosition;
        pos.y = startY;
        creditos.anchoredPosition = pos;

        scrolling = true;
    }

    void Update()
    {
        if (!scrolling) return;

        // Movimiento suave hacia arriba
        Vector2 pos = creditos.anchoredPosition;
        pos.y = Mathf.MoveTowards(pos.y, endY, scrollSpeed * Time.deltaTime);
        creditos.anchoredPosition = pos;

        // Cuando llega al final
        if (pos.y >= endY)
        {
            scrolling = false;

            // Apagar créditos
            creditosObj.SetActive(false);

            // Encender menú principal
            menuPrincipal.SetActive(true);
        }
    }
}

