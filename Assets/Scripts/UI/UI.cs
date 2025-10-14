using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    float tiempoRestante = 300f; // Tiempo total en segundos
    bool tiempoAgotado = false;
    float tiempoUltimaActualizacion = 0f;

    public Text textoTiempo; // Referencia al texto UI del tiempo
    public Color colorNormal = Color.white;
    public Color colorAdvertencia = Color.red;
    public float umbralAdvertencia = 30f;

    public void ControlarTiempoNivel()
    {
        if (tiempoAgotado) return;

        // Reducir el tiempo restante según el tiempo real transcurrido
        tiempoRestante -= Time.deltaTime;

        // Evitar valores negativos
        if (tiempoRestante <= 0)
        {
            tiempoRestante = 0;
            tiempoAgotado = true;

            // Mostrar mensaje visual o manejar evento de fin de tiempo
            textoTiempo.text = "00:00";
            textoTiempo.color = colorAdvertencia;
            Debug.Log("⏰ Tiempo agotado — fin del nivel o penalización.");
            return;
        }

        // Actualizar visualmente solo una vez por segundo (optimización)
        if (Time.time - tiempoUltimaActualizacion >= 1f)
        {
            tiempoUltimaActualizacion = Time.time;

            int minutos = Mathf.FloorToInt(tiempoRestante / 60);
            int segundos = Mathf.FloorToInt(tiempoRestante % 60);

            textoTiempo.text = $"{minutos:00}:{segundos:00}";

            // Cambiar color de advertencia si queda poco tiempo
            textoTiempo.color = (tiempoRestante <= umbralAdvertencia)
                ? colorAdvertencia
                : colorNormal;
        }
    }
    public void Puntuar()
    {
        // Otorgar puntos al jugador.
        // Calcular la cantidad de puntos base según el tipo de enemigo, ítem o acción realizada.
        // Aplicar multiplicadores de combo o bonificaciones especiales.
        // Actualizar el marcador de puntos en la interfaz de usuario (HUD).
        // Reproducir texto flotante con la cantidad de puntos ganados.
        // Reproducir sonido de confirmación o victoria asociado al puntaje.
    }
    // Start is called before the first frame update
    void Start()
    {
        ControlarTiempoNivel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
