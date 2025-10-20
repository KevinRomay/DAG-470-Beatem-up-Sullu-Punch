using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PlayerItemHandler : MonoBehaviour
{
    private WeaponItem armaActual;
    private MovimientoJugadorItem movimientoJugador;

    private void Awake()
    {
        movimientoJugador = GetComponent<MovimientoJugadorItem>();
    }

    public void EquipWeapon(WeaponItem arma)
    {
        armaActual = arma;
        Debug.Log("Has recogido un arma: " + arma.itemName + " (Daño: " + arma.damage + ")");
        // Aquí puedes cambiar el sprite o animación del jugador
    }

    public void ConsumeItem(ConsumibleItem item)
    {
        Debug.Log("Has consumido: " + item.itemName);
        movimientoJugador.ModificarVelocidad(item.speedBoost, item.duration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemBase item = collision.GetComponent<ItemBase>();
        if (item != null)
        {
            item.OnPickup(gameObject);
        }
    }
}
