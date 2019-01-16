using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeStaticTurret : MonoBehaviour {

    // Use this for initialization
    public StaticShooter staticTurret;
    public Tower tower;
    private void OnDisable()
    {
        if(staticTurret != null)
        {
            staticTurret.SetShooting(false);
        }
        if(tower != null)
        {
            tower.SetShooting(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && staticTurret != null)
        {
            staticTurret.SetShooting(true);
        }
        if(other.tag == "Player" && tower != null)
        {
            tower.SetShooting(true);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" && staticTurret != null)
        {
            staticTurret.SetShooting(false);
        }
        if(other.tag == "Player" && tower != null)
        {
            tower.SetShooting(false);
        }
    }
}
