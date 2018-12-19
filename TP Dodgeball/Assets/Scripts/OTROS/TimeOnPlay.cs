using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeOnPlay : MonoBehaviour {

    // Use this for initialization
    public Text time;
    public float minutes;
    public float seconds;
    private float auxMinutes;
    private float auxSeconds;
    private Player player;
    private bool tiempoAcabado = false;
	void Start () {
        if(Player.InstancePlayer != null)
        {
            player = Player.InstancePlayer;
        }
        auxMinutes = minutes;
        auxSeconds = seconds;
	}
	
	// Update is called once per frame
	void Update () {
        CheckTime();
	}
    public void CheckTime()
    {
        if (seconds <= 0 && minutes <= 0)
        {
            if (player != null)
            {
                tiempoAcabado = true;
                player.life = 0;
                seconds = auxSeconds;
                minutes = auxMinutes;
                if (seconds >= 10)
                {
                    time.text = (int)minutes + ":" + (int)seconds;
                }
                if (seconds < 10)
                {
                    time.text = (int)minutes + ":0" + (int)seconds;
                }

            }
        }
        if (player != null)
        {
            if (tiempoAcabado)
            {
                seconds = auxSeconds;
                minutes = auxMinutes;
                tiempoAcabado = false;
                if (seconds >= 10)
                {
                    time.text = (int)minutes + ":" + (int)seconds;
                }
                if (seconds < 10)
                {
                    time.text = (int)minutes + ":0" + (int)seconds;
                }
            }
        }
        if (seconds <= 0 && minutes > 0)
        {
            seconds = 59;
            minutes--;
        }
        if (seconds >= 10)
        {
            time.text = (int)minutes + ":" + (int)seconds;
        }
        if (seconds < 10)
        {
            time.text = (int)minutes + ":0" + (int)seconds;
        }
        seconds = seconds - Time.deltaTime;
    }
}