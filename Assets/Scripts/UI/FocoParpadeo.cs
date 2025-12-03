using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocoParpadeo : MonoBehaviour
{
    public Image imagenFoco;
    public float minIntensidad = 0.6f;
    public float maxIntensidad = 1f;
    public float velocidadCambio = 8f;

    private float objetivo;

    void Start()
    {
        objetivo = Random.Range(minIntensidad, maxIntensidad);
    }

    void Update()
    {
        float alphaActual = imagenFoco.color.a;

        alphaActual = Mathf.Lerp(alphaActual, objetivo, Time.deltaTime * velocidadCambio);

        imagenFoco.color = new Color(1f, 1f, 1f, alphaActual);

        if (Mathf.Abs(alphaActual - objetivo) < 0.05f)
        {
            objetivo = Random.Range(minIntensidad, maxIntensidad);
        }
    }
}

