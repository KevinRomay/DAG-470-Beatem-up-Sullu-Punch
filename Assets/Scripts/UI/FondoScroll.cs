using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoScroll : MonoBehaviour
{
    public RectTransform fondo1;
    public RectTransform fondo2;
    public float velocidad = 50f;

    private float ancho;

    void Start()
    {
        ancho = fondo1.rect.width;
    }

    void Update()
    {
        // Mover los fondos hacia la izquierda
        fondo1.anchoredPosition += Vector2.left * velocidad * Time.deltaTime;
        fondo2.anchoredPosition += Vector2.left * velocidad * Time.deltaTime;

        // Si un fondo salió completamente por la izquierda…
        if (fondo1.anchoredPosition.x <= -ancho)
            fondo1.anchoredPosition = new Vector2(fondo2.anchoredPosition.x + ancho, fondo1.anchoredPosition.y);

        if (fondo2.anchoredPosition.x <= -ancho)
            fondo2.anchoredPosition = new Vector2(fondo1.anchoredPosition.x + ancho, fondo2.anchoredPosition.y);
    }
}


