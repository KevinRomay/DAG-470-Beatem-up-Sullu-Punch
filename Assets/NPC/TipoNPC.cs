using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TipoNPC : MonoBehaviour
{
    public enum Tipo
    {
        Normal,
        Tienda,
        Historia,
        MisionSecundaria
    }

    [Header("Datos del NPC")]
    [SerializeField] private Tipo tipoNPC = Tipo.Normal;
    [SerializeField] private string nombreNPC = "NPC sin nombre";
    [SerializeField] private GameObject iconoInteraccion;

    [Header("Di�logo del NPC")]
    [TextArea(2, 6)]
    [SerializeField] private string[] lineasDialogo;

    private void Awake()
    {
        // Asegura que el icono est� desactivado al iniciar
        if (iconoInteraccion != null)
            iconoInteraccion.SetActive(false);
    }

    // Propiedades p�blicas (para que DetectorDialogo y ManagerDialogo accedan)
    public Tipo TipoDeNPC => tipoNPC;
    public string Nombre => nombreNPC;
    public string[] Lineas => lineasDialogo;
    public GameObject IconoInteraccion => iconoInteraccion;
}
