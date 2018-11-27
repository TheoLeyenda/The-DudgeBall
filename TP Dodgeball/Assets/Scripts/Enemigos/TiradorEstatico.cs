using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiradorEstatico : Enemigo
{

    // Use this for initialization
    private Jugador jugador;
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
    public int tipoMovimiento;
    public float danio;
    public AudioSource Audio;
    public AudioClip clip;

    void Start()
    {
        if(Jugador.instanciaJugador != null)
        {
            jugador = Jugador.instanciaJugador;
        }
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
    private void OnEnable()
    {
        vida = maxVida;
        SetMuerto(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (jugador != null)
        {
            if (jugador.GetInstaKill())
            {
                vida = 1;
            }
            if (!jugador.GetInstaKill() && jugador.GetActivarInstaKill())
            {
                vida = auxVida;
                if (dileyInsta > 0)
                {
                    dileyInsta = dileyInsta - Time.deltaTime;
                }
                if (dileyInsta <= 0)
                {
                    jugador.SetActivarInstaKill(false);
                    dileyInsta = 1;
                }
            }
        }
        updateHP();
        rig.Sleep();
        //EstaMuerto();
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
                    if (jugador != null)
                    {
                        if (jugador.GetDoblePuntuacion())
                        {
                            jugador.SumarPuntos(5*2);
                        }
                        else
                        {
                            jugador.SumarPuntos(5);
                        }
                        vida = vida - (GetDanioBolaFuego() + jugador.GetDanioAdicionalPelotaFuego());
                        EstaMuerto();
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
        if (jugador != null)
        {
            if (tipoMovimiento == 0)
            {
                transform.LookAt(new Vector3(jugador.transform.position.x, transform.position.y, jugador.transform.position.z));
            }
            if(tipoMovimiento == 1)
            {
                transform.LookAt(new Vector3(jugador.transform.position.x, jugador.transform.position.y, jugador.transform.position.z));
            }
        }
    }
    public void TirarBola()
    {
        if (Audio != null && clip != null)
        {
            //Audio.volume = 0;
            Audio.PlayOneShot(clip);
        }
        GameObject go = pelotasRugby.GetObject();
        PelotaEnemigo pelota = go.GetComponent<PelotaEnemigo>();
        go.transform.position = generadorPelota.transform.position;
        go.transform.rotation = generadorPelota.transform.rotation;
        pelota.Disparar();
        if (danio > 0)
        {
            pelota.danio = danio;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PelotaComun")
        {
            if (jugador != null)
            {
                vida = vida - (GetDanioBolaComun() + jugador.GetDanioAdicionalPelotaComun());
                EstaMuerto();
                if (jugador.GetDoblePuntuacion())
                {
                    jugador.SumarPuntos(10*2);
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
            if (GetEstadoEnemigo() != EstadoEnemigo.congelado)
            {
                timeEstado = 5;//tiempo por el cual el enemigo "Corredor" estara congelado
            }
            efectoCongelado.SetActive(true);
            SetEstadoEnemigo(EstadoEnemigo.congelado);
        }
        if(other.gameObject.tag == "MiniPelota")
        {
            if (jugador != null)
            {
                if (jugador.GetDoblePuntuacion())
                {
                    jugador.SumarPuntos(10*2);
                }
                else
                {
                    jugador.SumarPuntos(10);
                }
                vida = vida - (GetDanioMiniBola() + jugador.GetDanioAdicionalMiniPelota());
                EstaMuerto();
            }
        }
        if (other.gameObject.tag == "PelotaDanzarina")
        {
            if (jugador.GetDoblePuntuacion())
            {
                jugador.SumarPuntos(5*2);
            }
            else
            {
                jugador.SumarPuntos(5);
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
            if (jugador != null)
            {
                if (jugador.GetDoblePuntuacion())
                {
                    jugador.SumarPuntos(20*2);
                }
                else
                {
                    Jugador.GetJugador().SumarPuntos(20);
                }
                vida = vida - (GetDanioBolaExplociva() + jugador.GetDanioAdicionalPelotaExplociva());
            }
        }
    }
}
