using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFloat : MonoBehaviour
{
    public float amplitude = 10f;
    public float speed = 0.2f;

    private RectTransform rt;
    private float startX;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        startX = rt.anchoredPosition.x;
    }

    void Update()
    {
        rt.anchoredPosition = new Vector2(
            startX + Mathf.Sin(Time.time * speed) * amplitude,
            rt.anchoredPosition.y
        );
    }
}
