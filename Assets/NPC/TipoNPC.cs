using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class TopoPC : MonoBehaviour
{
    //enum TIPO NPC
    //nombre del npc
    //iconointeraccion
    //Generador de dialogos del npc

    public enum TipoNPC
    {
        Normal,
        Tienda,
        Historia,
        MisionSecundaria
    }

    public TMP_Text textodialogo;

    [Header("Datos del NPC")]
    [SerializeField] private TipoNPC tipoNPC;
    [SerializeField] private string nombreNPC = "NPC sin nombre";

    [SerializeField] private GameObject iconoInteraccion;

    [Header("Diálogo del NPC")]
    [TextArea(2, 5)]
    [SerializeField] private string[] lineasDialogo; // Puedes escribir las frases directo en el Inspector

    // Update is called once per frame
    void Update()
    {
        textodialogo.text = lineasDialogo[0];
        }
}
