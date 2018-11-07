using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoDebilKraken : Enemigo {

    // Use this for initialization
    private float timeEstado;
    private float efectoFuego;
    public Enemigo kraken;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckEstados();
	}
    public void CheckEstados()
    {
        if (timeEstado > 0)
        {
            
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
                        kraken.vida = kraken.vida - (GetDanioBolaFuego() + Jugador.GetJugador().GetDanioAdicionalPelotaFuego());
                        kraken.EstaMuerto();
                    }
                    efectoFuego = 0;
                }
                
            }
            timeEstado = timeEstado - Time.deltaTime;
        }
        if (timeEstado <= 0)
        {

            if (GetEstadoEnemigo() == EstadoEnemigo.quemado)
            {
                efectoQuemado.SetActive(false);
                SetEstadoEnemigo(EstadoEnemigo.normal);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PelotaComun")
        {
            if (Jugador.GetJugador() != null)
            {
                kraken.vida = kraken.vida - (GetDanioBolaComun() + Jugador.GetJugador().GetDanioAdicionalPelotaComun());
                kraken.EstaMuerto();
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
                kraken.vida = kraken.vida - (GetDanioMiniBola() + Jugador.GetJugador().GetDanioAdicionalMiniPelota());
                kraken.EstaMuerto();
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
                kraken.vida = kraken.vida - (GetDanioBolaExplociva() + Jugador.GetJugador().GetDanioAdicionalPelotaExplociva());
            }
            kraken.EstaMuerto();

        }
    }
}
