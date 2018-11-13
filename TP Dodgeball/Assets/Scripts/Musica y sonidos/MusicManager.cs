using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    // Use this for initialization
    public AudioSource audioSource;
    public AudioClip[] secuenciaCanciones;
    private int id;
	void Start () {
        id = -1;
	}
	
	// Update is called once per frame
	void Update () {
        checkLimiteId();
	}
    public void Reproducir()
    {
        if (secuenciaCanciones[id] != null)
        {
            audioSource.clip = secuenciaCanciones[id];
            audioSource.Play();
        }
    }
    public void Reproducir(int numCancion)
    {
        if (secuenciaCanciones[numCancion] != null)
        {
            audioSource.clip = secuenciaCanciones[numCancion];
            audioSource.Play();
        }
    }
    public void checkLimiteId()
    {
        if(id>= secuenciaCanciones.Length)
        {
            id = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            id++;
            Reproducir();
        }
    }
}
