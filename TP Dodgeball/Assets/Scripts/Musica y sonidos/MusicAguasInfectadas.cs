using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAguasInfectadas : MonoBehaviour {

    // Use this for initialization
    public AudioClip[] clips;
    public GameObject[] enemigos;
    public AudioSource audioSource;
    public AudioClip clipInitial;
    public Shark shark;
    private int id;
    private bool playMusic;
    private bool startClips;
    private bool song1;
    private bool song2;
    private bool onceShark;
	void Start () {
        id = -1;
        onceShark = true;
        playMusic = true;
        startClips = false;
        audioSource.clip = clipInitial;
        audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(id);
        if(shark.gameObject.activeSelf == true && shark.life > 0 && onceShark)
        {
            
            song2 = true;
            onceShark = false;
        }
        if(shark.life <= 0 && !onceShark)
        {
            onceShark = true;
            song1 = true;
        }
        if(!startClips)
        {
            CheckMusic1();
        }
        else
        {
            if(song1)
            {
                Debug.Log("Song1");
                audioSource.clip = clips[0];
                audioSource.Play();
                song1 = false;
            }
            if(song2)
            {
                Debug.Log("Song2");
                audioSource.clip = clips[1];
                audioSource.Play();
                song2 = false;
            }
        }
       
	}
    public void CheckMusic1()
    {
        int count = 0;
        for(int i = 0; i< enemigos.Length; i++)
        {
            if(enemigos[i].activeSelf == false)
            {
                count++;
            }
        }
        if(count >= enemigos.Length)
        {
            song1 = true;
            startClips = true;
        }
        else
        {
            count = 0;
        }
    }
}
