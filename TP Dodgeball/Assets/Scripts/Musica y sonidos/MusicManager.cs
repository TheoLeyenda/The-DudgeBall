using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class MusicManager : MonoBehaviour {

    // Use this for initialization
    public AudioSource audioSource;
    public AudioClip[] songSequence;
    private int id;
	void Start () {
        id = -1;
	}
	
	// Update is called once per frame
	void Update () {
        CheckLimitId();
	}
    public void PlayMusic()
    {
        if (songSequence[id] != null)
        {
            audioSource.clip = songSequence[id];
            audioSource.Play();
        }
    }
    public void PlayMusic(int numCancion)
    {
        if (songSequence[numCancion] != null)
        {
            audioSource.clip = songSequence[numCancion];
            audioSource.Play();
        }
    }
    public void CheckLimitId()
    {
        if(id>= songSequence.Length)
        {
            id = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            id++;
            PlayMusic();
        }
    }
}

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
