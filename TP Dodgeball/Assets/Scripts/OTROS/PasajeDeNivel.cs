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
}
