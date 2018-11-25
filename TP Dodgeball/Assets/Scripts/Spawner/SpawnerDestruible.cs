using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDestruible : Enemigo {

    // Use this for initialization
    private float timeEstado;
    private float efectoFuego;
    private Jugador jugador;
    void Start () {
        if(Jugador.instanciaJugador != null)
        {
            jugador = Jugador.instanciaJugador;
        }
        efectoFuego = 0;
        efectoCongelado.SetActive(false);
        efectoQuemado.SetActive(false);
        efectoMusica.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if(vida <= 0)
        {
            jugador.SumarPuntos(150);
            gameObject.SetActive(false);
        }
        if (timeEstado > 0)
        {
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
                        vida = vida - (GetDanioBolaFuego() + jugador.GetDanioAdicionalPelotaFuego());
                    }
                    efectoFuego = 0;
                }
            }
            timeEstado = timeEstado - Time.deltaTime;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PelotaComun")
        {
            if (jugador != null)
            {
                vida = vida - (GetDanioBolaComun() + jugador.GetDanioAdicionalPelotaComun());
                if (jugador.GetDoblePuntuacion())
                {
                    jugador.SumarPuntos(10 * 2);
                }
                else
                {
                    jugador.SumarPuntos(10);
                }
            }
            updateHP();
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
            updateHP();
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
                vida = vida - (GetDanioMiniBola() + jugador.GetDanioAdicionalMiniPelota());
            }
            updateHP();
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
                timeEstado = 7;//tiempo por el cual el enemigo estara bailando
            }
            SetEstadoEnemigo(EstadoEnemigo.bailando);
            vida = vida - GetDanioBolaDanzarina();
            updateHP();
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
            updateHP();
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
                vida = vida - (GetDanioBolaExplociva() + jugador.GetDanioAdicionalPelotaExplociva());
            }
            updateHP();
        }
    }
}
