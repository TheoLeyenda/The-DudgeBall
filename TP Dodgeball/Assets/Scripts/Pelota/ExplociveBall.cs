using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplociveBall : MonoBehaviour {

    // Use this for initialization
    public AudioSource sound;
    public AudioClip explotionSound;
    public Pool pool;
    private PoolObject poolObject;
    private bool auxiliaryTimeEnabled;
    public float power;
    public Camera CAMERA;
    public float lifeTime;
    private float auxlifeTime;
    private Rigidbody rigBola;
    //private float poder;
    public float radio;
    //public float upforce;
    public GameObject bomb;
    private bool destroy;
    private float count;
    public SphereCollider collisionSphere;
    public GameObject explotionEffect;
    private float countEffect;
    private bool tell;
    private void Start()
    {
        //SI ALGO SE ROMPE CON LA BOMBA DESCOMENTAR ESTA LINEA
        //SphereCollider esfera = EsferaColicionadora;
    }
    private void OnEnable()
    {
        
        auxlifeTime = lifeTime;
        
    }
    public void Shoot()
    {
        if (lifeTime <= 0)
        {
            lifeTime = auxlifeTime;
        }
        rigBola = GetComponent<Rigidbody>();
        rigBola.velocity = Vector3.zero;
        rigBola.angularVelocity = Vector3.zero;
        if (collisionSphere != null)
        {
            collisionSphere.radius = radio;
            collisionSphere.gameObject.SetActive(false);
        }
        rigBola.AddRelativeForce(CAMERA.transform.forward * power, ForceMode.Impulse);
        explotionEffect.SetActive(false);
        countEffect = 0;
        count = 0;
        destroy = false;
        tell = false;
        poolObject = GetComponent<PoolObject>();

    }
    // Update is called once per frame
    public void CheckVolume()
    {
        if (Player.InstancePlayer != null)
        {
            sound.volume = Player.InstancePlayer.effectsVolumeController.volume;
        }
    }
    void Update()
    {
        CheckVolume();
        lifeTime = lifeTime - Time.deltaTime;
        if (lifeTime <= 0)
        {
            if(sound != null && explotionSound != null)
            {
                sound.clip = explotionSound;
                sound.PlayOneShot(explotionSound);
            }
            Detonate();
            destroy = true;
            tell = true;
        }
        if(count>3 && destroy)
        {
            //Destroy(this.gameObject);
            if (collisionSphere != null)
            {
                collisionSphere.gameObject.SetActive(false);
            }
            poolObject.Recycle();
            lifeTime = auxlifeTime;
        }
        if(tell)
        {
            countEffect = countEffect + Time.deltaTime;
            count = count + Time.deltaTime;
        }
        if(countEffect >= 1)
        {
            explotionEffect.SetActive(false);
        }
    }
    void Detonate()
    {
        if (collisionSphere != null)
        {
            collisionSphere.gameObject.SetActive(true);
        }
        //Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "GeneradorPelotaEnemigo")
        {
            if (sound != null && explotionSound != null)
            {
                sound.clip = explotionSound;
                sound.PlayOneShot(explotionSound);
            }
            Detonate();
            explotionEffect.SetActive(true);
            destroy = true;
            tell = true;
        }
    }

}