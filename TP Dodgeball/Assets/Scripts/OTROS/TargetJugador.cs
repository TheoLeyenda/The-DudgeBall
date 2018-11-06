using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetJugador : MonoBehaviour {

	void Update () {
        TargetearJugador();
	}
    public void TargetearJugador()
    {
        if(Jugador.GetJugador() != null)
        {
            transform.LookAt(Jugador.GetJugador().transform.position);
        }
    }
}
