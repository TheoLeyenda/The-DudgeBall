using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeStaticTurret : MonoBehaviour {

    // Use this for initialization
    public StaticShooter staticTurret;
    private void OnDisable()
    {
        staticTurret.SetShooting(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            staticTurret.SetShooting(true);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            staticTurret.SetShooting(false);
        }
    }
}
