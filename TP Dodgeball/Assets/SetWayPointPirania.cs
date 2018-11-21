using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWayPointPirania : MonoBehaviour {

    // Use this for initialization
    public List<Pirania> piranias;
    public List<Transform> WaypointsWindows;
    public List<Transform> WaypointAndroid;
    private int idPirania = 0;
    private int idWaypointWindows = 0;
    private int idWaypointAndroid;
    private Jugador instanciaJugador;
	void Start () {
        if (Jugador.instanciaJugador != null)
        {
            instanciaJugador = Jugador.instanciaJugador;
        }
        GameObject[] auxWayPointWindows;
        auxWayPointWindows = GameObject.FindGameObjectsWithTag("WaypointPiraniaWindows");

        GameObject[] auxPirania = null;
        for (int i = 0; i < piranias.Count; i++)
        {
            auxPirania[i] = piranias[i].gameObject;
        }

        GameObject[] auxWayPointAndroid;
        auxWayPointAndroid = GameObject.FindGameObjectsWithTag("WaypointPiraniaAndroid");

        for (int i = 0; i < auxWayPointWindows.Length; i++)
        {
            WaypointsWindows.Add(auxWayPointWindows[i].transform);
        }

        for(int i = 0; i< auxPirania.Length; i++)
        {
            piranias.Add(auxPirania[i].GetComponent<Pirania>());
        }

        for(int i = 0; i< auxWayPointAndroid.Length; i++)
        {
            WaypointAndroid.Add(auxWayPointAndroid[i].transform);
        }
        while(piranias.Count != 0)
        {
            while (WaypointsWindows.Count != 0)
            {
                if(instanciaJugador.jugadorAndroid)
                {
                    piranias[idPirania].SetWaypoint(WaypointAndroid[idWaypointAndroid]);
                    piranias.Remove(piranias[idPirania]);
                    WaypointAndroid.Remove(WaypointAndroid[idWaypointAndroid]);
                }
                if(instanciaJugador.jugadorWindows)
                {
                    piranias[idPirania].SetWaypoint(WaypointsWindows[idWaypointWindows]);
                    piranias.Remove(piranias[idPirania]);
                    WaypointsWindows.Remove(WaypointsWindows[idWaypointWindows]);
                }
                //idWaypoint++;
            }
            //idPirania++;
        }

        
    }
	
	// Update is called once per frame
	void Update () {
         
	}
}
