using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaSubmarino : MonoBehaviour {

    // Use this for initialization
    private Jugador jugador;
    public PoolPelota poolBalas;
    private PoolObject poolObject;
    public GameObject generadorBala;
    public float dilay;
    private float auxDilay;
    private bool disparar;
    void Start() {
        if (Jugador.instanciaJugador != null)
        {
            jugador = Jugador.instanciaJugador;
        }
        auxDilay = dilay;
        disparar = false;
	}
	
	// Update is called once per frame
	void Update () {
        Movimiento();
        CheckDisparo();
    }
    public void Disparar()
    {
        if (disparar)
        {
            if (poolBalas.GetId() < poolBalas.count)
            {
                GameObject go = poolBalas.GetObject();
                PelotaEnemigo pelota = go.GetComponent<PelotaEnemigo>();
                go.transform.position = generadorBala.transform.position;
                go.transform.rotation = generadorBala.transform.rotation;
                pelota.Disparar();
            }
        }
    }
    public void CheckDisparo()
    {
        if (dilay > 0)
        {
            dilay = dilay - Time.deltaTime;
        }
        if (dilay <= 0)
        {
            Disparar();
            dilay = auxDilay;
        }
    }
    public void Movimiento()
    {
        if (jugador != null)
        {
            if (disparar)
            {
                transform.LookAt(new Vector3(jugador.transform.position.x, jugador.transform.transform.position.y, jugador.transform.position.z));
            }
        }
    }
    public void SetDisparar(bool _disparar)
    {
        disparar = _disparar;
    }
    public bool GetDisparar()
    {
        return disparar;
    }
}
