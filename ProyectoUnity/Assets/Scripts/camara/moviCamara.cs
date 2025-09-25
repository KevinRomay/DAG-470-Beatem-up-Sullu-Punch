using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float speed = 2f;          // Velocidad del movimiento
    private bool canMove = false;     // Control del movimiento
    private Vector3 targetPosition;   // Hacia dónde se moverá
    private bool isMoving = false;

    private CountdownTimer countdown; // Referencia al contador

    void Start()
    {
        countdown = FindObjectOfType<CountdownTimer>(); // Buscamos el contador en la escena
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // ✅ Si llegó al destino, se detiene y reinicia el contador
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
                if (countdown != null)
                {
                    countdown.ResetCountdown();
                }
            }
        }
    }

    public void StartMoving()
    {
        // Calculamos el tamaño de la cámara en X
        float cameraWidth = Camera.main.orthographicSize * 0.8f * Camera.main.aspect;

        // Definimos la nueva posición meta
        targetPosition = transform.position + new Vector3(cameraWidth, 0, 0);
        isMoving = true;
    }
}
