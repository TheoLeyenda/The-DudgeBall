using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorBloques : MonoBehaviour {

    public GameObject bloques;
    private float randomX;
    private float randomZ;
    private float randomInstanciar;
    public float ancho;
    public float alto;
    public float valorY;
    private PoolObject poolObject;
    public PoolPelota poolCubitos;
    // ESTO TEORICAMENTE FUNCIONARIA (PROBAR LUEGO)
    private void Start()
    {
        for (int i = 1; i < ancho; i++)
        {
            for (int j = 1; j < alto; j++)
            {
                if(i % 2 == 0 && j %2 != 0)
                {
                    randomInstanciar = Random.Range(1, 100);
                    if(randomInstanciar >= 60)
                    {
                        Instantiate(bloques, new Vector3(i,valorY,j), Quaternion.identity);
                    }
                }
                if(j % 2 == 0)
                {
                    randomInstanciar = Random.Range(1, 100);
                    if (randomInstanciar >= 60)
                    {
                        Instantiate(bloques, new Vector3(i,valorY,j), Quaternion.identity);
                    }
                }

            }
        }
    }
     //------------------------------------------
    
    void Update()
    {

    }
}



    // Update is called once per frame
   