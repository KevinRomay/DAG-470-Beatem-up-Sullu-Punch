using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaDistancia : Arma
{
    public override void Atacar()
    {
        if (infoArma.durabilidad > 0)
        {
            Debug.Log($"ATACANDO A DISTANCIA CON {infoArma.nombre}, con da�o {infoArma.da�o}");
            //InstanciarProyectil();
        }
        else
        {
            Debug.Log("Arma inusable, destruyendo");
        }
    }
}
