using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorAccordingEnemyLife : MonoBehaviour {

    // Use this for initialization
    public BarsDoor gateOfBars;
    public Enemy[] Enemy;
    private int countDeathEnemys;
    private bool openDoor;
	void Start () {
        openDoor = false;
	}
	
	// Update is called once per frame
	void Update () {
        CheckDeathEnemys();
        CheckOpenDoor();
	}
    public void CheckOpenDoor()
    {
        if(openDoor)
        {
            if(gateOfBars != null)
            {
                gateOfBars.SetOpenDoor(true);
            }
        }
    }
    public void CheckDeathEnemys()
    {
        for(int i = 0; i< Enemy.Length; i++)
        {
            if (Enemy[i] != null)
            {
                if (Enemy[i].life <= 0)
                {
                    countDeathEnemys++;
                }
            }
        }
        if(countDeathEnemys >= Enemy.Length)
        {
            openDoor = true;
        }
        else
        {
            countDeathEnemys = 0;
        }
    }
}