using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumibleItem : ItemBase
{
    [Header("Consumible Effect")]
    [SerializeField]public float speedBoost = 1.5f;
    [SerializeField]public float duration = 5f;

    public override void OnPickup(GameObject player)
    {
        PlayerItemHandler handler = player.GetComponent<PlayerItemHandler>();

        if (handler != null)
        {
            handler.ConsumeItem(this);
        }

        Destroy(gameObject);
    }
}
