using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="InfoArmas", menuName = "Armas/InfoArmas")]

public class ArmaScriptable : ScriptableObject
{
    public List<InfoArma> infoArmas;
}
