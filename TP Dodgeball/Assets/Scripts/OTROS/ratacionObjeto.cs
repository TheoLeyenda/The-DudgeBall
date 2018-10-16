using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ratacionObjeto : MonoBehaviour {

    // Use this for initialization
    public float rotacion;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0,rotacion, 0);
	}
}
