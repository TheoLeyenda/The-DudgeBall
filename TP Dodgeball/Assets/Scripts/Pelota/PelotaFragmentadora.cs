using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class PelotaFragmentadora : MonoBehaviour {

    // Use this for initialization
    private bool baseBallDestroyed;
    private bool auxiliaryTimeEnabled = false;
    public float power;
    public Camera CAMERA;
    public float lifeTime;
    private Rigidbody rigBall;
    public GameObject miniBall1;
    public GameObject miniBall2;
    public GameObject miniBall3;
    public GameObject baseBall;
    private Vector3 auxPos;
    private bool recycle;
    public void Shoot()
    {
        recycle = false;
        baseBallDestroyed = false;
        rigBall = GetComponent<Rigidbody>();
        rigBall.velocity = Vector3.zero;
        rigBall.angularVelocity = Vector3.zero;
        rigBall.AddRelativeForce(CAMERA.transform.forward * power, ForceMode.Impulse);
        miniBall1.SetActive(false);
        miniBall2.SetActive(false);
        miniBall3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!auxiliaryTimeEnabled)
        {
            auxiliaryTimeEnabled = true;
        }
        lifeTime = lifeTime - Time.deltaTime;
        if (lifeTime <= 0)
        {
            if (miniBall1 != null && miniBall2 != null && miniBall3 != null)
            {

               
                //Instantiate(miniPelota1, transform.position,transform.rotation);
                miniBall1.transform.position = gameObject.transform.position;
                miniBall1.transform.rotation = gameObject.transform.rotation;
                miniBall1.SetActive(true);
                //Instantiate(miniPelota2, transform.position, transform.rotation);
                miniBall2.transform.position = gameObject.transform.position;
                miniBall2.transform.rotation = gameObject.transform.rotation;
                miniBall2.SetActive(true);
                //Instantiate(miniPelota3, transform.position,transform.rotation);
                miniBall3.transform.position = gameObject.transform.position;
                miniBall3.transform.rotation = gameObject.transform.rotation;
                miniBall3.SetActive(true);
                //new Quaternion(transform.rotation.x, transform.rotation.y - 45, transform.rotation.z, transform.rotation.w)
            }
            //Destroy(this.gameObject);
            baseBallDestroyed = true;
            recycle = true;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag != "MiniPelota")
        {
            if (miniBall1 != null && miniBall2 != null && miniBall3 != null)
            {
               
                //Instantiate(miniPelota1, transform.position,transform.rotation);
                miniBall1.transform.position = gameObject.transform.position;
                miniBall1.transform.rotation = gameObject.transform.rotation;
                miniBall1.SetActive(true);
                //Instantiate(miniPelota2, transform.position, transform.rotation);
                miniBall2.transform.position = gameObject.transform.position;
                miniBall2.transform.rotation = gameObject.transform.rotation;
                miniBall2.SetActive(true);
                //Instantiate(miniPelota3, transform.position,transform.rotation);
                miniBall3.transform.position = gameObject.transform.position;
                miniBall3.transform.rotation = gameObject.transform.rotation;
                miniBall3.SetActive(true);
                //new Quaternion(transform.rotation.x, transform.rotation.y - 45, transform.rotation.z, transform.rotation.w)
            }
            //Destroy(this.gameObject);
            baseBallDestroyed = true;
            recycle = true;
        }
    }
    public bool GetBaseBallDestroyed()
    {
        return baseBallDestroyed;
    }
    public bool GetRecycle()
    {
        return recycle;
    }
    public void SetRecycle(bool _recycle)
    {
        recycle = _recycle;
    }
    public void SetLifeTime(float time)
    {
        lifeTime = time;
    }
    public float GetLifeTime()
    {
        return lifeTime;
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
