using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDanioBomba : MonoBehaviour {

    // Use this for initialization
    public float danio;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (Jugador.GetJugador() != null)
            {
                if (Jugador.GetJugador().blindaje > 0)
                {
                    Jugador.GetJugador().blindaje = 0;
                }
                else
                {
                    Jugador.GetJugador().vida = Jugador.GetJugador().vida - danio;
                }
            }
        }
    }
}
