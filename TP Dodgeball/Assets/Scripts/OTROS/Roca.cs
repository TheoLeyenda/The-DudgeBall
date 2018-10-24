using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roca : MonoBehaviour {

    // Use this for initialization
    public PoolPelota poolRoca;
    private PoolObject poolObject;
    void Start () {
        poolObject = GetComponent<PoolObject>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "RompeObjetos")
        {
            poolObject.Resiclarme();
        }
    }
}
