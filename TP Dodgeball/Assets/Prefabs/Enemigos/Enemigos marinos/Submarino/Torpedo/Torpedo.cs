using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : Enemigo {

    // Use this for initialization
    public float Velocidad;
    public float VelocidadInicial;
    public float Danio;
    public PoolPelota pool;
    public float dileyAdelante;
    public GameObject burbujas;

    private float auxVelocidaInicial;
    private float auxVelocidad;
    private float timeEstado;
    private float efectoFuego;
    private float auxDileyAdelante;
    private float auxVida;
    private Rigidbody rig;

    private PoolObject poolObject;
    void Start() {
        poolObject = GetComponent<PoolObject>();
        rig = GetComponent<Rigidbody>();
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;
        auxDileyAdelante = dileyAdelante;
        vida = maxVida;
        auxVelocidaInicial = VelocidadInicial;
        auxVelocidad = Velocidad;
    }

    public void Prendido()
    {
        SetEstadoEnemigo(EstadoEnemigo.normal);
        poolObject = GetComponent<PoolObject>();
        rig = GetComponent<Rigidbody>();
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;
        auxDileyAdelante = dileyAdelante;
        vida = maxVida;
        timeEstado = 0;
    }
    // Update is called once per frame
    void Update()
    {
        Moverse();
        CheckMuerto();
        if (timeEstado > 0)
        {
            if (GetEstadoEnemigo() == EstadoEnemigo.bailando)
            {
                SetRotarY(90);
                Rotar();
            }
            if (GetEstadoEnemigo() == EstadoEnemigo.congelado)
            {
                Velocidad = 0;
                VelocidadInicial = 0;
            }
            timeEstado = timeEstado - Time.deltaTime;
        }
        if (timeEstado <= 0)
        {
            if (GetEstadoEnemigo() == EstadoEnemigo.congelado)
            {
                Velocidad = auxVelocidad;
                VelocidadInicial = auxVelocidaInicial;
                if (efectoCongelado != null && efectoMusica != null)
                {
                    efectoCongelado.SetActive(false);
                    efectoMusica.SetActive(false);
                }
                SetEstadoEnemigo(EstadoEnemigo.normal);

            }
            if (GetEstadoEnemigo() == EstadoEnemigo.bailando)
            {
                if (efectoCongelado != null && efectoMusica != null)
                {
                    efectoMusica.SetActive(false);
                    efectoCongelado.SetActive(false);
                }
                SetEstadoEnemigo(EstadoEnemigo.normal);
            }
            if (GetEstadoEnemigo() == EstadoEnemigo.quemado)
            {
                if ( efectoCongelado != null && efectoMusica != null)
                {
                    efectoMusica.SetActive(false);
                    efectoCongelado.SetActive(false);
                }
                SetEstadoEnemigo(EstadoEnemigo.normal);
            }
        }
    }
    public void Moverse()
    {
        if (GetEstadoEnemigo() != EstadoEnemigo.congelado && GetEstadoEnemigo() != EstadoEnemigo.bailando)
        {
            if (dileyAdelante > 0)
            {
                transform.position = transform.position + transform.forward * Time.deltaTime * VelocidadInicial;
                burbujas.SetActive(false);
            }
            if (dileyAdelante < 0)
            {
                burbujas.SetActive(true);
                if (Jugador.GetJugador() != null)
                {
                    transform.LookAt(Jugador.GetJugador().transform.position);
                    transform.position = transform.position + transform.forward * Time.deltaTime * Velocidad;
                }
            }
            dileyAdelante = dileyAdelante - Time.deltaTime;
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
        if (other.gameObject.tag == "PelotaComun")
        {
            if (Jugador.GetJugador() != null)
            {
                vida = vida - (GetDanioBolaComun() + Jugador.GetJugador().GetDanioAdicionalPelotaComun());
                EstaMuerto();
                if (Jugador.GetJugador().GetDoblePuntuacion())
                {
                    Jugador.GetJugador().SumarPuntos(1 * 2);
                }
                else
                {
                    Jugador.GetJugador().SumarPuntos(1);
                }
            }
        }
        if (other.gameObject.tag == "PelotaDeHielo")
        {
            if (Jugador.GetJugador() != null)
            {
                if (Jugador.GetJugador().GetDoblePuntuacion())
                {
                    Jugador.GetJugador().SumarPuntos(1 * 2);
                }
                else
                {
                    Jugador.GetJugador().SumarPuntos(1);
                }
                vida = vida - (GetDanioBolaHielo() + Jugador.GetJugador().GetDanioAdicionalPelotaHielo());
            }
            EstaMuerto();
            if (Velocidad > 0 || VelocidadInicial > 0)
            {
                Velocidad = Velocidad - 2f;
                VelocidadInicial = VelocidadInicial - 4f;
                //velMovimiento = 0;
            }
            if (Velocidad <= 0 || VelocidadInicial <= 0)
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
                    Jugador.GetJugador().SumarPuntos(1 * 2);
                }
                else
                {
                    Jugador.GetJugador().SumarPuntos(1);
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
                    Jugador.GetJugador().SumarPuntos(1 * 2);
                }
                else
                {
                    Jugador.GetJugador().SumarPuntos(1);
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
        if (other.gameObject.tag == "PelotaExplociva")
        {
            if (Jugador.GetJugador() != null)
            {
                if (Jugador.GetJugador().GetDoblePuntuacion())
                {
                    Jugador.GetJugador().SumarPuntos(1 * 2);
                }
                else
                {
                    Jugador.GetJugador().SumarPuntos(1);
                }
                vida = vida - (GetDanioBolaExplociva() + Jugador.GetJugador().GetDanioAdicionalPelotaExplociva());
            }
            EstaMuerto();

        }
        if(other.gameObject.tag == "Player")
        {
            if(Jugador.GetJugador() != null)
            {
                Jugador.GetJugador().vida = Jugador.GetJugador().vida - Danio;
            }
        }
    }
}
