using UnityEngine;

public abstract class Arma : MonoBehaviour
{
    public InfoArma infoArma;

    public virtual void Atacar() { }
}

[System.Serializable]
public class InfoArma{
    public string nombre;
    public float daño;
    public TipoArma tipoArma;
    public int durabilidad;
    public float tiempoEspera;
    public float cadencia;
    public GameObject prefabArma;
    public bool enUso;
}

public enum TipoArma
{
    CuerpoACuerpo, Distancia,
}
