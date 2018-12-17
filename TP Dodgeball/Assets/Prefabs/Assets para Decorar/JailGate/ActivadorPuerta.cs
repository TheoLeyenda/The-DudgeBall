using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
public class ActivadorPuerta : MonoBehaviour {

    // Use this for initialization
    public PuertaRejas Door;
    public bool close;
    public bool open;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (close)
            {
                if (Door != null)
                {
                    Door.SetCloseDoor(true);
                    Door.SetOpenDoor(false);
                }
            }
            if(open)
            {
                if(Door != null)
                {
                    Door.SetCloseDoor(false);
                    Door.SetOpenDoor(true);
                }
            }
        }
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
