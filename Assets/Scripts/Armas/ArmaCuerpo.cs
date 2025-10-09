using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaCuerpo : Arma, IDañable, IRecogible
{
    public override void Atacar()
    {
        if (infoArma.durabilidad > 0)
        {
            Debug.Log($"ATACANDO CUERPO A CUERPO CON {infoArma.nombre}, con daño {infoArma.daño}");
            //ActivarColliderGolpe();
        }
    }

    public void Recoger()
    {
        //verificar si el arma se puede recoger;
        //verificar si no tengo arma en la mano;
        Debug.Log($"Has recogirido el arma {infoArma.nombre}");
        //Añadir arma a la mano;
        //Desactivar arma en el mundo;
    }

    public void Dañar()
    {
        //Reproducir animacion de rotura;
        //Reproducir sonido de rotura;
        //Desactivar arma;
    }
}
