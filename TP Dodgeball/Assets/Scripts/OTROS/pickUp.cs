using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
public class pickUp : MonoBehaviour {

    // Use this for initialization
    public PoolPelota pool;
    private PoolObject poolObject;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void On()
    {
        poolObject = GetComponent<PoolObject>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            poolObject.Recycle();
        }
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
