using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
public class ratacionObjeto : MonoBehaviour {

    // Use this for initialization
    public float rotation;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0,rotation, 0);
	}
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
