using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorBlocks : MonoBehaviour {

    public GameObject blocks;
    private float randomX;
    private float randomZ;
    private float randomInstantiate;
    public float width;
    public float hight;
    public float valueY;
    private PoolObject poolObject;
    public Pool poolBlocks;
    private void Start()
    {
        for (int i = 1; i < width; i++)
        {
            for (int j = 1; j < hight; j++)
            {
                if(i % 2 == 0 && j %2 != 0)
                {
                    randomInstantiate = Random.Range(1, 100);
                    if(randomInstantiate >= 60)
                    {
                        Instantiate(blocks, new Vector3(i,valueY,j), Quaternion.identity);
                    }
                }
                if(j % 2 == 0)
                {
                    randomInstantiate = Random.Range(1, 100);
                    if (randomInstantiate >= 60)
                    {
                        Instantiate(blocks, new Vector3(i,valueY,j), Quaternion.identity);
                    }
                }

            }
        }
    }
     //------------------------------------------
}