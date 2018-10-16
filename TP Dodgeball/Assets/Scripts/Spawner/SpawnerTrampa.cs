using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTrampa : MonoBehaviour {

    // Use this for initialization
    public PoolPelota poolEnemigo;
    private PoolObject poolObject;
    public float RangoX;
    public float RangoZ;
    public int cantidad;
    void Start () {
        for (int i = 0; i < cantidad; i++)
        {
            Generar();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Generar()
    {
        GameObject go = poolEnemigo.GetObject();
        go.transform.position = new Vector3(Random.Range(-RangoX, RangoX), transform.position.y, Random.Range(-RangoZ, RangoZ));
    }
}
