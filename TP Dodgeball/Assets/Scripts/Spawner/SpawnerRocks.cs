using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerRocks : MonoBehaviour {

    // Use this for initialization
    public Pool poolRock;
    private PoolObject poolObject;
    public float RangeX;
    public float RangeZ;
    public float dileyGeneration;
    public float auxDileyGeneration;
    void Start () {
        auxDileyGeneration = dileyGeneration;
	}
	
	// Update is called once per frame
	void Update () {
		if(dileyGeneration <= 0)
        {
            GameObject go = poolRock.GetObject();
            go.transform.position = new Vector3((int)Random.Range(-RangeX, RangeX)+ transform.position.x, transform.position.y, (int)Random.Range(-RangeZ, RangeZ)+transform.position.z);
            dileyGeneration = auxDileyGeneration;
        }
        dileyGeneration = dileyGeneration - Time.deltaTime;
	}
}