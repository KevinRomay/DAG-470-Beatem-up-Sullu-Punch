using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    public float animationDuration = 1.40f;  // Duración de 1 ciclo
    public int cycles = 5;                   // Veces que quieres repetir
    public string nextScene = "PruebaTransiciones"; // Tu escena de destino

    private void Start()
    {
        StartCoroutine(LoadAfterCycles());
    }

    IEnumerator LoadAfterCycles()
    {
        float totalTime = animationDuration * cycles;
        yield return new WaitForSeconds(totalTime);

        SceneManager.LoadScene(nextScene);
    }
}

