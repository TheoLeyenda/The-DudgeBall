﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

    // Use this for initialization
    public Pool poolRock;
    private PoolObject poolObject;
    private Rigidbody rig;
    void Start () {
        poolObject = GetComponent<PoolObject>();
        rig = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "RompeObjetos")
        {
            rig.velocity = Vector3.zero;
            rig.angularVelocity = Vector3.zero;
            poolObject.Recycle();
        }
    }
}
