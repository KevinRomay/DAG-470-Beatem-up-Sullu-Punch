using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BotonEscalaHoer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Sprite que quiero agrandar")]
    public Transform spriteObjetivo;

    [Header("Escala")]
    public float escalaHover = 1.1f;
    private Vector3 escalaOriginal;

    void Start()
    {
        if (spriteObjetivo != null)
            escalaOriginal = spriteObjetivo.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (spriteObjetivo != null)
            spriteObjetivo.localScale = escalaOriginal * escalaHover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (spriteObjetivo != null)
            spriteObjetivo.localScale = escalaOriginal;
    }
}

