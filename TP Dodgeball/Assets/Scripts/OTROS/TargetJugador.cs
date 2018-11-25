using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetJugador : MonoBehaviour {
    public float ElevarVista;
    private Jugador jugador;
    private void Start()
    {
        if(Jugador.instanciaJugador != null)
        {
            jugador = Jugador.instanciaJugador;
        }
    }
    void Update () {
        TargetearJugador();
	}
    public void TargetearJugador()
    {
        if(Jugador.GetJugador() != null)
        {
            transform.LookAt(new Vector3(jugador.transform.position.x, jugador.transform.position.y+ ElevarVista, jugador.transform.position.z));
        }
    }
}
