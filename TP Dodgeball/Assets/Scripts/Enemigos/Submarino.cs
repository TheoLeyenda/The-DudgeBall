using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarino : Enemigo{

    // Use this for initialization
    /*
Patrullar: patrulla moviéndose por los distintos waypoints (no tiene activo
Su punto débil)

PatrullarBulnerable: en este estado el submarino patrulla pero tiene su
Punto débil activo.

PatrullarDisparando: el jugador mientras pasa por los distintos waypoints
Dispara balas hacia el jugador (tiene activa su punto débil)

Seguir: el submarino sigue al jugador mientras dispara torpedos y 
Si el jugador esta por el costado del submarino le dispara balas
(Tiene activo su punto débil)

AtacarTorpedos: el submarino pasa a este estado cuando pasa 
Por un waypoints especifico (tiene activo su punto débil), dispara sus 
Torpedos.

     */
    public enum States
    {
        Patrullar = 0,
        PatrullarBulnerable,
        PatrullarDisparando,
        Seguir,
        AtacarTorpedos,
        Quieto,
    }

    public PoolPelota pool;
    public float VelocidadMov;
    public Transform[] waypoints;
    public States estados;

    private int id;
    private bool puntoDebilActivado;
    private PoolObject poolObject;
    private Rigidbody rig;

    public void Prendido()
    {
        //PONER LO MISMO QUE EN EL "START();".
    }
    void Start () {
        id = 0;
        poolObject = GetComponent<PoolObject>();
        efectoCongelado.SetActive(false);
        efectoQuemado.SetActive(false);
        efectoMusica.SetActive(false);
        rig = GetComponent<Rigidbody>();
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;
        SetEstadoEnemigo(EstadoEnemigo.normal);
    }
	
	// Update is called once per frame
	void Update () {
        updateHP();
        UpdateStates();
    }

    public void UpdateStates()
    {
        switch ((int)estados)
        {
            case (int)States.Patrullar:
                Patrullar();
                break;
            case (int)States.PatrullarBulnerable:
                PatrullarBulnerable();
                break;
            case (int)States.PatrullarDisparando:
                PatrullarDisparando();
                break;
            case (int)States.Seguir:
                Seguir();
                break;
            case (int)States.AtacarTorpedos:
                AtacarTorpedos();
                break;
        }
    }

    //------------------TENGO QUE HACER QUE SI TOCA CIERTO WAYPOINT PASE DE ESTADO A ESTADO Y CADA VEZ QUE TOQUE UN WAYPOINT HAGA "id++".-----------------------------//

    //Patrullar: patrulla moviéndose por los distintos waypoints(no tiene activo
    //Su punto débil)
    // TAG PARA ENTRAR EN "Patrullar()" = "WaypointPatrullaje"
    public void Patrullar()
    {
        Debug.Log("Patrullar");
        puntoDebilActivado = false;
        
    }

    //PatrullarBulnerable: en este estado el submarino patrulla pero tiene su
    //Punto débil activo.
    // TAG PARA ENTRAR EN "PatrullarBulnerable()" = "WaypointPatrullarBulnerable"
    public void PatrullarBulnerable()
    {
        Debug.Log("PatrullarBulnerable");
        puntoDebilActivado = true;
    }

    //PatrullarDisparando: el jugador mientras pasa por los distintos waypoints
    //Dispara balas hacia el jugador(tiene activa su punto débil)
    //TAG PARA ENTRAR EN "PatrullaDisparando()" = "WaypointPatrullarDisparando"
    public void PatrullarDisparando()
    {
        Debug.Log("PatrullarDisparando");
        puntoDebilActivado = true;
    }

    //AtacarTorpedos: el submarino pasa a este estado cuando pasa
    //Por un waypoints especifico(tiene activo su punto débil), dispara sus
    //Torpedos.
    //TAG PARA ENTRAR EN "AtacarTorpedos()" = "WaypointAtacarTorpedos"
    public void AtacarTorpedos()
    {
        Debug.Log("AtacarTorpedos");
        puntoDebilActivado = true;
    }

    //Seguir: el submarino sigue al jugador mientras dispara torpedos y
    //Si el jugador esta por el costado del submarino le dispara balas
    //(Tiene activo su punto débil)
    //Para entrar en "Seguir()" CONFIGURARLO PARA QUE ENTRE CUANDO ESTES EN MODO "SUPERVIVENCIA"
    public void Seguir()
    {
        Debug.Log("Seguir");
        puntoDebilActivado = true;
    }
}
