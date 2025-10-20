using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : ItemBase
{
    [Header("Weapon Stats")]
    [SerializeField] public int damage = 10;

    public override void OnPickup(GameObject player)
    {
        PlayerItemHandler handler = player.GetComponent<PlayerItemHandler>();

        if (handler != null)
        {
            handler.EquipWeapon(this);
        }

        Destroy(gameObject);
    }
}
