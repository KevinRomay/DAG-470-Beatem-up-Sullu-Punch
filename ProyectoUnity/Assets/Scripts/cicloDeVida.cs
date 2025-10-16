using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cicloDeVida : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Estoy en start");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0,1));
        transform.Translate(new Vector2(0.025f,0));
    }

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        Debug.Log("Estoy en OnEnable");
    }

    void OnDisable()
    {
        Debug.Log("Estoy en OnDisable");
    }

    void OnDestroy()
    {
        Debug.Log("Estoy en OnDestroy");
    }

    private void FixedUpdate()
    {
        //Debug.Log("Estoy en FixedUpdate");
    }
    private void LateUpdate()
    {
        //Debug.Log("Estoy en LateUpdate");
    }
}
