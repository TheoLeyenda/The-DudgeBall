using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnjambreDePiranias : MonoBehaviour {

    // Use this for initialization

    public enum States
    {
        Nadando = 0,
        Seguir,
        Quieto,
    }

    public States estados;
    private int id = 0;

    private Jugador jugador;
    public bool volverPuntoInicio;
    public Transform[] waypoints;
    public Pirania[] pirania;
    private Vector3 puntoInicio;
    private Quaternion rotacionInicio;
    public float velocidadMovimiento;
    private bool autodestrucion;
    void Start()
    {
        if (Jugador.instanciaJugador != null)
        {
            jugador = Jugador.instanciaJugador;
        }
        puntoInicio = transform.position;
        rotacionInicio = transform.rotation;
        if (pirania.Length > 0)
        {
            for (int i = 0; i < pirania.Length; i++)
            {
                if (pirania[i] != null)
                {
                    pirania[i].activarPirania = false;
                }
            }
            autodestrucion = false;
        }
    }
    void Update()
    {
        if (volverPuntoInicio)
        {
            if (jugador != null)
            {
                if (jugador.vida <= 0)
                {
                    gameObject.SetActive(false);
                    VolverPuntoInicio();
                }
            }
        }
        if (!autodestrucion)
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
            case (int)States.Seguir:
                Seguir();
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
                transform.position = transform.position + transform.forward * Time.deltaTime * velocidadMovimiento;
                Vector3 diff = target - this.transform.position;

                if (diff.magnitude < 0.3f)
                {
                    id++;
                    if (id >= waypoints.Length)
                    {
                        for (int i = 0; i < pirania.Length; i++)
                        {
                            pirania[i].activarPirania = true;
                        }
                        autodestrucion = true;
                    }
                }
            }
        }
    }
    public void VolverPuntoInicio()
    {
        transform.position = puntoInicio;
        estados = States.Nadando;
        autodestrucion = false;
        id = 0;
        transform.rotation = rotacionInicio;
        for(int i = 0; i< pirania.Length; i++)
        {
            pirania[i].transform.position = pirania[i].GetPuntoInicio();
            pirania[i].transform.rotation = pirania[i].GetRotacionInicio();
            pirania[i].activarPirania = false;
            
        }
    }
    public void Seguir()
    {
        if (Jugador.GetJugador() != null)
        {
            Vector3 target = jugador.transform.position;
            transform.LookAt(target);
            transform.position = transform.position + transform.forward * Time.deltaTime * velocidadMovimiento;
        }
    }
    
	// Update is called once per frame
	
}
