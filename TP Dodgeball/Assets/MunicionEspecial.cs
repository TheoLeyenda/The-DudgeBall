using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunicionEspecial : MonoBehaviour {

    // Use this for initialization
    public GameObject pickUp;
    private Jugador jugador;
	void Start () {
		if(Jugador.instanciaJugador != null)
        {
            jugador = Jugador.instanciaJugador;
        }
	}
	
	// Update is called once per frame
	void Update () {
        checkMunicionJugador();
	}
    public void checkMunicionJugador()
    {
        if(jugador.GetMunicionPelotaFragmentadora() <= 0)
        {
            pickUp.gameObject.SetActive(true);
        }
    }
}
