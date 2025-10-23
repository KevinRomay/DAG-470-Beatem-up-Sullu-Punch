using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ControladorEnemigo;

public class AnimacionEnemigo : MonoBehaviour
{
    [SerializeField] Animator anim;

    public void CambiarAnimacion(string estado)
    {
        if (anim == null) return;

        switch (estado)
        {
            case "Idle":
                anim.Play("Idle");
                break;
            case "Patrullando":
                anim.Play("Walk");
                break;
            case "Persiguiendo":
                anim.Play("Walk");
                break;
            case "Atacando":
                anim.Play("Punch");
                break;
            case "Herido":
                anim.Play("Hurt");
                break;
            case "Muerto":
                anim.Play("Hurt");
                break;
            default:
                anim.Play("Idle");
                break;
        }
    }

}
