using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PasajeDeNivel : MonoBehaviour {

    // Use this for initialization
    public string nivel;
    public int numeroNivel;
    public bool NivelPorNumero;
    private Jugador jugador;
    private void Start()
    {
        if (Jugador.instanciaJugador != null)
        {
            jugador = Jugador.instanciaJugador;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (EstructuraDatosAuxiliares.GetEstructuraDatosAuxiliares() != null)
            {

                EstructuraDatosAuxiliares.GetEstructuraDatosAuxiliares().SetDatosJugador(jugador);
                if (jugador.blindaje > 0)
                {
                    EstructuraDatosAuxiliares.GetEstructuraDatosAuxiliares().DatosJugador.blindaje = jugador.blindaje;
                    jugador.logoBlindaje.SetActive(true);
                    jugador.textBlindaje.gameObject.SetActive(true);
                }
            }
            if (nivel != null && !NivelPorNumero)
            {
                SceneManager.LoadScene(nivel);
            }
            if(NivelPorNumero)
            {
                SceneManager.LoadScene(numeroNivel);
            }
        }
    }
    public void pasarNivel(string nivel)
    {

        if (nivel != null && !NivelPorNumero)
        {
            SceneManager.LoadScene(nivel);
        }
        if (NivelPorNumero)
        {
            SceneManager.LoadScene(numeroNivel);
        }
    }
}
