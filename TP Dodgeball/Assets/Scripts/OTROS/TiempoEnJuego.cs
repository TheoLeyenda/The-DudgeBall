using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TiempoEnJuego : MonoBehaviour {

    // Use this for initialization
    public Text tiempo;
    public float minutos;
    public float segundos;
    private float auxMinutos;
    private float auxSegundos;
    private Jugador jugador;
    private bool tiempoAcabado = false;
	void Start () {
        if(Jugador.instanciaJugador != null)
        {
            jugador = Jugador.instanciaJugador;
        }
        auxMinutos = minutos;
        auxSegundos = segundos;
	}
	
	// Update is called once per frame
	void Update () {
        CheckTiempo();
	}
    public void CheckTiempo()
    {
        if (segundos <= 0 && minutos <= 0)
        {
            if (jugador != null)
            {
                tiempoAcabado = true;
                jugador.vida = 0;
                segundos = auxSegundos;
                minutos = auxMinutos;
                if (segundos >= 10)
                {
                    tiempo.text = (int)minutos + ":" + (int)segundos;
                }
                if (segundos < 10)
                {
                    tiempo.text = (int)minutos + ":0" + (int)segundos;
                }

            }
        }
        if (jugador != null)
        {
            if (tiempoAcabado)
            {
                segundos = auxSegundos;
                minutos = auxMinutos;
                tiempoAcabado = false;
                if (segundos >= 10)
                {
                    tiempo.text = (int)minutos + ":" + (int)segundos;
                }
                if (segundos < 10)
                {
                    tiempo.text = (int)minutos + ":0" + (int)segundos;
                }
            }
        }
        if (segundos <= 0 && minutos > 0)
        {
            segundos = 59;
            minutos--;
        }
        if (segundos >= 10)
        {
            tiempo.text = (int)minutos + ":" + (int)segundos;
        }
        if (segundos < 10)
        {
            tiempo.text = (int)minutos + ":0" + (int)segundos;
        }
        segundos = segundos - Time.deltaTime;
    }
}
