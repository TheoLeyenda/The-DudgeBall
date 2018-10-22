using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiradorEstatico : Enemigo
{

    // Use this for initialization
    public float auxVida;
    public PoolPelota pelotasRugby;
    private PoolObject poolObject;
    private float auxTiempoVida;
    public float dilay;
    private float auxDilay;
    public GameObject Bola;
    public GameObject generadorPelota;
    public GameObject tirador;
    private float timeEstado;
    private float efectoFuego;
    private Rigidbody rig;
    private float dileyInsta;

    void Start()
    {
        dileyInsta = 1;
        auxVida = vida;
        auxDilay = dilay;
        timeEstado = 0;
        SetEstadoEnemigo(EstadoEnemigo.normal);
        efectoFuego = 0;
        efectoCongelado.SetActive(false);
        efectoMusica.SetActive(false);
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Jugador.GetJugador() != null)
        {
            if (Jugador.GetJugador().GetInstaKill())
            {
                vida = 1;
            }
            if (!Jugador.GetJugador().GetInstaKill() && Jugador.GetJugador().GetActivarInstaKill())
            {
                vida = auxVida;
                if (dileyInsta > 0)
                {
                    dileyInsta = dileyInsta - Time.deltaTime;
                }
                if (dileyInsta <= 0)
                {
                    Jugador.GetJugador().SetActivarInstaKill(false);
                    dileyInsta = 1;
                }
            }
        }
        updateHP();
        rig.Sleep();
        EstaMuerto();
        if (GetEstadoEnemigo() != EstadoEnemigo.congelado && GetEstadoEnemigo() != EstadoEnemigo.bailando)
        {
            Movimiento();
        }
        if (dilay <= 0)
        {
            dilay = auxDilay;
            TirarBola();
        }
        if (dilay > 0)
        {
            dilay = dilay - Time.deltaTime;
        }
        if (GetMuerto())
        {
            if (!estoyEnPool)
            {
                gameObject.SetActive(false);
            }
        }
        if (timeEstado > 0)
        {
            timeEstado = timeEstado - Time.deltaTime;

            if (GetEstadoEnemigo() == EstadoEnemigo.congelado)
            {
                dilay = 200000;
            }
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
                            Jugador.GetJugador().SumarPuntos(5*2);
                        }
                        else
                        {
                            Jugador.GetJugador().SumarPuntos(5);
                        }
                        vida = vida - (GetDanioBolaFuego() + Jugador.GetJugador().GetDanioAdicionalPelotaFuego());
                    }
                    efectoFuego = 0;
                }
            }
            if (timeEstado <= 0)
            {
                dilay = auxDilay;
                SetEstadoEnemigo(EstadoEnemigo.normal);
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                
            }
        }
        if (GetEstadoEnemigo() != EstadoEnemigo.quemado && GetEstadoEnemigo() != EstadoEnemigo.bailando)
        {
            efectoQuemado.SetActive(false);
        }
        if (GetEstadoEnemigo() != EstadoEnemigo.congelado)
        {
            efectoCongelado.SetActive(false);
        }
        if (GetEstadoEnemigo() != EstadoEnemigo.bailando)
        {
            efectoMusica.SetActive(false);
        }

    }
    public void Movimiento()
    {
        if (Jugador.GetJugador() != null)
        {
            transform.LookAt(new Vector3(Jugador.GetJugador().transform.position.x, transform.position.y, Jugador.GetJugador().transform.position.z));
        }
    }
    public void TirarBola()
    {
       
        GameObject go = pelotasRugby.GetObject();
        PelotaEnemigo pelota = go.GetComponent<PelotaEnemigo>();
        go.transform.position = generadorPelota.transform.position;
        go.transform.rotation = generadorPelota.transform.rotation;
        pelota.Disparar();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PelotaComun")
        {
            if (Jugador.GetJugador() != null)
            {
                vida = vida - (GetDanioBolaComun() + Jugador.GetJugador().GetDanioAdicionalPelotaComun());
                EstaMuerto();
                if (Jugador.GetJugador().GetDoblePuntuacion())
                {
                    Jugador.GetJugador().SumarPuntos(10*2);
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
            if (GetEstadoEnemigo() != EstadoEnemigo.congelado)
            {
                timeEstado = 5;//tiempo por el cual el enemigo "Corredor" estara congelado
            }
            efectoCongelado.SetActive(true);
            SetEstadoEnemigo(EstadoEnemigo.congelado);
        }
        if(other.gameObject.tag == "MiniPelota")
        {
            if (Jugador.GetJugador() != null)
            {
                if (Jugador.GetJugador().GetDoblePuntuacion())
                {
                    Jugador.GetJugador().SumarPuntos(10*2);
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
            if (Jugador.GetJugador().GetDoblePuntuacion())
            {
                Jugador.GetJugador().SumarPuntos(5*2);
            }
            else
            {
                Jugador.GetJugador().SumarPuntos(5);
            }
            if (GetEstadoEnemigo() != EstadoEnemigo.bailando)
            {
                timeEstado = 7;//tiempo por el cual el enemigo estara bailando
            }
            SetEstadoEnemigo(EstadoEnemigo.bailando);
            vida = vida - GetDanioBolaDanzarina();
            EstaMuerto();
            efectoMusica.SetActive(true);
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
            dilay = auxDilay;
        }
        if (other.gameObject.tag == "PelotaExplociva")
        {
            EstaMuerto();
            if (Jugador.GetJugador() != null)
            {
                if (Jugador.GetJugador().GetDoblePuntuacion())
                {
                    Jugador.GetJugador().SumarPuntos(20*2);
                }
                else
                {
                    Jugador.GetJugador().SumarPuntos(20);
                }
                vida = vida - (GetDanioBolaExplociva() + Jugador.GetJugador().GetDanioAdicionalPelotaExplociva());
            }
        }
    }
}
