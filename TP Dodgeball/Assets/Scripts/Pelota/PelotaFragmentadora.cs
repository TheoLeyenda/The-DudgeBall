using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelotaFragmentadora : MonoBehaviour {

    // Use this for initialization
    private bool PelotaBaseDestruida;
    private bool tiempoAuxiliarHabilitado = false;
    public float potencia;
    public Camera camara;
    public float tiempoVida;
    private Rigidbody rigBola;
    public GameObject miniPelota1;
    public GameObject miniPelota2;
    public GameObject miniPelota3;
    public GameObject pelotaBase;
    private Vector3 auxPos;
    private bool resiclar;
    public void Disparar()
    {
        resiclar = false;
        PelotaBaseDestruida = false;
        rigBola = GetComponent<Rigidbody>();
        rigBola.velocity = Vector3.zero;
        rigBola.angularVelocity = Vector3.zero;
        rigBola.AddRelativeForce(camara.transform.forward * potencia, ForceMode.Impulse);
        miniPelota1.SetActive(false);
        miniPelota2.SetActive(false);
        miniPelota3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!tiempoAuxiliarHabilitado)
        {
            tiempoAuxiliarHabilitado = true;
        }
        tiempoVida = tiempoVida - Time.deltaTime;
        if (tiempoVida <= 0)
        {
            if (miniPelota1 != null && miniPelota2 != null && miniPelota3 != null)
            {

               
                //Instantiate(miniPelota1, transform.position,transform.rotation);
                miniPelota1.transform.position = gameObject.transform.position;
                miniPelota1.transform.rotation = gameObject.transform.rotation;
                miniPelota1.SetActive(true);
                //Instantiate(miniPelota2, transform.position, transform.rotation);
                miniPelota2.transform.position = gameObject.transform.position;
                miniPelota2.transform.rotation = gameObject.transform.rotation;
                miniPelota2.SetActive(true);
                //Instantiate(miniPelota3, transform.position,transform.rotation);
                miniPelota3.transform.position = gameObject.transform.position;
                miniPelota3.transform.rotation = gameObject.transform.rotation;
                miniPelota3.SetActive(true);
                //new Quaternion(transform.rotation.x, transform.rotation.y - 45, transform.rotation.z, transform.rotation.w)
            }
            //Destroy(this.gameObject);
            PelotaBaseDestruida = true;
            resiclar = true;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag != "MiniPelota")
        {
            if (miniPelota1 != null && miniPelota2 != null && miniPelota3 != null)
            {
               
                //Instantiate(miniPelota1, transform.position,transform.rotation);
                miniPelota1.transform.position = gameObject.transform.position;
                miniPelota1.transform.rotation = gameObject.transform.rotation;
                miniPelota1.SetActive(true);
                //Instantiate(miniPelota2, transform.position, transform.rotation);
                miniPelota2.transform.position = gameObject.transform.position;
                miniPelota2.transform.rotation = gameObject.transform.rotation;
                miniPelota2.SetActive(true);
                //Instantiate(miniPelota3, transform.position,transform.rotation);
                miniPelota3.transform.position = gameObject.transform.position;
                miniPelota3.transform.rotation = gameObject.transform.rotation;
                miniPelota3.SetActive(true);
                //new Quaternion(transform.rotation.x, transform.rotation.y - 45, transform.rotation.z, transform.rotation.w)
            }
            //Destroy(this.gameObject);
            PelotaBaseDestruida = true;
            resiclar = true;
        }
    }
    public bool GetPelotaBaseDestruida()
    {
        return PelotaBaseDestruida;
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
