using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoBounce : MonoBehaviour
{
    public float speed = 2f;
    public float intensity = 0.03f;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float s = 1 + Mathf.Sin(Time.time * speed) * intensity;
        transform.localScale = originalScale * s;
    }
}
