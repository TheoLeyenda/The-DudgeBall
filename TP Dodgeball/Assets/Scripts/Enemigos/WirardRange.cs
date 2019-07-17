using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirardRange : MonoBehaviour {

    // Use this for initialization
    public Wizard wizard;

   
    private void OnTriggerStay(Collider other)
    {
        if (GetComponent<SphereCollider>() != null && this.gameObject.tag == "GeneradorPelotaEnemigo" && gameObject.tag != "Mano" && gameObject.tag != "Tirador")
        {
            if (other.tag == "Player")
            {
                wizard.aviableShoot = true;
                wizard.Attaking = false;
                //wizard.dilay = wizard.auxDilay;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
        if (GetComponent<SphereCollider>() != null && this.gameObject.tag == "GeneradorPelotaEnemigo" && gameObject.tag != "Mano" && gameObject.tag != "Tirador")
        {
            if (other.tag == "Player")
            {
                wizard.aviableShoot = false;
                wizard.Attaking = false;
                
            }
        }
        
    }

}
