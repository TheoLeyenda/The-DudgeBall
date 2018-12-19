using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObject : MonoBehaviour {

    // Use this for initialization
    public float rotation;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0,rotation, 0);
	}
}
