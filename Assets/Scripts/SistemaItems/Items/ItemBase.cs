using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] public string itemName;
    private Sprite Icon;
    [SerializeField] public float lifeTime = 10f; // los segundos que permance en el mapa
    [SerializeField]float timer;

    void Start()
    {
        timer = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 5f)
        {
            float alpha = Mathf.PingPong(Time.time * 5f, 1f);
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
            //Destroy(gameObject); // desaparece despues del tiempo
        }
        if (timer < 0f)
        {
            Destroy(gameObject);
        }
    }

    public abstract void OnPickup(GameObject player);
}
