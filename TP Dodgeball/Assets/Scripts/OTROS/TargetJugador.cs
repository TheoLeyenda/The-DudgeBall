using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetJugador : MonoBehaviour {
    public float ElevarVista;
	void Update () {
        TargetearJugador();
	}
    public void TargetearJugador()
    {
        if(Jugador.GetJugador() != null)
        {
            transform.LookAt(new Vector3(Jugador.GetJugador().transform.position.x,Jugador.GetJugador().transform.position.y+ ElevarVista,Jugador.GetJugador().transform.position.z));
        }
    }
}
