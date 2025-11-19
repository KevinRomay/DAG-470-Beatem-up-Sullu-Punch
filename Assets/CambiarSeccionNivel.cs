using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CambiarSeccionNivel : MonoBehaviour
{
    [SerializeField] private Transform posPersonaje;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Image imagenTransicion;

    [SerializeField] private Transform posSalida;
    [SerializeField] private Transform posObjetivo;
    [SerializeField] private MovimientoJugador movJugador;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag.Equals("Player") && movJugador.CurrentState != MovementState.Cinematic)
        {
            movJugador.SetState(MovementState.Cinematic);
            StartCoroutine(RutinaCambioSeccion());
        }
        
    }

    IEnumerator RutinaCambioSeccion()
    {
        Debug.Log("Cambiando escena");
        StartCoroutine(FadeScreen(1));
        yield return new WaitForSeconds(1f);
       
       
        StartCoroutine(MoverASalida());
        yield return new WaitForSeconds(4f);
    }

    IEnumerator MoverASalida()
    {
        while (Vector2.Distance(posPersonaje.position, posSalida.position) > 0.15f)
        {
            Debug.Log("MoviendoPersonaje");
            posPersonaje.position = Vector2.MoveTowards(posPersonaje.position, posSalida.position, 2f * Time.deltaTime);
            yield return null;
        }

        posPersonaje.position = posObjetivo.position;
        StartCoroutine(FadeScreen(-1));
        movJugador.SetState(MovementState.Normal);
        canvas.SetActive(false);
    }
    
    IEnumerator FadeScreen(int direction)
    {
        canvas.SetActive(true);
        if (direction > 0)
        {
            while (imagenTransicion.color.a < 1)
            {
                Color colorActual = imagenTransicion.color;
                colorActual.a += Time.deltaTime / 2;
                imagenTransicion.color = colorActual;
                yield return null;
            }
        }
        else
        {
            while (imagenTransicion.color.a > 0)
            {
                Color colorActual = imagenTransicion.color;
                colorActual.a -= Time.deltaTime / 2;
                imagenTransicion.color = colorActual;
                yield return null;
            }
        }
        
    }
}
