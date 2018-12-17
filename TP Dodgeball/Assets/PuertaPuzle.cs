using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class PuertaPuzle : MonoBehaviour {

    // Use this for initialization
    public Text noticeAndroid;
    public Text noticeWindows;
    public PuertaRejas door;
    private bool completedPuzzle;
    private Jugador player;
    private int downBarrels;
    public GameObject[] barrels;
    private float time;
    private float auxTime;
	void Start () {
        time = 0.1f;
        auxTime = time;
        if (Jugador.InstancePlayer != null)
        {
            player = Jugador.InstancePlayer;
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (downBarrels >= 3)
        {
            completedPuzzle = true;
        }
        if (downBarrels >= 1 && downBarrels < 3)
        {
            time = time - Time.deltaTime;
            if (time <= 0)
            {
                downBarrels = 0;
                time = auxTime;
                for (int i = 0; i < barrels.Length; i++)
                {
                    if (barrels[i] != null)
                    {
                        barrels[i].SetActive(true);
                    }
                }
            }
        }
	}
    public void AddBarrelDown()
    {
        downBarrels++;
    }
    public void SetPuzzleCompleted(bool puzzle)
    {
        completedPuzzle = puzzle;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (completedPuzzle)
            {
                door.SetOpenDoor(true);
            }
            if (completedPuzzle == false)
            {
                if (player.playerAndroid)
                {
                    noticeAndroid.gameObject.SetActive(true);
                }
                if(player.playerWindows)
                {
                    noticeWindows.gameObject.SetActive(true);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        { 
            if (player.playerAndroid)
            {
                noticeAndroid.gameObject.SetActive(false);
            }
            if (player.playerWindows)
            {
                noticeWindows.gameObject.SetActive(false);
            }
        }
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)