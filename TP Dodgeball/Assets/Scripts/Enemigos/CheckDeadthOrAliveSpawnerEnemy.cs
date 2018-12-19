using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheckDeadthOrAliveSpawnerEnemy : MonoBehaviour {

    // Use this for initialization
    private bool once = true;
    public int zone;
    private LabyrinthManager labyrinthManager;
    private void Start()
    {
        if(LabyrinthManager.instanceLabyrinthManager != null)
        {
            labyrinthManager = LabyrinthManager.instanceLabyrinthManager;
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

