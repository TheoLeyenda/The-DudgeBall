using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerRocas : MonoBehaviour {

    // Use this for initialization
    public PoolPelota poolRoca;
    private PoolObject poolObject;
    public float RangoX;
    public float RangoZ;
    public float dileyGeneracion;
    public float auxDileyGeneracion;
    void Start () {
        auxDileyGeneracion = dileyGeneracion;
	}
	
	// Update is called once per frame
	void Update () {
		if(dileyGeneracion <= 0)
        {
            GameObject go = poolRoca.GetObject();
            go.transform.position = new Vector3((int)Random.Range(-RangoX, RangoX)+ transform.position.x, transform.position.y, (int)Random.Range(-RangoZ, RangoZ)+transform.position.z);
            dileyGeneracion = auxDileyGeneracion;
        }
        dileyGeneracion = dileyGeneracion - Time.deltaTime;
	}
}
