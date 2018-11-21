using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torre : Enemigo {

    // Use this for initialization
    public PoolPelota flechas;
    private PoolObject poolObject;
    public float dilayDisparo;
    private float auxDilayDisparo;
    private float timeEstado;
    private float efectoFuego;
    public float danio;
    public float potenciaFlecha;
    public GameObject generadorPelota;

    private Rigidbody rig;
    void Start () {
        auxDilayDisparo = dilayDisparo;
        timeEstado = 0;
        SetEstadoEnemigo(EstadoEnemigo.normal);
        efectoFuego = 0;
        efectoCongelado.SetActive(false);
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        updateHP();
        EstaMuerto();
        if (GetMuerto())
        {
            if (!estoyEnPool)
            {
                gameObject.SetActive(false);
            }
        }
        CheckEstadoTorre();
        CheckDisparo();
        
    }
    public void CheckDisparo()
    {
        if (dilayDisparo > 0)
        {
            dilayDisparo = dilayDisparo - Time.deltaTime;
        }
        if(dilayDisparo <= 0)
        {
            dilayDisparo = auxDilayDisparo;
            TirarFlecha();
        }
    }
    public void TirarFlecha()
    {
        //Instantiate(Bola,generadorPelota.transform.position ,generadorPelota.transform.rotation);
        GameObject go = flechas.GetObject();
        PelotaEnemigo pelota = go.GetComponent<PelotaEnemigo>();
        go.transform.position = generadorPelota.transform.position;
        go.transform.rotation = generadorPelota.transform.rotation;
        if (danio > 0)
        {
            pelota.danio = danio;
        }
        if(potenciaFlecha > 0)
        {
            pelota.potencia = potenciaFlecha;
        }
        pelota.Disparar();
    }
    public void CheckEstadoTorre()
    {
        if (timeEstado > 0)
        {
            timeEstado = timeEstado - Time.deltaTime;
            if (GetEstadoEnemigo() == EstadoEnemigo.congelado)
            {
                dilayDisparo = 1000000000;
            }
            if (GetEstadoEnemigo() == EstadoEnemigo.bailando)
            {
                SetRotarY(20);
                Rotar();
            }
            if (timeEstado <= 0 && GetEstadoEnemigo() == EstadoEnemigo.bailando)
            {
                SetEstadoEnemigo(EstadoEnemigo.normal);
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
                    }
                    efectoFuego = 0;
                }
            }
        }
        if (timeEstado <= 0 && GetEstadoEnemigo() == EstadoEnemigo.congelado)
        {

            dilayDisparo = auxDilayDisparo;
            SetEstadoEnemigo(EstadoEnemigo.normal);
        }
        if (timeEstado <= 0 && GetEstadoEnemigo() == EstadoEnemigo.quemado)
        {
            SetEstadoEnemigo(EstadoEnemigo.normal);
        }
        if (GetEstadoEnemigo() != EstadoEnemigo.quemado && GetEstadoEnemigo() != EstadoEnemigo.bailando && efectoQuemado != null)
        {
            efectoQuemado.SetActive(false);
        }
        if (GetEstadoEnemigo() != EstadoEnemigo.congelado && efectoCongelado != null)
        {
            efectoCongelado.SetActive(false);
        }
        if (GetEstadoEnemigo() != EstadoEnemigo.bailando && efectoMusica != null)
        {
            efectoMusica.SetActive(false);
        }
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
            if (GetEstadoEnemigo() != EstadoEnemigo.congelado)
            {
                timeEstado = 5;//tiempo por el cual el enemigo "Corredor" estara congelado
            }
            SetEstadoEnemigo(EstadoEnemigo.congelado);
            efectoCongelado.SetActive(true);
            
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
            dilayDisparo = auxDilayDisparo;
        }
        if (other.gameObject.tag == "PelotaExplociva")
        {
            EstaMuerto();
            if (Jugador.GetJugador() != null)
            {
                if (Jugador.GetJugador().GetDoblePuntuacion())
                {
                    Jugador.GetJugador().SumarPuntos(20 * 2);
                }
                else
                {
                    Jugador.GetJugador().SumarPuntos(10);
                }
                vida = vida - (GetDanioBolaExplociva() + Jugador.GetJugador().GetDanioAdicionalPelotaExplociva());
            }

        }
    }
}
