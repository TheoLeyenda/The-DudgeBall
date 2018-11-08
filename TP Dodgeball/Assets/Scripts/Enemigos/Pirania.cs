using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirania : MonoBehaviour {

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
}
