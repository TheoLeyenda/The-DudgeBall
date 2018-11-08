using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirania : Enemigo {

    // Use this for initialization
    public enum States
    {
        Nadando = 0,
        Atacar,
        Quieto,
    }
    private int id = 0;

    public States estados;
    public float velocidadAtaque;
    public float velocidadMovimiento;
    public float danio;
    public bool activarPirania;
    public Transform[] waypoints;
    public Transform waypointJugadorAndroid;
    public Transform waypointJugadorWindows;
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
       
        if (activarPirania)
        {
            UpdateStates();
           
        }
        if (GetMuerto())
        {
            if (!estoyEnPool)
            {
                gameObject.SetActive(false);
            }
        }


    }
    public void UpdateStates()
    {

        switch ((int)estados)
        {
            case (int)States.Nadando:
                Nadar();
                break;
            case (int)States.Atacar:
                Atacar();
                break;
        }
    }
    public void Atacar()
    {
        if (Jugador.GetJugador() != null)
        {
            if (Jugador.GetJugador().jugadorWindows && Jugador.GetJugador().jugadorAndroid == false)
            {
                if (waypointJugadorWindows != null)
                {
                    Vector3 target = waypointJugadorWindows.position;
                    transform.LookAt(target);
                    if (transform.position != target)
                    {
                        transform.position = transform.position + transform.forward * Time.deltaTime * velocidadAtaque;
                    }
                }
            }
            if (Jugador.GetJugador().jugadorAndroid && Jugador.GetJugador().jugadorWindows == false)
            {
                if(waypointJugadorAndroid != null)
                {
                    Vector3 target = waypointJugadorAndroid.position;
                    transform.LookAt(target);
                    if (transform.position != target)
                    {
                        transform.position = transform.position + transform.forward * Time.deltaTime * velocidadAtaque;
                    }
                }
            }
        }
    }
    public void Nadar()
    {
        if (waypoints.Length > 0)
        {

            if (waypoints[id] != null)
            {
                Vector3 target = waypoints[id].position;
                transform.LookAt(target);
                transform.position = transform.position + transform.forward * Time.deltaTime * velocidadMovimiento;
                Vector3 diff = target - this.transform.position;

                if (diff.magnitude < 0.3f)
                {
                    id++;
                    if(id >= waypoints.Length)
                    {
                        id = 0;
                    }
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
        if(other.tag == "Player")
        {
            if (Jugador.GetJugador() != null)
            {
                Jugador.GetJugador().vida = Jugador.GetJugador().vida - danio;
            }
        }
        if (other.gameObject.tag == "PelotaComun")
        {
            if (Jugador.GetJugador() != null)
            {
                vida = vida - (GetDanioBolaComun() + Jugador.GetJugador().GetDanioAdicionalPelotaComun());
                EstaMuerto();
                if (Jugador.GetJugador().GetDoblePuntuacion())
                {
                    Jugador.GetJugador().SumarPuntos(10 * 2);
                }
                else
                {
                    Jugador.GetJugador().SumarPuntos(10);
                }
            }
        }
        if (other.gameObject.tag == "PelotaDeHielo")
        {
            if (Jugador.GetJugador() != null)
            {
                if (Jugador.GetJugador().GetDoblePuntuacion())
                {
                    Jugador.GetJugador().SumarPuntos(10 * 2);
                }
                else
                {
                    Jugador.GetJugador().SumarPuntos(10);
                }
                vida = vida - (GetDanioBolaHielo() + Jugador.GetJugador().GetDanioAdicionalPelotaHielo());
            }
            EstaMuerto();
        }
        if (other.gameObject.tag == "MiniPelota")
        {
            if (Jugador.GetJugador() != null)
            {
                if (Jugador.GetJugador().GetDoblePuntuacion())
                {
                    Jugador.GetJugador().SumarPuntos(10 * 2);
                }
                else
                {
                    Jugador.GetJugador().SumarPuntos(10);
                }
                vida = vida - (GetDanioMiniBola() + Jugador.GetJugador().GetDanioAdicionalMiniPelota());
                EstaMuerto();
            }
        } 
        /*if (other.gameObject.tag == "PelotaExplociva")
        {
            if (Jugador.GetJugador() != null)
            {
                if (Jugador.GetJugador().GetDoblePuntuacion())
                {
                    Jugador.GetJugador().SumarPuntos(20 * 2);
                }
                else
                {
                    Jugador.GetJugador().SumarPuntos(20);
                }
                vida = vida - (GetDanioBolaExplociva() + Jugador.GetJugador().GetDanioAdicionalPelotaExplociva());
            }
            EstaMuerto();

        }*/
    }

}
