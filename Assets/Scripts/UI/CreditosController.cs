using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditosController : MonoBehaviour
{
    [Header("Referencias")]
    public RectTransform textoCreditos;

    [Header("Configuración")]
    public float velocidad = 50f;
    public float alturaFinal = 1080f;
    public string escenaMenu = "MenuPrincipal";

    private bool terminado = false;

    void Update()
    {
        if (!terminado && textoCreditos != null)
        {
            // mover el texto hacia arriba con el tiempo
            textoCreditos.anchoredPosition += Vector2.up * velocidad * Time.deltaTime;

            // cuando alcance la altura final, cambiar de escena
            if (textoCreditos.anchoredPosition.y >= alturaFinal)
            {
                terminado = true;
                StartCoroutine(VolverAlMenu());
            }
        }
    }

    System.Collections.IEnumerator VolverAlMenu()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(escenaMenu);
    }
}

