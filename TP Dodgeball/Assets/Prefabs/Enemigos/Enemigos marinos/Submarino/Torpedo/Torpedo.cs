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
    }

    public void Prendido()
    {
        poolObject = GetComponent<PoolObject>();
        rig = GetComponent<Rigidbody>();
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;
        auxDileyAdelante = dileyAdelante;
        vida = maxVida;
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
                    Velocidad = 0;
                    VelocidadInicial = 0;
                }
            }
            timeEstado = timeEstado - Time.deltaTime;
        }
    }
    public void Moverse()
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
    public void CheckMuerto()
    {
        if (vida <= 0)
        {
            poolObject.Resiclarme();
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
}
