using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PersonajeIcono : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject personajeRelacionado;
    public GameObject[] todosLosPersonajes;
    public float escalaHover = 1.15f;
    public float velocidad = 8f;

    private Vector3 escalaInicial;
    private bool estaHover = false;

    void Start()
    {
        escalaInicial = transform.localScale;
    }

    void Update()
    {
        Vector3 objetivo = estaHover ? escalaInicial * escalaHover : escalaInicial;
        transform.localScale = Vector3.Lerp(transform.localScale, objetivo, Time.deltaTime * velocidad);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        estaHover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        estaHover = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        foreach (var p in todosLosPersonajes)
            p.SetActive(false);

        personajeRelacionado.SetActive(true);
    }
}

