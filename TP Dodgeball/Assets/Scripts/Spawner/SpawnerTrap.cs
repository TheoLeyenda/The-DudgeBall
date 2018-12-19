using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTrap : MonoBehaviour {

    // Use this for initialization
    public Pool poolEnemy;
    private PoolObject poolObject;
    public float RangeX;
    public float RangeZ;
    public int count;
    public bool generateOnStart;
    void Start () {
        
        if (generateOnStart)
        {
            for (int i = 0; i < count; i++)
            {
                Generate();
            }
        }
	}

    // Update is called once per frame
    void Update () {
		
	}
    public void Generate()
    {
        GameObject go = poolEnemy.GetObject();
        float x = Random.Range(-RangeX, RangeX);
        float z = Random.Range(-RangeZ, RangeZ);
        go.transform.position = new Vector3(transform.position.x+x, transform.position.y, transform.position.z+z);
    }
}
