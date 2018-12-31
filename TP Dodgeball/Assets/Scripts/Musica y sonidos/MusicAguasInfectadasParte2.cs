using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAguasInfectadasParte2 : MonoBehaviour {

    // Use this for initialization
    public Submarine[] submarines;
    public Kraken kraken;
    public AudioSource audioSource;
    public AudioClip[] clips;
    public AudioClip clipInitial;
    private bool startClips;
    private bool song1;
    private bool song2;
    private bool onceKraken;
    void Start () {
        onceKraken = true;
        startClips = false;
        audioSource.clip = clipInitial;
        audioSource.Play();
	}

    // Update is called once per frame
    void Update() {
        if (kraken.gameObject.activeSelf == true && kraken.life > 0 && onceKraken)
        {
            song2 = true;
            onceKraken = false;
        }
        if(kraken.gameObject.activeSelf == false && kraken.life <= 0 && !onceKraken)
        {
            song1 = true;
            onceKraken = true;
        }
		if(!startClips)
        {
            CheckFinishMusic1();
        }
        if(song1)
        {
            audioSource.clip = clips[0];
            audioSource.Play();
            song1 = false;
        }
        if(song2)
        {
            audioSource.clip = clips[1];
            audioSource.Play();
            song2 = false;
        }
	}
    public void CheckFinishMusic1()
    {
        int Count = 0;
        for (int i = 0; i < submarines.Length; i++)
        {
            if (submarines[i].life <= 0)
            {
                Count++;
            }
        }
        if (Count >= submarines.Length)
        {
            song1 = true;
            startClips = true;
        }
        else
        {
            Count = 0;
        }
    }
}
