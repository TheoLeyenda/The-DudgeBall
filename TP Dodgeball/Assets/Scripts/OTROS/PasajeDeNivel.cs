using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PasajeDeNivel : MonoBehaviour {

    // Use this for initialization
    public string nivel;
    public int numeroNivel;
    public bool NivelPorNumero;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (EstructuraDatosAuxiliares.GetEstructuraDatosAuxiliares() != null)
            {

                EstructuraDatosAuxiliares.GetEstructuraDatosAuxiliares().SetDatosJugador(Jugador.GetJugador());
                if (Jugador.GetJugador().blindaje > 0)
                {
                    EstructuraDatosAuxiliares.GetEstructuraDatosAuxiliares().DatosJugador.blindaje = Jugador.GetJugador().blindaje;
                    Jugador.GetJugador().logoBlindaje.SetActive(true);
                    Jugador.GetJugador().textBlindaje.gameObject.SetActive(true);
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
