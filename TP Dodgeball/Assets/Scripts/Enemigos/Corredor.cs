using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Corredor : Enemigo
{

    // Use this for initialization
    public float auxVida;
    public PoolPelota pool;
    private PoolObject poolObject;
    public float velocidad;
    private float timeEstado;
    private float efectoFuego;
    private float auxVelocidad;
    private Rigidbody rig;
    private float dileyInsta;
    public float MaxVelocidad;
    public int PatronDeMovimiento;
    public float rangoDoblar;

    public PoolPelota poolPoderInmune;
    public PoolPelota poolDoblePuntuacion;
    public PoolPelota poolInstaKill;

    void Start()
    {
        dileyInsta = 1;
        SetEsquivar(false);
        SetEstadoEnemigo(EstadoEnemigo.normal);
        rig = GetComponent<Rigidbody>();
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;
        auxVelocidad = velocidad;
        efectoFuego = 0;
        efectoCongelado.SetActive(false);
        efectoQuemado.SetActive(false);
        efectoMusica.SetActive(false);
    }

    // Update is called once per frame
    public void Prendido()
    {
        dileyInsta = 1;
        SetEsquivar(false);
        SetEstadoEnemigo(EstadoEnemigo.normal);
        vida = auxVida;
        rig = GetComponent<Rigidbody>();
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;
        auxVelocidad = velocidad;
        efectoFuego = 0;
        efectoCongelado.SetActive(false);
        efectoQuemado.SetActive(false);
        efectoMusica.SetActive(false);
        poolObject = GetComponent<PoolObject>();
    }
    void Update()
    {
        if (Jugador.GetJugador() != null)
        {
            if (!Jugador.GetJugador().GetActivarInstaKill())
            {
                dileyInsta = 1;
            }
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
                }
            }
        }
        EstaMuerto();
        updateHP();
        if (GetEstadoEnemigo() != EstadoEnemigo.congelado && GetEstadoEnemigo() != EstadoEnemigo.bailando)
        {
            Movimiento();
        }
        if (GetMuerto())
        {
            // Seguir configurando la probabilidad de aparicion de los powers ups
            float auxiliar = Random.Range(1, 100);
            if (auxiliar > 0 && auxiliar <= 8)
            {
                GameObject go = poolPoderInmune.GetObject();
                if (go != null)
                {
                    poolPoderInmune.RestarId();
                    go.transform.position = transform.position;
                    go.transform.rotation = transform.rotation;
                }
            }
            if (auxiliar > 12 && auxiliar <= 25)
            {
                GameObject go = poolDoblePuntuacion.GetObject();
                if (go != null)
                {
                    poolDoblePuntuacion.RestarId();
                    go.transform.position = transform.position;
                    go.transform.rotation = transform.rotation;
                }
            }
            if (auxiliar > 30 && auxiliar <= 35)
            {
                GameObject go = poolInstaKill.GetObject();
                if (go != null)
                {
                    poolInstaKill.RestarId();
                    go.transform.position = transform.position;
                    go.transform.rotation = transform.rotation;
                }
            }
            if (Jugador.GetJugador().GetDoblePuntuacion())
            {
                Jugador.GetJugador().SumarPuntos(50 * 2);
            }
            else
            {
                Jugador.GetJugador().SumarPuntos(50);
            }
            GameManager.GetGameManager().SumarMuertes();
            if (GameManager.GetGameManager() != null && estoyEnPool)
            {
                GameManager.GetGameManager().RestarEnemigoEnPantalla();
            }
            SetMuerto(false);
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
                SetRotarY(20);
                Rotar();
            }
            if (GetEstadoEnemigo() == EstadoEnemigo.quemado || efectoQuemado.active)
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
            timeEstado = timeEstado - Time.deltaTime;

        }
        if (timeEstado <= 0)
        {
            if (GetEstadoEnemigo() == EstadoEnemigo.congelado)
            {
                velocidad = auxVelocidad;
                SetEstadoEnemigo(EstadoEnemigo.normal);
            }
            if (GetEstadoEnemigo() == EstadoEnemigo.bailando)
            {
                SetEstadoEnemigo(EstadoEnemigo.normal);
            }
            if (GetEstadoEnemigo() == EstadoEnemigo.quemado)
            {
                SetEstadoEnemigo(EstadoEnemigo.normal);

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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Corredor" || collision.gameObject.tag == "Tirador" && PatronDeMovimiento == 1)
        {
            collision.gameObject.transform.Rotate(0, 180, 0);
        }
        if(collision.gameObject.tag == "Pared")
        {
            transform.Rotate(0, 90, 0);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Piso")
        {
            SetTocandoSuelo(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Piso")
        {
            SetTocandoSuelo(true);
        }
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
            if (velocidad > 0)
            {
                //velocidad = velocidad - 0.2f;
                velocidad = 0;
            }
            if (velocidad <= 0)
            {
                SetEstadoEnemigo(EstadoEnemigo.congelado);
                efectoCongelado.SetActive(true);
                timeEstado = 5;//tiempo por el cual el enemigo "Corredor" estara congelado
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
                timeEstado = 7;//tiempo por el cual el enemigo estara bailando
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
            velocidad = auxVelocidad;
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
    public void SetVelocidad(float _velocidad)
    {
        velocidad = _velocidad;
    }
    public void SetAuxVelocidad(float _auxVelociad)
    {
        auxVelocidad = _auxVelociad;
    }
    public float GetVelociadad()
    {
        return velocidad;
    }
    public float GetAuxVelocidad()
    {
        return auxVelocidad;
    }
    public void SumarVelocidad()
    {
        if (velocidad < MaxVelocidad)
        {
            velocidad = velocidad + Random.Range(0.01f, 1.5f);
            auxVelocidad = velocidad;
        }
    }
    public void Movimiento()
    {
        if (Jugador.GetJugador() != null)
        {
            if (PatronDeMovimiento == 0)
            {
                rig.velocity = Vector3.zero;
                rig.angularVelocity = Vector3.zero;

                transform.LookAt(new Vector3(Jugador.GetJugador().transform.position.x, transform.position.y, Jugador.GetJugador().transform.position.z));
                // si no esta colicionando con el piso que esto no se ejecute
                if (!GetTocandoSuelo())
                {
                    transform.position += transform.forward * Time.deltaTime * velocidad;
                }
            }
        }
        if (PatronDeMovimiento == 1)
        {
            rig.velocity = Vector3.zero;
            rig.angularVelocity = Vector3.zero;
            transform.position += transform.forward * Time.deltaTime * velocidad;
            //RaycastHit hit;
            //if (Physics.Raycast(fpsCamara.transform.position, fpsCamara.transform.forward, out hit, rango))
            //{

            //}
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, rangoDoblar))
            {
                float opcion = Random.Range(0, 2);
                if (opcion >= 1)
                {
                    transform.Rotate(0, 90, 0);
                }
                else
                {
                    transform.Rotate(0, -90, 0);
                }
            }
        }
    }
}
   
