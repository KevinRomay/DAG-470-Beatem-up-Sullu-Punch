using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEscala : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 escalaNormal = Vector3.one;
    public Vector3 escalaHover = new Vector3(1.15f, 1.15f, 1f);
    public float velocidad = 8f;

    private bool hover;

    void Update()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            hover ? escalaHover : escalaNormal,
            Time.deltaTime * velocidad
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hover = false;
    }
}

