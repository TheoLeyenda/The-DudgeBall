using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelotaExpliciva : MonoBehaviour {

    // Use this for initialization
    public AudioSource sonido;
    public AudioClip sonidoExplocion;
    public PoolPelota pool;
    private PoolObject poolObject;
    private bool tiempoAuxiliarHabilitado;
    public float potencia;
    public Camera camara;
    public float tiempoVida;
    private float auxTiempoVida;
    private Rigidbody rigBola;
    //private float poder;
    public float radio;
    //public float upforce;
    public GameObject bomba;
    private bool destruir;
    private float contador;
    public SphereCollider EsferaColicionadora;
    public GameObject efectoExplocion;
    private float contadorEfecto;
    private bool contar;
    private void Start()
    {
        //SI ALGO SE ROMPE CON LA BOMBA DESCOMENTAR ESTA LINEA
        //SphereCollider esfera = EsferaColicionadora;
    }
    private void OnEnable()
    {
        
        auxTiempoVida = tiempoVida;
        
    }
    public void disparar()
    {
        if (tiempoVida <= 0)
        {
            tiempoVida = auxTiempoVida;
        }
        rigBola = GetComponent<Rigidbody>();
        rigBola.velocity = Vector3.zero;
        rigBola.angularVelocity = Vector3.zero;
        if (EsferaColicionadora != null)
        {
            EsferaColicionadora.radius = radio;
            EsferaColicionadora.gameObject.SetActive(false);
        }
        rigBola.AddRelativeForce(camara.transform.forward * potencia, ForceMode.Impulse);
        efectoExplocion.SetActive(false);
        contadorEfecto = 0;
        contador = 0;
        destruir = false;
        contar = false;
        poolObject = GetComponent<PoolObject>();

    }
    // Update is called once per frame
    void Update()
    {
        tiempoVida = tiempoVida - Time.deltaTime;
        if (tiempoVida <= 0)
        {
            if(sonido != null && sonidoExplocion != null)
            {
                sonido.clip = sonidoExplocion;
                sonido.PlayOneShot(sonidoExplocion);
            }
            Detonar();
            destruir = true;
            contar = true;
        }
        if(contador>3 && destruir)
        {
            //Destroy(this.gameObject);
            if (EsferaColicionadora != null)
            {
                EsferaColicionadora.gameObject.SetActive(false);
            }
            poolObject.Resiclarme();
            tiempoVida = auxTiempoVida;
        }
        if(contar)
        {
            contadorEfecto = contadorEfecto + Time.deltaTime;
            contador = contador + Time.deltaTime;
        }
        if(contadorEfecto >= 1)
        {
            efectoExplocion.SetActive(false);
        }
    }
    void Detonar()
    {
        if (EsferaColicionadora != null)
        {
            EsferaColicionadora.gameObject.SetActive(true);
        }
        //Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (sonido != null && sonidoExplocion != null)
        {
            sonido.clip = sonidoExplocion;
            sonido.PlayOneShot(sonidoExplocion);
        }
        Detonar();
        efectoExplocion.SetActive(true);
        destruir = true;
        contar = true;
    }

}
