using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirardRange : MonoBehaviour {

    // Use this for initialization
    public Wizard wizard;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            wizard.aviableShoot = true;
            wizard.dilay = wizard.auxDilay;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            wizard.aviableShoot = false;
            //wizard.dilay = wizard.auxDilay;
        }
    }
}
