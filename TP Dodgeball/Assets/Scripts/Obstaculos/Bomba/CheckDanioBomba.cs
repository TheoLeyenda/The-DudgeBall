using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDanioBomba : MonoBehaviour {

    // Use this for initialization
    public float danio;
    private Jugador jugador;
    private void Start()
    {
        if(Jugador.instanciaJugador != null)
        {
            jugador = Jugador.instanciaJugador;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (jugador != null)
            {
                if (jugador.blindaje > 0)
                {
                    jugador.blindaje = 0;
                }
                else
                {
                    jugador.vida = jugador.vida - danio;
                }
            }
        }
    }
}
