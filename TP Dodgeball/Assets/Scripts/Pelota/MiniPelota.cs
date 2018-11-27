using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPelota : MonoBehaviour {

    // Use this for initialization
    public PelotaFragmentadora Base;
    public float potencia;
    public float tiempoVida;
    public float auxTiempoVida;
    public int tipoRuta;
    public GameObject pelotaBase;
    private float angulo;
    private Rigidbody rigBola;
    private float rotarX;
    private float rotarY;
    private float rotarZ;
    private bool resiclar;
    public AudioSource Audio;
    public AudioClip clip;

    void OnEnable()
    {
        //Audio = GetComponent<AudioSource>();
        //Audio.PlayOneShot(clip);
        resiclar = false;
        rotarX = 0;
        rotarY = 4;
        rotarZ = 0;
        rigBola = GetComponent<Rigidbody>();
        rigBola.velocity = Vector3.zero;
        rigBola.angularVelocity = Vector3.zero;

        if (tipoRuta == 1)
        {
            //rigBola.AddRelativeForce(transform.forward * potencia, ForceMode.Impulse);
            rigBola.AddForce(transform.right * potencia, ForceMode.Impulse);
        }
        if(tipoRuta == 2)
        {
            transform.Rotate(rotarX, -rotarY, rotarZ);
            //rigBola.AddRelativeForce(transform.forward * potencia, ForceMode.Impulse);
            rigBola.AddForce(transform.right * potencia, ForceMode.Impulse);
        }
        if (tipoRuta == 3)
        {
            transform.Rotate(rotarX, rotarY, rotarZ);
            // rigBola.AddRelativeForce(transform.forward * potencia, ForceMode.Impulse);
            rigBola.AddForce(transform.right * potencia, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {

        tiempoVida = tiempoVida - Time.deltaTime;

        if (tiempoVida <= 0)
        {
            //Destroy(this.gameObject);
            resiclar = true;
            tiempoVida = auxTiempoVida;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "PelotaFragmentadora" && other.gameObject.tag != "MiniPelota")
        {
            //Destroy(this.gameObject);
            resiclar = true;
            tiempoVida = auxTiempoVida;
        }
    }
    public bool GetResiclar()
    {
        return resiclar;
    }
    public void SetResiclar(bool _resiclar)
    {
        resiclar = _resiclar;
    }
    public void SetTiempoVida(float tiempo)
    {
        tiempoVida = tiempo;
    }
    public float GetTiempoVida()
    {
        return tiempoVida;
    }
}
