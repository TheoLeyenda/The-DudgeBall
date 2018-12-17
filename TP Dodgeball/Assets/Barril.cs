using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class Barril : MonoBehaviour {

    // Use this for initialization
    public PuertaPuzle PuzleDoor;
    private bool once;
	void Start () {
        once = true;
	}
    private void OnDisable()
    {
        if (once)
        {
            PuzleDoor.AddBarrelDown();
            //unaVez = false;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MiniPelota")
        {
            gameObject.SetActive(false);
        }
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)