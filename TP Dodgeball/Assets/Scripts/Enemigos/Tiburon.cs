using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiburon : Enemigo {

    // Use this for initialization
    public enum States
    {
        Nadando = 0,
        Seguir,
        Atacar,
        Retirse,
        Count
    }

    public enum Events
    {
        EnVista = 0,
        FueraDeVista,
        EnRangoDeAtaque,
        FueraDelRangoDeAtaque,
        LuegoDeAtacar,
        VolviendoQuieto,
        Count
    }
    private Jugador jugador;
    public float danio;
    public float potenciaAtaque;
    public float reducirPotenciaAtaque;
    public float reducirDanioPelotaComun;
    public float reducirDanioPelotaExplociva;
    public float reducirDanioPelotaFragmentadora;
    public float aumentarDanioPelotaFuego;
    public float velMovimiento;
    public Transform[] waypoints;
    public PoolPelota pool;
    public BoxCollider puntoDebil;

    private PoolObject poolObject;
    private float auxVelAtaque;
    private float auxVelMovimiento;
    private float timeEstado;
    private float efectoFuego;
    private States estados;
    private Events eventos;
    private int id = 0;
    private Rigidbody rig;
    private float velAtaque;
    private Vector3 posJugador;
    //private FSM fsm;
    public void Prendido()
    {
        if (Jugador.instanciaJugador != null)
        {
            jugador = Jugador.instanciaJugador;
        }
        rig = GetComponent<Rigidbody>();
        estados = States.Nadando;
        if (potenciaAtaque <= 0)
        {
            potenciaAtaque = 1;
        }
        velAtaque = velMovimiento * potenciaAtaque;
        velAtaque = velAtaque - reducirPotenciaAtaque;
        auxVelAtaque = velAtaque;
        auxVelMovimiento = velMovimiento;
    }
    void Start()
    {
        if(Jugador.instanciaJugador != null)
        {
            jugador = Jugador.instanciaJugador;
        }
        rig = GetComponent<Rigidbody>();
        estados = States.Nadando;
        if(potenciaAtaque <= 0)
        {
            potenciaAtaque = 1;
        }
        velAtaque = velMovimiento * potenciaAtaque;
        velAtaque = velAtaque - reducirPotenciaAtaque;
        auxVelAtaque = velAtaque;
        auxVelMovimiento = velMovimiento;
    }
	
	// Update is called once per frame
	void Update () {
        UpdateStates();
        updateHP();
        if (estados != States.Atacar)
        {
            UpdatePositionPlayer();
        }

        if (GetMuerto())
        {
            if(jugador != null)
            {
                jugador.SumarPuntos(250);
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
                    if (jugador != null)
                    {
                        if (jugador.GetDoblePuntuacion())
                        {
                            jugador.SumarPuntos(5 * 2);
                        }
                        else
                        {
                            jugador.SumarPuntos(5);
                        }
                        vida = vida - (GetDanioBolaFuego() + jugador.GetDanioAdicionalPelotaFuego() + aumentarDanioPelotaFuego);
                        EstaMuerto();
                    }
                    efectoFuego = 0;
                }
                if(GetEstadoEnemigo() == EstadoEnemigo.congelado)
                {
                    velAtaque = 0;
                    velMovimiento = 0;
                }
            }
            timeEstado = timeEstado - Time.deltaTime;
        }

        if (timeEstado <= 0)
        {
            if (GetEstadoEnemigo() == EstadoEnemigo.congelado)
            {
                velAtaque = auxVelAtaque;
                velMovimiento = auxVelMovimiento;
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
            case (int)States.Nadando:
                Nadar();
                break;
            case (int)States.Seguir:
                Seguir();
                break;
            case (int)States.Atacar:
                Atacar();
                break;
            case (int)States.Retirse:
                Retirarse();
                break;
        }
    }
    public void UpdatePositionPlayer()
    {
        if (jugador != null)
        {
            posJugador = jugador.transform.position;
        }
    }
    public void Nadar()
    {
        if (puntoDebil != null)
        {
            puntoDebil.enabled = false;
        }
        if (waypoints.Length > 0)
        {
            if (waypoints[id] != null)
            {
                Vector3 target = waypoints[id].position;
                transform.LookAt(target);
                transform.position = transform.position + transform.forward * Time.deltaTime * velMovimiento; 
                if (id >= waypoints.Length)
                {
                    id = 0;
                }
                
            }
        }
        else
        {
            id = 0;
        }
    }
    public void Seguir()
    {
        if (jugador != null)
        {
            transform.LookAt(jugador.transform.position);
            transform.position = transform.position + transform.forward * Time.deltaTime * velMovimiento;
        }
    }
    public void Atacar()
    {
        if (puntoDebil != null)
        {
            puntoDebil.enabled = true;
        }
        if(jugador != null)
        {
            transform.LookAt(posJugador);
            transform.position = transform.position + transform.forward * Time.deltaTime * velAtaque;
            if (transform.position.y < jugador.transform.position.y)
            {
                estados = States.Retirse;
            }
        }
        
    }
    public void Retirarse()
    {
        estados = States.Nadando;
        id++;
        if (id >= waypoints.Length)
        {
            id = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (puntoDebil.enabled == true)
        {
            if (other.gameObject.tag == "PelotaComun")
            {
                if (jugador != null)
                {
                    vida = vida - ((GetDanioBolaComun() + jugador.GetDanioAdicionalPelotaComun()) - reducirDanioPelotaComun);
                    EstaMuerto();
                    if (jugador.GetDoblePuntuacion())
                    {
                        jugador.SumarPuntos(10 * 2);
                    }
                    else
                    {
                        jugador.SumarPuntos(10);
                    }
                }
            }
            if (other.gameObject.tag == "PelotaDeHielo")
            {
                if (jugador != null)
                {
                    if (jugador.GetDoblePuntuacion())
                    {
                        jugador.SumarPuntos(10 * 2);
                    }
                    else
                    {
                        jugador.SumarPuntos(10);
                    }
                    vida = vida - (GetDanioBolaHielo() + jugador.GetDanioAdicionalPelotaHielo());
                }
                EstaMuerto();
                if (velMovimiento > 0 || velAtaque > 0)
                {
                    velMovimiento = velMovimiento - 20f;
                    velAtaque = velAtaque - 20f;
                    //velMovimiento = 0;
                }
                if (velMovimiento <= 0 || velAtaque <= 0)
                {
                    SetEstadoEnemigo(EstadoEnemigo.congelado);
                    efectoCongelado.SetActive(true);
                    timeEstado = 2.5f;//tiempo por el cual el enemigo "Corredor" estara congelado
                }
            }
            if (other.gameObject.tag == "MiniPelota")
            {
                if (jugador != null)
                {
                    if (jugador.GetDoblePuntuacion())
                    {
                        jugador.SumarPuntos(10 * 2);
                    }
                    else
                    {
                        jugador.SumarPuntos(10);
                    }
                    vida = vida - ((GetDanioMiniBola() + jugador.GetDanioAdicionalMiniPelota())- reducirDanioPelotaFragmentadora);
                    EstaMuerto();
                }
            }
            if (other.gameObject.tag == "PelotaDanzarina")
            {
                if (jugador != null)
                {
                    if (jugador.GetDoblePuntuacion())
                    {
                        jugador.SumarPuntos(5 * 2);
                    }
                    else
                    {
                        jugador.SumarPuntos(5);
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
                velMovimiento = auxVelMovimiento;
                velAtaque = auxVelAtaque;
            }
            if (other.gameObject.tag == "PelotaExplociva")
            {
                if (jugador != null)
                {
                    if (jugador.GetDoblePuntuacion())
                    {
                        jugador.SumarPuntos(20 * 2);
                    }
                    else
                    {
                        jugador.SumarPuntos(20);
                    }
                    vida = vida - ((GetDanioBolaExplociva() + jugador.GetDanioAdicionalPelotaExplociva()) - reducirDanioPelotaExplociva);
                }
                EstaMuerto();

            }
        }
        if (other.tag == "Player")
        {
            estados = States.Retirse;
            id++;
            if (id >= waypoints.Length)
            {
                id = 0;
            }
            if(jugador != null)
            {
                if (jugador.blindaje > 0)
                {
                    jugador.blindaje = jugador.blindaje - danio;
                }
                else
                {
                    jugador.vida = jugador.vida - danio;
                }
            }
        }
        if (other.tag == "WaypointRandom")
        {
            float random = Random.Range(1, 100);
            if (random >= 80)
            {
                estados = States.Atacar;
            }
            if(random < 80)
            {
                id++;
                if (id >= waypoints.Length)
                {
                    id = 0;
                }
            }
            random = 0;
        }
        if(other.tag == "Waypoint")
        {
            id++;
            if (id >= waypoints.Length)
            {
                id = 0;
            }
        }
    }
}
