using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public void IrAlMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
    public void IrAPantallaCarga()
    {
        SceneManager.LoadScene("PantallaDeCarga");
    }
    public void IrAPruebasTrancisiones()
    {
        SceneManager.LoadScene("PruebaTransiciones");
    }
}
