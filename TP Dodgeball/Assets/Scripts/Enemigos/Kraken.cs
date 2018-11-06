using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraken : Enemigo {

    // Use this for initialization
    private States estados;
    private Rigidbody rig;
    private Vector3 posJugador;
    private int id = 0;
    private bool impulsar;

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
        impulsar = true;
        id = 0;
        estados = States.Nadando;
        rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        updateHP();
        UpdateStates();
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
                // HACER QUE TENGA UN PEQUEÑO IMPULSO AL MOVERSE Y CUANDO ESE IMPULSO VUELVA A CERO REINICIARLO.
                rig.velocity = Vector3.zero;
                rig.angularVelocity = Vector3.zero;
                //rig.AddRelativeForce(camara.transform.forward * potencia, ForceMode.Impulse);

                rig.AddForce(transform.right * FuerzaImpulsoMov, ForceMode.Impulse);
                impulsar = false;

                Debug.Log("Hola");
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WaypointRandom")
        {
            //float random = Random.Range(1, 100);
            //if (random >= 80)
            //{
                //estados = States.Atacar;
            //}
            //if (random < 80)
            //{
                id++;
                if (id >= waypoints.Length)
                {
                    id = 0;
                }
            //}
            //random = 0;
        }
        if (other.tag == "Waypoint")
        {
            id++;
            if (id >= waypoints.Length)
            {
                id = 0;
            }
        }
    }
}
