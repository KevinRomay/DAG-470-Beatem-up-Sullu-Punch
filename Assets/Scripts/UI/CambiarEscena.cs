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
    public void IrAModoDeJuego()
    {
        SceneManager.LoadScene("ModoDeJuego");
    }
    public void IrANiveles()
    {
        SceneManager.LoadScene("Niveles");
    }
    public void IrASeleccionarPersonaje()
    {
        SceneManager.LoadScene("SeleccionPersonajes");
    }
    public void IrAPantallaCarga()
    {
        SceneManager.LoadScene("PantallaDeCarga");
    }
    public void IrAOpciones()
    {
        SceneManager.LoadScene("Opciones");
    }
    public void IrACreditos()
    {
        SceneManager.LoadScene("Creditos");
    }
}
