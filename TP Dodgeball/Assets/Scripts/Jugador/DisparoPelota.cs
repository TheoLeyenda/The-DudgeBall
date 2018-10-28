using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoPelota : MonoBehaviour {

    // Use this for initialization
    //public GameObject pelota;
    public AudioSource sonido;
    public AudioClip sonidoPelotaComun;
    public AudioClip sonidoPelotaHielo;
    public AudioClip sonidoPelotaDanzarina;
    public AudioClip sonidoPelotaFuego;
    public AudioClip sonidoPelotaExplociva;
    public AudioClip sonidoPelotaFragmentadora;
    public PoolPelota PelotaComun;
    public PoolPelota pelotaDeHielo;
    public PoolPelota pelotaFragmentadora;
    public PoolPelota pelotaDanzarina;
    public PoolPelota pelotaDeFuego;
    public PoolPelota pelotaExplociva;
    public GameObject generador;
    public GameObject generadorExplicivos;
    public Transform jugador;
    public GameObject panelArmas;
    private float efectoFuego;
    public bool jugadorWindows;
    private bool estaDisparando;
    private int contador;

    private float finDelay;
    private float delay;
	void Start () {
        contador = 0;
        delay = 0f;
        finDelay = 0.1f;
        if (panelArmas != null)
        {
            panelArmas.SetActive(false);
        }
        estaDisparando = false;
    }

    // Update is called once per frame
    void Update() {
        if (delay <= finDelay)
        {
            delay = delay + Time.deltaTime;
        }
//#if UNITY_EDITOR// modo de disparar en compu

        // ESTO ES PARA PC
        if (Input.GetButtonDown("Fire1") && jugadorWindows)
        { 
            Disparar();
        }
        //----------------------
//#elif UNITY_STANDALONE
        //if (Input.GetButtonDown("Fire1"))
        //{
             //Disparar();
             //estaDisparando = true;
       // }
//#endif
    }
    public void Disparar()
    {
        
        if (Time.timeScale > 0)
        {

            if (Jugador.GetJugador().tipoPelota == 1 && generador != null && PelotaComun != null)
            {
                estaDisparando = true;
                GameObject go = PelotaComun.GetObject();
                Pelota pelota = go.GetComponent<Pelota>();
                go.transform.position = generador.transform.position + generador.transform.right;
                go.transform.rotation = generador.transform.rotation;
                pelota.Disparar();
                if(sonido != null && sonidoPelotaComun != null)
                {
                    sonido.clip = sonidoPelotaComun;
                    sonido.PlayOneShot(sonidoPelotaComun);
                }
            }
            if (Jugador.GetJugador().tipoPelota == 2 && pelotaDeHielo != null && generador != null && Jugador.GetJugador().GetMunicionPelotaHielo() > 0)
            {
                estaDisparando = true;
                // cambiar esto por un codigo que haga que la pelota en cuestion se teletrasporte y luego la rotacion de la misma sea
                // igual a la del generador luego de esto que active el gameObject
                //Instantiate(pelotaDeHielo, generador.transform.position + generador.transform.forward, generador.transform.rotation);
                GameObject go = pelotaDeHielo.GetObject();
                Pelota pelota = go.GetComponent<Pelota>();
                go.transform.position = generador.transform.position + generador.transform.right;
                go.transform.rotation = generador.transform.rotation;
                pelota.Disparar();
                Jugador.GetJugador().RestarMunicionHielo();
                if(sonido != null && sonidoPelotaHielo != null)
                {
                    sonido.clip = sonidoPelotaHielo;
                    sonido.PlayOneShot(sonidoPelotaHielo);
                }
            }
            if (Jugador.GetJugador().tipoPelota == 3 && pelotaFragmentadora != null && generador != null && Jugador.GetJugador().GetMunicionPelotaFragmentadora() > 0)
            {
                estaDisparando = true;
                // cambiar esto por un codigo que haga que la pelota en cuestion se teletrasporte y luego la rotacion de la misma sea
                // igual a la del generador luego de esto que active el gameObject
                //Instantiate(pelotaFragmentadora, generador.transform.position + generador.transform.forward, generador.transform.rotation);
                GameObject go = pelotaFragmentadora.GetObject();
                GestorPelotaFragmentadora pelota = go.GetComponent<GestorPelotaFragmentadora>();
                go.transform.position = generador.transform.position + generador.transform.forward;
                go.transform.rotation = generador.transform.rotation;
                pelota.Disparar();
                Jugador.GetJugador().RestarMunicionFragmentadora();
                if(sonido != null && sonidoPelotaFragmentadora != null)
                {
                    sonido.clip = sonidoPelotaFragmentadora;
                    sonido.PlayOneShot(sonidoPelotaFragmentadora);
                }
            }
            if (Jugador.GetJugador().tipoPelota == 4 && pelotaDanzarina != null && generador != null && Jugador.GetJugador().GetMunicionPelotaDanzarina() > 0)
            {
                estaDisparando = true;
                // cambiar esto por un codigo que haga que la pelota en cuestion se teletrasporte y luego la rotacion de la misma sea
                // igual a la del generador luego de esto que active el gameObject
                //Instantiate(pelotaDanzarina, generador.transform.position + generador.transform.forward, generador.transform.rotation);
                GameObject go = pelotaDanzarina.GetObject();
                Pelota pelota = go.GetComponent<Pelota>();
                go.transform.position = generador.transform.position + generador.transform.right;
                go.transform.rotation = generador.transform.rotation;
                pelota.Disparar();
                Jugador.GetJugador().RestarMunicionDanzarina();
                if(sonido != null && sonidoPelotaDanzarina != null)
                {
                    sonido.clip = sonidoPelotaDanzarina;
                    sonido.PlayOneShot(sonidoPelotaDanzarina);
                }
            }
            if (Jugador.GetJugador().tipoPelota == 5 && pelotaDeFuego != null && generador != null && Jugador.GetJugador().GetMunicionPelotaFuego() > 0)
            {
                estaDisparando = true;
                // cambiar esto por un codigo que haga que la pelota en cuestion se teletrasporte y luego la rotacion de la misma sea
                // igual a la del generador luego de esto que active el gameObject
                //Instantiate(pelotaDeFuego, generador.transform.position + generador.transform.forward, generador.transform.rotation);
                GameObject go = pelotaDeFuego.GetObject();
                Pelota pelota = go.GetComponent<Pelota>();
                go.transform.position = generador.transform.position + generador.transform.right;
                go.transform.rotation = generador.transform.rotation;
                pelota.Disparar();
                Jugador.GetJugador().RestarMunicionFuego();
                if(sonido != null && sonidoPelotaFuego != null)
                {
                    sonido.clip = sonidoPelotaFuego;
                    sonido.PlayOneShot(sonidoPelotaFuego);
                }
            }
            if (Jugador.GetJugador().tipoPelota == 6 && pelotaExplociva != null && generadorExplicivos != null && Jugador.GetJugador().GetMunicionPelotaExplosiva() > 0)
            {
                estaDisparando = true;
                // cambiar esto por un codigo que haga que la pelota en cuestion se teletrasporte y luego la rotacion de la misma sea
                // igual a la del generador luego de esto que active el gameObject
                //Instantiate(pelotaExplociva, generadorExplicivos.transform.position + generadorExplicivos.transform.forward, generador.transform.rotation);
                GameObject go = pelotaExplociva.GetObject();
                PelotaExpliciva pelota = go.GetComponent<PelotaExpliciva>();
                go.transform.position = generadorExplicivos.transform.position + generadorExplicivos.transform.right;
                go.transform.rotation = generadorExplicivos.transform.rotation;
                pelota.disparar();
                Jugador.GetJugador().RestarMunicionExplosiva();
                if(sonido != null && sonidoPelotaExplociva != null)
                {
                    sonido.clip = sonidoPelotaExplociva;
                    sonido.PlayOneShot(sonidoPelotaExplociva);
                }
            }
        }
        //estaDisparando = false;
    }
    public void CambiarArmaAndroid(int numeroArma)
    {
        switch (numeroArma)
        {
            case 1:
                Jugador.GetJugador().tipoPelota = 1;
                break;
            case 2:
                Jugador.GetJugador().tipoPelota = 2;
                break;
            case 3:
                Jugador.GetJugador().tipoPelota = 3;
                break;
            case 4:
                Jugador.GetJugador().tipoPelota = 4;
                break;
            case 5:
                Jugador.GetJugador().tipoPelota = 5;
                break;
            case 6:
                Jugador.GetJugador().tipoPelota = 6;
                break;
            default:
                Jugador.GetJugador().tipoPelota = 1;
                break;
        }
    }
    public void ActivarPanel()
    {
        if(contador == 2)
        {
            panelArmas.SetActive(false);
            contador = -1;
        }
        if(panelArmas != null && contador == 0)
        {
            panelArmas.SetActive(true);
            contador = contador + 1;
            if (GameManager.GetGameManager() != null)
            {
                GameManager.GetGameManager().pausa = true;
            }
        }
        if(contador == 1)
        {
            contador = contador + 1;
        }
        if(contador == -1)
        {
            contador = 0;
            if (GameManager.GetGameManager() != null)
            {
                GameManager.GetGameManager().pausa = false;
            }
            //Time.timeScale = 1;
        }
    }
    public bool GetEstaDisparando()
    {
        return estaDisparando;
    }
    public void SetEstaDisparando(bool _estaDisparando)
    {
        estaDisparando = _estaDisparando;
    }
}
