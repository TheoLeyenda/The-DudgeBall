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
    public float danio;
    public float potenciaAtaque;
    public float reducirPotenciaAtaque;
    public float reducirDanioPelotaComun;
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
        if (GetMuerto())
        {
            if (!estoyEnPool)
            {
                gameObject.SetActive(false);
            }
            if (estoyEnPool)
            {
                poolObject.Resiclarme();
            }
        }
        if (estados != States.Atacar)
        {
            UpdatePositionPlayer();
        }
        if (timeEstado > 0)
        {
            if (GetEstadoEnemigo() == EstadoEnemigo.bailando)
            {
                SetRotarY(20);
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
                        vida = vida - (GetDanioBolaFuego() + Jugador.GetJugador().GetDanioAdicionalPelotaFuego() + aumentarDanioPelotaFuego);
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
                
            }
            if (GetEstadoEnemigo() == EstadoEnemigo.bailando)
            {
                efectoMusica.SetActive(false);
                efectoQuemado.SetActive(false);
                efectoCongelado.SetActive(false);
            }
            if (GetEstadoEnemigo() == EstadoEnemigo.quemado)
            {
                efectoQuemado.SetActive(false);
                efectoMusica.SetActive(false);
                efectoCongelado.SetActive(false);
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
        if (Jugador.GetJugador() != null)
        {
            posJugador = Jugador.GetJugador().transform.position;
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

                //Vector3 diff = target - this.transform.position;

                //if (diff.magnitude < 0.5f)
                //{
                    
                    if (id >= waypoints.Length)
                    {
                        id = 0;
                    }
                //}
            }
        }
        else
        {
            id = 0;
        }
    }
    public void Seguir()
    {
        if (Jugador.GetJugador() != null)
        {
            transform.LookAt(Jugador.GetJugador().transform.position);
            transform.position = transform.position + transform.forward * Time.deltaTime * velMovimiento;
        }
    }
    public void Atacar()
    {
        if (puntoDebil != null)
        {
            puntoDebil.enabled = true;
        }
        if(Jugador.GetJugador() != null)
        {
            transform.LookAt(posJugador);
            transform.position = transform.position + transform.forward * Time.deltaTime * velAtaque;
            if (transform.position.y < Jugador.GetJugador().transform.position.y)
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
                if (Jugador.GetJugador() != null)
                {
                    vida = vida - ((GetDanioBolaComun() + Jugador.GetJugador().GetDanioAdicionalPelotaComun()) - reducirDanioPelotaComun);
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
                    vida = vida - (GetDanioBolaHielo() + Jugador.GetJugador().GetDanioAdicionalPelotaHielo());
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
                    vida = vida - (GetDanioMiniBola() + Jugador.GetJugador().GetDanioAdicionalMiniPelota());
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
                velMovimiento = auxVelMovimiento;
                velAtaque = auxVelAtaque;
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
                    vida = vida - (GetDanioBolaExplociva() + Jugador.GetJugador().GetDanioAdicionalPelotaExplociva());
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
            if(Jugador.GetJugador() != null)
            {
                if (Jugador.GetJugador().blindaje > 0)
                {
                    Jugador.GetJugador().blindaje = Jugador.GetJugador().blindaje - danio;
                }
                else
                {
                    Jugador.GetJugador().vida = Jugador.GetJugador().vida - danio;
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
    // HACER QUE CUANDO LE DE EL GOLPE AL JUGADOR LO CAMBIE A ESTADO NADANDO Y DESACTIVE EL RANGO DE ATAQUE Y SEGUIMIENTO (O SOLO DE ATAQUE)
    // HASTA QUE EL TIBURON LLEGUE AL PROXIMO WAYPOINT

    //FIJARSE DE HACER QUE EL TIBURON ATAQUE NO SOLO CUANDO ESTA EN RANGO SINO TAMBIEN EN CUALQUIER MOMENTO DE FORMA ALEATOREA.

    // PARA HACER QUE EL TIBURON ENTRE EN ESTADO ATAQUE DE FORMA ALEATOREA: cuando el tiburon haga triggered/collisione con un waypoint
    //tirar un random entre 0 y 100. Si en el random aparece como resultado el numero que yo defino para que el tiburon ataque, el tiburon pasa a estado de ataque.

    //ACTIVAR Y DESACTIVAR LOS RANGOS DE VISION Y ATAQUE SEGUN EL CASO(PARA ESTO USAR BOLEANOS).

}
