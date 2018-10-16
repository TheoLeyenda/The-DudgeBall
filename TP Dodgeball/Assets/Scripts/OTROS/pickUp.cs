using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUp : MonoBehaviour {

    // Use this for initialization
    public PoolPelota pool;
    private PoolObject poolObject;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Prendido()
    {
        poolObject = GetComponent<PoolObject>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            poolObject.Resiclarme();
        }
    }
}
