using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelotaEnemigo : MonoBehaviour {

    // Use this for initialization
    public PoolPelota pool;
    private PoolObject poolObject;
    private bool tiempoAuxiliarHabilitado;
    private float auxTiempoVida;
    public float potencia;
    public float tiempoVida;
    public float danio;
    private Rigidbody rigBola;
    public GameObject generador;
    public bool pelotaTinta;
    public void Disparar() {
        //rigBola = GetComponent<Rigidbody>();
        //rigBola.AddRelativeForce(-generador.transform.forward * potencia, ForceMode.Impulse);
        if (!pelotaTinta)
        {
            rigBola = GetComponent<Rigidbody>();
            rigBola.velocity = Vector3.zero;
            rigBola.angularVelocity = Vector3.zero;
            rigBola.AddRelativeForce(-generador.transform.forward * potencia, ForceMode.Impulse);
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
        if(pelotaTinta)
        {
            rigBola = GetComponent<Rigidbody>();
            rigBola.velocity = Vector3.zero;
            rigBola.angularVelocity = Vector3.zero;
            rigBola.AddForce(-generador.transform.forward * potencia, ForceMode.Impulse);
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
    }
	
	// Update is called once per frame
	void Update () {
        tiempoVida = tiempoVida - Time.deltaTime;
        if(tiempoVida <= 0)
        {
            //Destroy(this.gameObject);
            if (poolObject != null)
            {
                poolObject.Resiclarme();
            }
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "GeneradorPelotaEnemigo" && other.tag != "Tirador" && other.tag != "Corredor" && other.tag != "PelotaDeTinta" && other.tag != "Kraken" && other.tag != "TraspasablePorPelotaTinta")
        {
            if (poolObject != null)
            {
                poolObject.Resiclarme();
            }
        }
    }
}
