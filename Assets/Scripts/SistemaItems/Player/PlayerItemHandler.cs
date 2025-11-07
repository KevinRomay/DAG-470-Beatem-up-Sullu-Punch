using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PlayerItemHandler : MonoBehaviour
{
    private WeaponItem armaActual;
    private MovimientoJugador movimientoJugador;
    private ItemBase itemCercano; // referencia al ítem más cercano
    private GameObject armaVisual; // referencia al arma visual en la mano

    void Awake()
    {
        movimientoJugador = GetComponent<MovimientoJugador>();
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

        // Bloquea movimiento momentáneamente mientras recoge
        movimientoJugador.SetState(MovimientoJugador.MovementState.Busy);

        // Ejecuta la acción del ítem
        itemCercano.OnPickup(gameObject);

        // Limpia la referencia
        itemCercano = null;

        // Libera movimiento nuevamente
        movimientoJugador.ResetState();
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

        // Si ya había un arma visual, eliminarla
        if (armaVisual != null)
            Destroy(armaVisual);

        // Crear una copia visual del arma
        if (arma.weaponPrefab != null)
        {
            armaVisual = Instantiate(arma.weaponPrefab);

            // Buscar el punto de la mano
            Transform puntoMano = transform.Find("PuntoMano");
            if (puntoMano != null)
            {
                armaVisual.transform.SetParent(puntoMano);
                armaVisual.transform.localPosition = Vector3.zero;
                armaVisual.transform.localRotation = Quaternion.identity;
            }
            else
            {
                // Si no existe PuntoMano, la coloca al lado del jugador
                armaVisual.transform.SetParent(transform);
                armaVisual.transform.localPosition = new Vector3(0.5f, 0, 0);
            }
        }
    }

    public void ConsumeItem(ConsumibleItem item)
    {
        Debug.Log("Has consumido: " + item.itemName);
        // Ejemplo: aumentar velocidad temporalmente
        movimientoJugador.velocidad *= item.speedBoost;
        Invoke(nameof(RestaurarVelocidad), item.duration);
    }

    private void RestaurarVelocidad()
    {
        movimientoJugador.velocidad = 5f; // valor base
    }
}