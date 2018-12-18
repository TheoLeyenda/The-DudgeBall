using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class SetWayPointPirania : MonoBehaviour {

    // Use this for initialization
    public List<Pirania> piranha;
    public List<Transform> WaypointsWindows;
    public List<Transform> WaypointAndroid;
    private int idPiranha = 0;
    private int idWaypointWindows = 0;
    private int idWaypointAndroid;
    private Jugador instancePlayer;
	void Start () {
        if (Jugador.InstancePlayer != null)
        {
            instancePlayer = Jugador.InstancePlayer;
        }
        GameObject[] auxWayPointWindows;
        auxWayPointWindows = GameObject.FindGameObjectsWithTag("WaypointPiraniaWindows");

        GameObject[] auxPirania = null;
        for (int i = 0; i < piranha.Count; i++)
        {
            auxPirania[i] = piranha[i].gameObject;
        }

        GameObject[] auxWayPointAndroid;
        auxWayPointAndroid = GameObject.FindGameObjectsWithTag("WaypointPiraniaAndroid");

        for (int i = 0; i < auxWayPointWindows.Length; i++)
        {
            WaypointsWindows.Add(auxWayPointWindows[i].transform);
        }

        for(int i = 0; i< auxPirania.Length; i++)
        {
            piranha.Add(auxPirania[i].GetComponent<Pirania>());
        }

        for(int i = 0; i< auxWayPointAndroid.Length; i++)
        {
            WaypointAndroid.Add(auxWayPointAndroid[i].transform);
        }
        while(piranha.Count != 0)
        {
            while (WaypointsWindows.Count != 0)
            {
                if(instancePlayer.playerAndroid)
                {
                    piranha[idPiranha].SetWaypoint(WaypointAndroid[idWaypointAndroid]);
                    piranha.Remove(piranha[idPiranha]);
                    WaypointAndroid.Remove(WaypointAndroid[idWaypointAndroid]);
                }
                if(instancePlayer.playerWindows)
                {
                    piranha[idPiranha].SetWaypoint(WaypointsWindows[idWaypointWindows]);
                    piranha.Remove(piranha[idPiranha]);
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
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)