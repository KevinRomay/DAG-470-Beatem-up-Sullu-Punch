using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControlSFX : MonoBehaviour
{
    public static ControlSFX Instance;
    public List<ListaSonidos> listaSonidos;

    [Range(0f,0.2f)]
    public float variacionPitch;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InstanciarLista();
    }

    private void InstanciarLista()
    {
        foreach (var sonido in listaSonidos)
        {
            AudioSource nuevoParlante = gameObject.AddComponent<AudioSource>();
            nuevoParlante.playOnAwake = false;
            nuevoParlante.clip = sonido.clip;
            sonido.parlante = nuevoParlante;
        }
    }

    public void ReproducirSonido(string nombreSonido)
    {
        foreach (var sonido in listaSonidos)
        {
            if (sonido.nombre.Equals(nombreSonido))
            {
                sonido.parlante.pitch = 1 + Random.Range(-variacionPitch, variacionPitch);
                sonido.parlante.Play();
                break;
            }
        }
    }
}

[System.Serializable]
public class ListaSonidos
{
    public string nombre;
    public AudioClip clip;
    public AudioSource parlante;
}
