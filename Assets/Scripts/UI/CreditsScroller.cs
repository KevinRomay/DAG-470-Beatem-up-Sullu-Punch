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
    private bool started = false;

    void Update()
    {
        // Si creditosObj está activo y aún no hemos empezado
        if (creditosObj.activeSelf && !started)
        {
            // Iniciar posición
            Vector2 pos = creditos.anchoredPosition;
            pos.y = startY;
            creditos.anchoredPosition = pos;

            scrolling = true;
            started = true; // Marcamos que ya empezó
        }

        if (!scrolling) return;

        // Movimiento suave hacia arriba
        Vector2 posScroll = creditos.anchoredPosition;
        posScroll.y = Mathf.MoveTowards(posScroll.y, endY, scrollSpeed * Time.deltaTime);
        creditos.anchoredPosition = posScroll;

        // Cuando llega al final
        if (posScroll.y >= endY)
        {
            scrolling = false;
            started = false; // Reiniciamos para la próxima vez

            // Apagar créditos
            creditosObj.SetActive(false);

            // Encender menú principal
            menuPrincipal.SetActive(true);
        }
    }
}



