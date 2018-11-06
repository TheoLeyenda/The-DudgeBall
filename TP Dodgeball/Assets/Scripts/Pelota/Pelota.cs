using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelota : MonoBehaviour {

    // Use this for initialization

    public PoolPelota pool;
    public float potencia;
    public Camera camara;
    public float tiempoVida;
    private float auxTiempoVida;
    private Rigidbody rigBola;
    private PoolObject poolObject;
    private bool tiempoAuxiliarHabilitado;

    public void Disparar() {
       
        rigBola = GetComponent<Rigidbody>();
        rigBola.velocity = Vector3.zero;
        rigBola.angularVelocity = Vector3.zero;
        rigBola.AddRelativeForce(camara.transform.forward  * potencia, ForceMode.Impulse);
        poolObject = GetComponent<PoolObject>();
        if (!tiempoAuxiliarHabilitado)
        {
            tiempoAuxiliarHabilitado = true;
            auxTiempoVida = tiempoVida;
        }
        if (tiempoVida <= 0)
        {
            tiempoVida = auxTiempoVida;
        }
      
    }
    // Update is called once per frame
    void Update () {
        tiempoVida = tiempoVida - Time.deltaTime;
        if(tiempoVida <= 0)
        {
            if (poolObject != null)
            {
                poolObject.Resiclarme();
            }
        }
	}
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag != "PelotaDeTinta")
        {
            tiempoVida = 0;
        }
    }
    public float GetTiempoVida()
    {
        return tiempoVida;
    }
}
