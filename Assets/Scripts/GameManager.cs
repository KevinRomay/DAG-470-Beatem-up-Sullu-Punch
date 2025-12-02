using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;
    public int enemigosDerrotados = 0;
    public int enemigosNecesarios = 4;
    public GameObject menuFinal;
    // Start is called before the first frame update
    private void Awake ()
    {
        instancia = this;
    }

    void Start()
    {
        if (menuFinal != null)
        {
            menuFinal.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegistrarEnemigoDerrotado()
    {
        enemigosDerrotados++;
        if (enemigosDerrotados >= enemigosNecesarios)
        {
            ActivarMenuFinal();
        }
    }

    private void ActivarMenuFinal()
    {
        Time.timeScale = 0f;
        if (menuFinal != null)
        menuFinal.SetActive(true);
    }

    public void Continuar()
    {
        Time.timeScale = 1f;

        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu Principal");
    }
}