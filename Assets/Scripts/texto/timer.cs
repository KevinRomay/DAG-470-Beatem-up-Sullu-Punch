using UnityEngine;
using TMPro;
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; 
    public int startTime = 30;        
    private int currentTime;

    public CameraMover cameraMover;

    private Coroutine countdownCoroutine;

    private WaitForSeconds tiempoEspera = new WaitForSeconds(1f);

    void Start()
    {
        ResetCountdown();
    }

    IEnumerator StartCountdown()
    {
        while (currentTime > 0)
        {
            timerText.text = currentTime.ToString();
            yield return tiempoEspera;
            currentTime--;
        }

        // Cuando llegue a 0
        timerText.text = "0";
        Debug.Log("¡Tiempo terminado!");

        if (cameraMover != null)
        {
            cameraMover.StartMoving();
        }
    }

    // 🔹 Método para reiniciar el contador
    public void ResetCountdown()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }

        currentTime = startTime;
        countdownCoroutine = StartCoroutine(StartCountdown());
    }
}
