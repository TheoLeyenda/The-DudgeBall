using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class Pelota : MonoBehaviour {

    // Use this for initialization

    public PoolPelota pool;
    public float power;
    public Camera CAMERA;
    public float timeLife;
    private float auxTimeLife;
    private Rigidbody rigBola;
    private PoolObject poolObject;
    private bool auxiliaryTimeEnabled;

    public void Shoot() {
       
        rigBola = GetComponent<Rigidbody>();
        rigBola.velocity = Vector3.zero;
        rigBola.angularVelocity = Vector3.zero;
        rigBola.AddRelativeForce(CAMERA.transform.forward  * power, ForceMode.Impulse);
        poolObject = GetComponent<PoolObject>();
        if (!auxiliaryTimeEnabled)
        {
            auxiliaryTimeEnabled = true;
            auxTimeLife = timeLife;
        }
        if (timeLife <= 0)
        {
            timeLife = auxTimeLife;
        }
      
    }
    // Update is called once per frame
    void Update () {
        timeLife = timeLife - Time.deltaTime;
        if(timeLife <= 0)
        {
            if (poolObject != null)
            {
                poolObject.Recycle();
            }
        }
	}
    private void OnTriggerEnter(Collider other)
    {

        //if (other.tag != "PelotaDeTinta")
        //{
            timeLife = 0;
        //}
    }
    public float GetTimeLife()
    {
        return timeLife;
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)