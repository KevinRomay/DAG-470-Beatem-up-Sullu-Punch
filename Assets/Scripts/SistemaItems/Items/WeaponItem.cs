using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : ItemBase
{
    [Header("Weapon Stats")]
    [SerializeField] public int damage = 10;

    [Header("Visual del arma equipada")]
    public GameObject armaEnManoPrefab; //  Prefab del arma que aparecerá en la mano

    public override void OnPickup(GameObject player)
    {
        PlayerItemHandler handler = player.GetComponent<PlayerItemHandler>();

        if (handler != null)
        {
            handler.EquipWeapon(this);

            //  Mostrar el arma visualmente en la mano del jugador
            Transform puntoArma = player.transform.Find("PuntoArma"); // el punto donde se mostrará el arma
            if (puntoArma != null && armaEnManoPrefab != null)
            {
                // Instanciamos el arma visual como hijo del jugador
                GameObject armaEquipada = Object.Instantiate(armaEnManoPrefab, puntoArma.position, Quaternion.identity, puntoArma);
                armaEquipada.transform.localPosition = Vector3.zero; // Centrar sobre el punto
            }
        }

        //  Destruir el arma del suelo
        Destroy(gameObject);
    }
}
