using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // ← necesario para usar Image

public class PulsingLightUI : MonoBehaviour
{
    public Image luz;        // la imagen UI de la luz
    public float speed = 1.5f;

    private float minAlpha = 0.5f;   // 50%
    private float maxAlpha = 1f;     // 100%

    void Update()
    {
        // valor oscilante entre 0 y 1
        float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, t);

        Color c = luz.color;
        c.a = alpha;
        luz.color = c;
    }
}


