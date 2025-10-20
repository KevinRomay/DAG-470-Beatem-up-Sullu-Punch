//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;

//public class ManagerDialogo : MonoBehaviour
//{
//    public static ManagerDialogo Instance { get; private set; }

//    [SerializeField] private GameObject panelDialogo;
//    [SerializeField] private TMP_Text nombreNPCText;
//    [SerializeField] private TMP_Text textoDialogoText;

//    private Queue<string> colaDialogos = new Queue<string>();
//    private bool dialogoActivo = false;

//    private void Awake()
//    {
//        Instance = this;
//    }

//    public void IniciarDialogo(TipoNPC npc)
//    {
//        if (npc == null || npc.Lineas.Length == 0) return;

//        panelDialogo.SetActive(true);
//        nombreNPCText.text = npc.Nombre;

//        colaDialogos.Clear();
//        foreach (string linea in npc.Lineas)
//        {
//            colaDialogos.Enqueue(linea);
//        }

//        dialogoActivo = true;
//        MostrarSiguienteLinea();
//    }

//    public void MostrarSiguienteLinea()
//    {
//        if (colaDialogos.Count == 0)
//        {
//            CerrarDialogo();
//            return;
//        }

//        string lineaActual = colaDialogos.Dequeue();
//        textoDialogoText.text = lineaActual;
//    }

//    public void CerrarDialogo()
//    {
//        panelDialogo.SetActive(false);
//        textoDialogoText.text = "";
//        nombreNPCText.text = "";
//        dialogoActivo = false;
//    }

//    public bool DialogoActivo => dialogoActivo;
//}



//hacer de los npc como prefabs
//hacer que los npc puedan dar objetos/items/potenciadores/algo