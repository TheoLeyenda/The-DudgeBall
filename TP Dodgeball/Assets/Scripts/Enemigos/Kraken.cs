using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraken : Enemigo {

    // Use this for initialization
    private States estados;
    private Rigidbody rig;
    private Vector3 posJugador;
    private int id = 0;

    public float FuerzaImpulsoMov;
    public Transform[] waypoints;
    public enum States
    {
        Nadando = 0,
        Atacar,
        Retirse,
        Count
    }

    public enum Events
    {
        EnVista = 0,
        FueraDeVista,
        EnRangoDeAtaque,
        FueraDelRangoDeAtaque,
        LuegoDeAtacar,
        VolviendoQuieto,
        Count
    }

    void Start () {
        rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
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
            case (int)States.Retirse:
                Retirarse();
                break;
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
                if (id >= waypoints.Length)
                {
                    id = 0;
                }

            }
        }
        else
        {
            id = 0;
        }
    }
    public void Atacar()
    {

    }
    public void Retirarse()
    {

    }
    public void UpdatePositionPlayer()
    {
        if (Jugador.GetJugador() != null)
        {
            posJugador = Jugador.GetJugador().transform.position;
        }
    }
}
