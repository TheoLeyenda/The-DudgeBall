using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaSubmarino : MonoBehaviour {

    // Use this for initialization
    public PoolPelota poolBalas;
    private PoolObject poolObject;
    public GameObject generadorBala;
    public float dilay;
    private float auxDilay;
    private bool disparar;
    void Start () {
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
        if (Jugador.GetJugador() != null)
        {
            if (disparar)
            {
                transform.LookAt(new Vector3(Jugador.GetJugador().transform.position.x, Jugador.GetJugador().transform.transform.position.y, Jugador.GetJugador().transform.position.z));
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
