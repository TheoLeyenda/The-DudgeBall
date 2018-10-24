using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PasajeDeNivel : MonoBehaviour {

    // Use this for initialization
    public string nivel;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SceneManager.LoadScene(nivel);
        }
    }
}
