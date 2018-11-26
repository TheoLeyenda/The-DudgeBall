using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarMultiplesPuertas : MonoBehaviour {

    // Use this for initialization
    private EstructuraDatosAuxiliares estructuraDatosAuxiliares;
    private Jugador jugador;
    public PuertaRejas[] puertas;
    public Enemigo enemigo;
    public SphereCollider sphereCollider;
	void Start () { 
        if(EstructuraDatosAuxiliares.estructuraDatosAuxiliares != null)
        {
            estructuraDatosAuxiliares = EstructuraDatosAuxiliares.estructuraDatosAuxiliares;
        }
        if(Jugador.instanciaJugador != null)
        {
            jugador = Jugador.instanciaJugador;
        }
	}
	
	// Update is called once per frame
	void Update () {
        CheckAbrirPuertas();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            enemigo.gameObject.SetActive(true);
            for (int i = 0; i < puertas.Length; i++)
            {
                puertas[i].SetCerrarPuerta(true);
            }
            if (sphereCollider != null)
            {
                sphereCollider.enabled = false;
            }
        }
    }
    public void CheckAbrirPuertas()
    {
        if(enemigo != null)
        {
            if (enemigo.vida <= 0)
            {
                estructuraDatosAuxiliares.SetDatosJugador(jugador);
                estructuraDatosAuxiliares.SetValoresDelJugador(jugador);
                for (int i = 0; i < puertas.Length; i++)
                {
                    puertas[i].SetAbrirPuerta(true);
                }
                if (sphereCollider != null)
                {
                    sphereCollider.enabled = true;
                }
            }
        }
    }

}
