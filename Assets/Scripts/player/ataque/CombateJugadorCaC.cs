using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // añadido para InputAction y CallbackContext

public class CombateJugadorCaC : MonoBehaviour
{
	// Renombradas las referencias públicas para que coincidan con el uso en OnEnable/OnDisable
	public InputActionReference punchAction;
	public InputActionReference kickAction;

	public Transform punchPoint;
	public Transform kickPoint;
	public float radius = 0.5f;
	public LayerMask enemyLayer;

	private void OnEnable()
	{
		// comprobaciones null para evitar excepciones en tiempo de ejecución
		if (punchAction != null && punchAction.action != null)
			punchAction.action.performed += DoPunch;
		if (kickAction != null && kickAction.action != null)
			kickAction.action.performed += DoKick;
	}

	private void OnDisable()
	{
		if (punchAction != null && punchAction.action != null)
			punchAction.action.performed -= DoPunch;
		if (kickAction != null && kickAction.action != null)
			kickAction.action.performed -= DoKick;
	}

	void DoPunch(InputAction.CallbackContext ctx)
	{
		Collider2D hit = Physics2D.OverlapCircle(punchPoint.position, radius, enemyLayer);
		if (hit)
		{
			Debug.Log("Golpe de puño al enemigo: " + hit.name);
		}
	}

	void DoKick(InputAction.CallbackContext ctx)
	{
		Collider2D hit = Physics2D.OverlapCircle(kickPoint.position, radius, enemyLayer);
		if (hit)
		{
			Debug.Log("Patada al enemigo: " + hit.name);
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (punchPoint)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(punchPoint.position, radius);
		}

		if (kickPoint)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(kickPoint.position, radius);
		}
	}
	void Start()
	{
		// ...existing code...
	}

	// Update is called once per frame
	void Update()
	{
		// ...existing code...
	}
}
