using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PlayerItemHandler : MonoBehaviour
{
    private WeaponItem armaActual;
    private MovimientoJugadorItem movimientoJugador;
    private ItemBase itemCercano; // referencia al ítem más cercano

    void Awake()
    {
        movimientoJugador = GetComponent<MovimientoJugadorItem>();
    }

    void Update()
    {
        // Detecta si el jugador presiona E
        if (itemCercano != null && Input.GetKeyDown(KeyCode.E))
        {
            RecogerItem();
        }
    }

    private void RecogerItem()
    {
        if (itemCercano == null) return;

        // Activa la acción del ítem
        itemCercano.OnPickup(gameObject);

        // Limpia la referencia para evitar recoger dos veces
        itemCercano = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemBase item = collision.GetComponent<ItemBase>();
        if (item != null)
        {
            itemCercano = item;
            Debug.Log($"Te acercaste a un ítem: {item.itemName} (Presiona E para recoger)");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ItemBase item = collision.GetComponent<ItemBase>();
        if (item != null && item == itemCercano)
        {
            itemCercano = null;
            Debug.Log($"Te alejaste de {item.itemName}");
        }
    }

    public void EquipWeapon(WeaponItem arma)
    {
        armaActual = arma;
        Debug.Log("Has recogido un arma: " + arma.itemName + " (Daño: " + arma.damage + ")");
    }

    public void ConsumeItem(ConsumibleItem item)
    {
        Debug.Log("Has consumido: " + item.itemName);
        movimientoJugador.ModificarVelocidad(item.speedBoost, item.duration);
    }
}
