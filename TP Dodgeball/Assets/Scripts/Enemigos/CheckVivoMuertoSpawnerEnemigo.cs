using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
public class CheckVivoMuertoSpawnerEnemigo : MonoBehaviour {

    // Use this for initialization
    private bool once = true;
    public int zone;
    private LaberintoManager labyrinthManager;
    private void Start()
    {
        if(LaberintoManager.instanceLabyrinthManager != null)
        {
            labyrinthManager = LaberintoManager.instanceLabyrinthManager;
        }
    }
    private void OnDisable()
    {
        if(once)
        {
            once = false;
            if (labyrinthManager != null)
            {
                labyrinthManager.AddSpawnersDestroyedByArea(zone);
            }
        }
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
