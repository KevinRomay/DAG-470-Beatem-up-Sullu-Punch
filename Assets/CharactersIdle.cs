using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersIdle : MonoBehaviour
{
    public float speed = 1.3f;
    public float intensity = 6f;

    private RectTransform rt;
    private float startY;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        startY = rt.anchoredPosition.y;
    }

    void Update()
    {
        rt.anchoredPosition = new Vector2(
            rt.anchoredPosition.x,
            startY + Mathf.Sin(Time.time * speed) * intensity
        );
    }
}

