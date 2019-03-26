using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherRange : MonoBehaviour {

    // Use this for initialization
    public Shooter shooter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            shooter.aviableShoot = true;
            shooter.dilay = shooter.auxDilay;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            shooter.aviableShoot = false;
            shooter.dilay = shooter.auxDilay;
        }
    }
}
