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

    public PoolPelota poolTorpedos;
    public float dileyDisparoTorpedos;
    public float VelocidadMov;
    public Transform[] waypoints;
    public GameObject[] GeneradorTorpedos;
    public States estados;
    public GameObject particulasBurbujas;

    private int id;
    private float auxDileyDisparoTorpedos;
    private bool puntoDebilActivado;
    private PoolObject poolObject;
    private Rigidbody rig;

    public void Prendido()
    {
        //PONER LO MISMO QUE EN EL "START();".
        id = 0;
        poolObject = GetComponent<PoolObject>();
        if (efectoCongelado != null)
        {
            efectoCongelado.SetActive(false);
        }
        if (efectoQuemado != null)
        {
            efectoQuemado.SetActive(false);
        }
        if (efectoMusica != null)
        {
            efectoMusica.SetActive(false);
        }
        rig = GetComponent<Rigidbody>();
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;
        SetEstadoEnemigo(EstadoEnemigo.normal);
        auxDileyDisparoTorpedos = dileyDisparoTorpedos;
    }
    void Start () {
        id = 0;
        poolObject = GetComponent<PoolObject>();
        if (efectoCongelado != null)
        {
            efectoCongelado.SetActive(false);
        }
        if (efectoQuemado != null)
        {
            efectoQuemado.SetActive(false);
        }
        if (efectoMusica != null)
        {
            efectoMusica.SetActive(false);
        }
        rig = GetComponent<Rigidbody>();
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;
        SetEstadoEnemigo(EstadoEnemigo.normal);
        auxDileyDisparoTorpedos = dileyDisparoTorpedos;
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
            case (int)States.Quieto:
                Quieto();
                break;
        }
    }

    //------------------TENGO QUE HACER QUE SI TOCA CIERTO WAYPOINT PASE DE ESTADO A ESTADO Y CADA VEZ QUE TOQUE UN WAYPOINT HAGA "id++".-----------------------------//

    public void Quieto()
    {
        if (particulasBurbujas != null)
        {
            particulasBurbujas.SetActive(false);
        }
    }
    //Patrullar: patrulla moviéndose por los distintos waypoints(no tiene activo
    //Su punto débil)
    // TAG PARA ENTRAR EN "Patrullar()" = "WaypointPatrullaje"
    public void Patrullar()
    {
        puntoDebilActivado = false;
        if (waypoints.Length > 0)
        {
            if (id >= waypoints.Length)
            {
                id = 0;
            }
            if (waypoints[id] != null)
            {
                Vector3 target = waypoints[id].position;
                transform.LookAt(target);
                transform.position = transform.position + transform.forward * Time.deltaTime * VelocidadMov;
            }
        }

    }

    //PatrullarBulnerable: en este estado el submarino patrulla pero tiene su
    //Punto débil activo.
    // TAG PARA ENTRAR EN "PatrullarBulnerable()" = "WaypointPatrullarBulnerable"
    public void PatrullarBulnerable()
    {
        puntoDebilActivado = true;
        if (waypoints.Length > 0)
        {
            if (id >= waypoints.Length)
            {
                id = 0;
            }
            if (waypoints[id] != null)
            {
                Vector3 target = waypoints[id].position;
                transform.LookAt(target);
                transform.position = transform.position + transform.forward * Time.deltaTime * VelocidadMov;
            }
        }
    }

    //PatrullarDisparando: el jugador mientras pasa por los distintos waypoints
    //Dispara balas hacia el jugador(tiene activa su punto débil)
    //TAG PARA ENTRAR EN "PatrullaDisparando()" = "WaypointPatrullarDisparando"
    public void PatrullarDisparando()
    {
        puntoDebilActivado = true;
        if (waypoints.Length > 0)
        {
            if (id >= waypoints.Length)
            {
                id = 0;
            }
            if (waypoints[id] != null)
            {
                Vector3 target = waypoints[id].position;
                transform.LookAt(target);
                transform.position = transform.position + transform.forward * Time.deltaTime * VelocidadMov;
            }
        }
    }

    //AtacarTorpedos: el submarino pasa a este estado cuando pasa
    //Por un waypoints especifico(tiene activo su punto débil), dispara sus
    //Torpedos.
    //TAG PARA ENTRAR EN "AtacarTorpedos()" = "WaypointAtacarTorpedos"
    public void AtacarTorpedos()
    {
        puntoDebilActivado = true;
        if (waypoints.Length > 0)
        {
            if (id >= waypoints.Length)
            {
                id = 0;
            }
            if (waypoints[id] != null)
            {
                Vector3 target = waypoints[id].position;
                transform.LookAt(target);
                transform.position = transform.position + transform.forward * Time.deltaTime * VelocidadMov;
                
                if(dileyDisparoTorpedos> 0)
                {
                    dileyDisparoTorpedos = dileyDisparoTorpedos - Time.deltaTime;
                }
                if(dileyDisparoTorpedos<= 0)
                {
                    for (int i = 0; i < GeneradorTorpedos.Length; i++)
                    {
                        if (GeneradorTorpedos[i].activeSelf == true && poolTorpedos.GetId() <= poolTorpedos.count)
                        {
                            GameObject go = poolTorpedos.GetObject();
                            Torpedo torpedo = go.GetComponent<Torpedo>();
                            go.transform.position = GeneradorTorpedos[i].transform.position;
                            go.transform.rotation = GeneradorTorpedos[i].transform.rotation;
                            torpedo.Prendido();
                        }
                    }
                    dileyDisparoTorpedos = auxDileyDisparoTorpedos;
                }
            }
        }
    }

    //Seguir: el submarino sigue al jugador mientras dispara torpedos y
    //Si el jugador esta por el costado del submarino le dispara balas
    //(Tiene activo su punto débil)
    //Para entrar en "Seguir()" CONFIGURARLO PARA QUE ENTRE CUANDO ESTES EN MODO "SUPERVIVENCIA"
    public void Seguir()
    {
        Debug.Log("Seguir");
        puntoDebilActivado = true;
        if(Jugador.GetJugador() != null)
        {
            transform.LookAt(Jugador.GetJugador().transform.position);
        }
        transform.position = transform.position + transform.forward * Time.deltaTime * VelocidadMov;
        // FALTA HACER QUE MIENTRAS SIGA ATAQUE.
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WaypointPatrullaje")
        {
            estados = States.Patrullar;
            if (id >= waypoints.Length)
            {
                id = 0;
            }
            else
            {
                id++;
            }
        }
        if (other.tag == "WaypointPatrullarBulnerable")
        {
            estados = States.PatrullarBulnerable;
            if (id >= waypoints.Length)
            {
                id = 0;
            }
            else
            {
                id++;
            }
        }
        if (other.tag == "WaypointPatrullarDisparando")
        {
            estados = States.PatrullarDisparando;
            if (id >= waypoints.Length)
            {
                id = 0;
            }
            else
            {
                id++;
            }
        }
        if (other.tag == "WaypointAtacarTorpedos")
        {
            estados = States.AtacarTorpedos;
            if (id >= waypoints.Length)
            {
                id = 0;
            }
            else
            {
                id++;
            }
        }
    }
}
