using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour {

    // Use this for initialization
    public PoolPelota pool;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Resiclarme()
    {
        pool.Recycle(this.gameObject);
    }
}
