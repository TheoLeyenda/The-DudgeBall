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
	void Start () {
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
            if (Jugador.GetJugador() != null)
            { 
                Jugador.GetJugador().vida = 0;
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
        if (Jugador.GetJugador() != null)
        {
            if (Jugador.GetJugador().vida < 0)
            {
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
