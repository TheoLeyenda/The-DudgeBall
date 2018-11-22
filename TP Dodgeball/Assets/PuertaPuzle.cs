using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuertaPuzle : MonoBehaviour {

    // Use this for initialization
    public Text AvisoAndroid;
    public Text AvisoWindows;
    public PuertaRejas puerta;
    private bool puzleCompletado;
    private Jugador jugador;
    private int barrilesDerribados;
    public GameObject[] barriles;
    private float time;
    private float auxTime;
	void Start () {
        time = 0.1f;
        auxTime = time;
        if (Jugador.instanciaJugador != null)
        {
            jugador = Jugador.instanciaJugador;
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (barrilesDerribados >= 3)
        {
            puzleCompletado = true;
        }
        if (barrilesDerribados >= 1 && barrilesDerribados < 3)
        {
            time = time - Time.deltaTime;
            if (time <= 0)
            {
                barrilesDerribados = 0;
                time = auxTime;
                for (int i = 0; i < barriles.Length; i++)
                {
                    if (barriles[i] != null)
                    {
                        barriles[i].SetActive(true);
                    }
                }
            }
        }
	}
    public void sumarBarrilDerribado()
    {
        barrilesDerribados++;
    }
    public void SetPuzleeCompletado(bool puzzle)
    {
        puzleCompletado = puzzle;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (puzleCompletado)
            {
                puerta.SetAbrirPuerta(true);
            }
            if (puzleCompletado == false)
            {
                if (jugador.jugadorAndroid)
                {
                    AvisoAndroid.gameObject.SetActive(true);
                }
                if(jugador.jugadorWindows)
                {
                    AvisoWindows.gameObject.SetActive(true);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        { 
            if (jugador.jugadorAndroid)
            {
                AvisoAndroid.gameObject.SetActive(false);
            }
            if (jugador.jugadorWindows)
            {
                AvisoWindows.gameObject.SetActive(false);
            }
        }
    }
}
