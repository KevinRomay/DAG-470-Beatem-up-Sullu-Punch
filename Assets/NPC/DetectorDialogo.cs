using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorDialogo : MonoBehaviour
{
    [SerializeField] private GameObject Iconointeraccion; // Guardaremos el icono del NPC actual
    private GameObject npcCercano;
    //[SerializeField] private Collider2D colliderDetectarNPC;
    //private bool JugadorCerca;

    private void OnTriggerEnter2D(Collider2D other)
    {
  
        if (other.CompareTag("NPCDialogo"))
        {
            npcCercano = other.gameObject;
            Iconointeraccion.SetActive(true); // Muestra el icono
            Debug.Log("NPC detectado: " + npcCercano.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPCDialogo"))
        {
            Iconointeraccion.SetActive(false); // Oculta el icono
            npcCercano = null;
            Debug.Log("Ya no estás detectando al NPC");
        }
    }

    private void Update()
    {

        if (npcCercano != null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interacción iniciada con: " + npcCercano.name);
        }
    }
}
