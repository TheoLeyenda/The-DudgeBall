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
    int cont = 0;
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
    // Use this for initialization
    /*public GameObject bloqueDestructible;
    public GameObject puerta;
    private GameObject[] cajita;
    private float random;
    private float RandomPuerta;
    void Start() {
        for (int i = 0; i <= 14; i++)
        {
            for (int j = 0; j <= 16; j++)
            {
                random = Random.Range(0, 5);
                if (random == 0)
                {
                    if ((j % 2 == 0) || (j % 2 != 0) && (i % 2 == 0))
                    {
                        if (((i == 12 && j == 4) || (i == 2 && j == 4) || (i == 2 && j == 14) || (i == 12 && j == 14) || (i == 7 && j == 10) || (i == 7 && j == 8) || (i == 0 && j == 0)) == false)
                        {
                            Instantiate(bloqueDestructible, new Vector3(i * -1, 0, j), Quaternion.identity);
                        }
                    }
                }
            }
        }
        cajita = GameObject.FindGameObjectsWithTag("Destructible");//Cargo el array con todos los objetos instanciados 
                                                                   //que tengan el tag pasado por parametro
        RandomPuerta = Random.Range(0, cajita.Length);
        Instantiate(puerta, cajita[(int)RandomPuerta].transform.position, Quaternion.identity);*/
    void Update()
    {

    }
}



    // Update is called once per frame
   