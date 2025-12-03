using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonPulse : MonoBehaviour
{
    public float speed = 3f;
    public float scaleAmount = 0.05f;

    Vector3 baseScale;

    void Start()
    {
        baseScale = transform.localScale;
    }

    void Update()
    {
        float s = 1 + Mathf.Sin(Time.time * speed) * scaleAmount;
        transform.localScale = baseScale * s;
    }
}

