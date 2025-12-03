using UnityEngine;
using UnityEngine.UI;

public class FondoInfinito : MonoBehaviour
{
    public float velocidad = 0.1f;
    private RawImage imagen;

    void Start()
    {
        imagen = GetComponent<RawImage>();
    }

    void Update()
    {
        imagen.uvRect = new Rect(
            imagen.uvRect.x + (velocidad * Time.deltaTime),
            imagen.uvRect.y,
            imagen.uvRect.width,
            imagen.uvRect.height
        );
    }
}


