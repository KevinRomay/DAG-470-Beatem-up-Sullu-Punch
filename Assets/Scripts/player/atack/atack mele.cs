using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool isJabbing = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && !isJabbing)
        {
            DoJab();
        }
    }

    void DoJab()
    {
        isJabbing = true;
        animator.SetBool("pega", true); // ✅ Activa animación
        Debug.Log("Ejecutando Jab");
    }

    // 🔹 Llamado al final de la animación con Animation Event
    public void EndJab()
    {
        isJabbing = false;
        animator.SetBool("pega", false); // ✅ Vuelve a Idle
        Debug.Log("Jab terminado");
    }
}
