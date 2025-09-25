using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaCuerpo : Arma
{
    public override void Atacar()
    {
        if (infoArma.durabilidad > 0)
        {
            Debug.Log($"ATACANDO CUERPO A CUERPO CON {infoArma.nombre}, con daño {infoArma.daño}");
            //ActivarColliderGolpe();
        }
    }

   
}
