using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Atributos de vida")]
    public float vidaMax = 100f;
    public float vidaActual;

    [Header("UI")]
    public Image barraDeVida;

    void Start()
    {
        vidaActual = vidaMax;
        ActualizarBarra();
    }

    public void RecibirDaño(float cantidad)
    {
        vidaActual -= cantidad;
        if (vidaActual < 0) vidaActual = 0;
        ActualizarBarra();

        if (vidaActual == 0)
        {
            Debug.Log("¡El jugador ha muerto!");
        }
    }

    void ActualizarBarra()
    {
        if (barraDeVida != null)
        {
            barraDeVida.fillAmount = vidaActual / vidaMax;
        }
    }
}
