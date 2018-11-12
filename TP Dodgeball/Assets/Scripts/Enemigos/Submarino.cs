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
        AtacarConTodo,
        Quieto,
    }

    public PoolPelota poolTorpedos;
    public float dileyDisparoTorpedos;
    public float VelocidadMov;
    public Transform[] waypoints;
    public GameObject[] GeneradorTorpedos;
    public States estados;
    public GameObject particulasBurbujas;
    public TorretaSubmarino[] torretas;
    public float ReducirDanioPelotaComun;
    public float ReducirDanioPelotaHielo;
    public float ReducirDanioMiniPelota;
    public float ReducirDanioExplocivo;

    private int id;
    private float auxVelocidadMov;
    private float timeEstado;
    private float efectoFuego;
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
        auxVelocidadMov = VelocidadMov;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(estados + "id:" +id);
        updateHP();
        UpdateStates();
        if (puntoDebilActivado)
        {
            CheckMuerto();
        }
        if (timeEstado > 0)
        {
            if (GetEstadoEnemigo() == EstadoEnemigo.bailando)
            {
                SetRotarY(90);
                Rotar();
            }
            if (GetEstadoEnemigo() == EstadoEnemigo.quemado || efectoQuemado.activeSelf)
            {
                efectoFuego = efectoFuego + Time.deltaTime;
                if (efectoFuego >= 1)
                {
                    if (Jugador.GetJugador() != null)
                    {
                        if (Jugador.GetJugador().GetDoblePuntuacion())
                        {
                            Jugador.GetJugador().SumarPuntos(5 * 2);
                        }
                        else
                        {
                            Jugador.GetJugador().SumarPuntos(5);
                        }
                        vida = vida - (GetDanioBolaFuego() + Jugador.GetJugador().GetDanioAdicionalPelotaFuego());
                        EstaMuerto();
                    }
                    efectoFuego = 0;
                }
                if (GetEstadoEnemigo() == EstadoEnemigo.congelado)
                {
                    VelocidadMov = 0;
                }
            }
            timeEstado = timeEstado - Time.deltaTime;
        }

        if (timeEstado <= 0)
        {
            if (GetEstadoEnemigo() == EstadoEnemigo.congelado)
            {

                VelocidadMov = auxVelocidadMov;
                efectoCongelado.SetActive(false);
                efectoQuemado.SetActive(false);
                efectoMusica.SetActive(false);
                SetEstadoEnemigo(EstadoEnemigo.normal);

            }
            if (GetEstadoEnemigo() == EstadoEnemigo.bailando)
            {
                efectoMusica.SetActive(false);
                efectoQuemado.SetActive(false);
                efectoCongelado.SetActive(false);
                SetEstadoEnemigo(EstadoEnemigo.normal);
            }
            if (GetEstadoEnemigo() == EstadoEnemigo.quemado)
            {
                efectoQuemado.SetActive(false);
                efectoMusica.SetActive(false);
                efectoCongelado.SetActive(false);
                SetEstadoEnemigo(EstadoEnemigo.normal);
            }
        }
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
            case (int)States.AtacarConTodo:
                AtacarConTodo();
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
   
    //(HECHO)
    //Patrullar: patrulla moviéndose por los distintos waypoints(no tiene activo
    //Su punto débil)
    // TAG PARA ENTRAR EN "Patrullar()" = "WaypointPatrullaje"
    public void Patrullar()
    {
        for(int i = 0; i< torretas.Length; i++)
        {
            if(torretas[i] != null)
            {
                torretas[i].SetDisparar(false);
            }
        }
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
    //(HECHO)
    //PatrullarBulnerable: en este estado el submarino patrulla pero tiene su
    //Punto débil activo.
    // TAG PARA ENTRAR EN "PatrullarBulnerable()" = "WaypointPatrullarBulnerable"
    public void PatrullarBulnerable()
    {
        for (int i = 0; i < torretas.Length; i++)
        {
            if (torretas[i] != null)
            {
                torretas[i].SetDisparar(false);
            }
        }
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

    
    //(HECHO)
    //AtacarTorpedos: el submarino pasa a este estado cuando pasa
    //Por un waypoints especifico(tiene activo su punto débil), dispara sus
    //Torpedos.
    //TAG PARA ENTRAR EN "AtacarTorpedos()" = "WaypointAtacarTorpedos"
    public void AtacarTorpedos()
    {
        for (int i = 0; i < torretas.Length; i++)
        {
            if (torretas[i] != null)
            {
                torretas[i].SetDisparar(false);
            }
        }
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
                    {                                                           // esta condicion del GetId() 
                                                                             //sirve para que no se pase del array
                        if (GeneradorTorpedos[i].activeSelf == true && poolTorpedos.GetId() < poolTorpedos.count)
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
    //(HECHO)
    //Seguir: el submarino sigue al jugador mientras dispara torpedos y
    //Si el jugador esta por el costado del submarino le dispara balas
    //(Tiene activo su punto débil)
    //Para entrar en "Seguir()" CONFIGURARLO PARA QUE ENTRE CUANDO ESTES EN MODO "SUPERVIVENCIA"
    public void Seguir()
    {
        for (int i = 0; i < torretas.Length; i++)
        {
            if (torretas[i] != null)
            {
                torretas[i].SetDisparar(false);
            }
        }
        puntoDebilActivado = true;
        if(Jugador.GetJugador() != null)
        {
            transform.LookAt(Jugador.GetJugador().transform.position);
        }
        transform.position = transform.position + transform.forward * Time.deltaTime * VelocidadMov;
        if (dileyDisparoTorpedos > 0)
        {
            dileyDisparoTorpedos = dileyDisparoTorpedos - Time.deltaTime;
        }
        if (dileyDisparoTorpedos <= 0)
        {
            for (int i = 0; i < GeneradorTorpedos.Length; i++)
            {                                                           // esta condicion del GetId() 
                                                                        //sirve para que no se pase del array
                if (GeneradorTorpedos[i].activeSelf == true && poolTorpedos.GetId() < poolTorpedos.count)
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
        // FALTA HACER QUE MIENTRAS SIGA ATAQUE.
    }

    //(HECHO)
    //PatrullarDisparando: el jugador mientras pasa por los distintos waypoints
    //Dispara balas hacia el jugador(tiene activa su punto débil)
    //TAG PARA ENTRAR EN "PatrullaDisparando()" = "WaypointPatrullarDisparando"
    public void PatrullarDisparando()
    {
        for (int i = 0; i < torretas.Length; i++)
        {
            if (torretas[i] != null)
            {
                torretas[i].SetDisparar(true);
            }
        }
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

    public void AtacarConTodo()
    {
        for (int i = 0; i < torretas.Length; i++)
        {
            if (torretas[i] != null)
            {
                torretas[i].SetDisparar(true);
            }
        }
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

                if (dileyDisparoTorpedos > 0)
                {
                    dileyDisparoTorpedos = dileyDisparoTorpedos - Time.deltaTime;
                }
                if (dileyDisparoTorpedos <= 0)
                {
                    for (int i = 0; i < GeneradorTorpedos.Length; i++)
                    {                                                           // esta condicion del GetId() 
                                                                                //sirve para que no se pase del array
                        if (GeneradorTorpedos[i].activeSelf == true && poolTorpedos.GetId() < poolTorpedos.count)
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


    public void CheckMuerto()
    {
        if (vida <= 0)
        {
            if (GetMuerto())
            {
                if (Jugador.GetJugador() != null)
                {
                    Jugador.GetJugador().SumarPuntos(250);
                }
                if (!estoyEnPool)
                {
                    gameObject.SetActive(false);
                }
                if (estoyEnPool)
                {
                    poolObject.Resiclarme();
                }
            }
        }
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
        if(other.tag == "WaypointAtacarConTodo")
        {
            estados = States.AtacarConTodo;
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
        if (puntoDebilActivado)
        {
            if (other.gameObject.tag == "PelotaComun")
            {
                if (Jugador.GetJugador() != null)
                {
                    vida = vida - (GetDanioBolaComun() + Jugador.GetJugador().GetDanioAdicionalPelotaComun() - ReducirDanioPelotaComun);
                    EstaMuerto();
                    if (Jugador.GetJugador().GetDoblePuntuacion())
                    {
                        Jugador.GetJugador().SumarPuntos(10 * 2);
                    }
                    else
                    {
                        Jugador.GetJugador().SumarPuntos(10);
                    }
                }
            }
            if (other.gameObject.tag == "PelotaDeHielo")
            {
                if (Jugador.GetJugador() != null)
                {
                    if (Jugador.GetJugador().GetDoblePuntuacion())
                    {
                        Jugador.GetJugador().SumarPuntos(10 * 2);
                    }
                    else
                    {
                        Jugador.GetJugador().SumarPuntos(10);
                    }
                    vida = vida - (GetDanioBolaHielo() + Jugador.GetJugador().GetDanioAdicionalPelotaHielo()-ReducirDanioPelotaHielo);
                }
                EstaMuerto();
                if (VelocidadMov > 0)
                {
                    VelocidadMov = VelocidadMov - 5;

                    //velMovimiento = 0;
                }
                if (VelocidadMov <= 0)
                {
                    SetEstadoEnemigo(EstadoEnemigo.congelado);
                    efectoCongelado.SetActive(true);
                    timeEstado = 2.5f;//tiempo por el cual el enemigo "Corredor" estara congelado
                }
            }
            if (other.gameObject.tag == "MiniPelota")
            {
                if (Jugador.GetJugador() != null)
                {
                    if (Jugador.GetJugador().GetDoblePuntuacion())
                    {
                        Jugador.GetJugador().SumarPuntos(10 * 2);
                    }
                    else
                    {
                        Jugador.GetJugador().SumarPuntos(10);
                    }
                    vida = vida - (GetDanioMiniBola() + Jugador.GetJugador().GetDanioAdicionalMiniPelota() - ReducirDanioMiniPelota);
                    EstaMuerto();
                }
            }
            if (other.gameObject.tag == "PelotaDanzarina")
            {
                if (Jugador.GetJugador() != null)
                {
                    if (Jugador.GetJugador().GetDoblePuntuacion())
                    {
                        Jugador.GetJugador().SumarPuntos(5 * 2);
                    }
                    else
                    {
                        Jugador.GetJugador().SumarPuntos(5);
                    }
                }
                if (GetEstadoEnemigo() != EstadoEnemigo.bailando)
                {
                    timeEstado = 1.5f;//tiempo por el cual el enemigo estara bailando
                }
                SetEstadoEnemigo(EstadoEnemigo.bailando);
                efectoMusica.SetActive(true);
                vida = vida - GetDanioBolaDanzarina();
                EstaMuerto();

            }
            if (other.gameObject.tag == "PelotaDeFuego")
            {
                if (GetEstadoEnemigo() != EstadoEnemigo.quemado)
                {
                    timeEstado = 7;
                }
                if (GetEstadoEnemigo() != EstadoEnemigo.bailando)
                {
                    SetEstadoEnemigo(EstadoEnemigo.quemado);
                }
                efectoQuemado.SetActive(true);
                VelocidadMov = auxVelocidadMov;
            }
            if (other.gameObject.tag == "PelotaExplociva")
            {
                if (Jugador.GetJugador() != null)
                {
                    if (Jugador.GetJugador().GetDoblePuntuacion())
                    {
                        Jugador.GetJugador().SumarPuntos(20 * 2);
                    }
                    else
                    {
                        Jugador.GetJugador().SumarPuntos(20);
                    }
                    vida = vida - ((GetDanioBolaExplociva() + Jugador.GetJugador().GetDanioAdicionalPelotaExplociva()) - ReducirDanioExplocivo);
                }
                EstaMuerto();

            }
        }
    }
}
/*
       *FALTA CALIBRARLO PARA QUE EL DAÑO QUE RESIVA POR PELOTA (pelota comun,pelota explociva, pelota de hielo y fragmentadora) SEA MUY BAJO.
*/
