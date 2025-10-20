using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI mensajeRecoger;

    public void MostrarMensaje(string texto)
    {
        mensajeRecoger.text = texto;
        mensajeRecoger.gameObject.SetActive(true);
    }

    public void OcultarMensaje()
    {
        mensajeRecoger.gameObject.SetActive(false);
    }
}
