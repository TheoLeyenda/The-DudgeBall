using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBall : MonoBehaviour {

    // Use this for initialization
    public Pool pool;
    private PoolObject poolObject;
    //auxiliaryTimeEnabled
    private bool auxiliaryTimeEnabled;
    private float auxTimeLife;
    public float power;
    public float timeLife;
    public float damage;
    private Rigidbody rigBall;
    public GameObject generator;
    public bool tinteBall;
    public bool arrow;
    private Player player;
    public AudioSource Audio;
    public AudioClip clipTinteBall;
    public AudioClip clipBullet;
    public AudioClip clipRugbyBall;
    public AudioClip clipArrow;
    private void Start()
    {
        if(Player.InstancePlayer != null)
        {
            player = Player.InstancePlayer;
        }
    }
    public void Shoot() {
        if (!tinteBall && !arrow)
        {
            if (Audio != null && clipRugbyBall != null)
            {
                Audio.clip = clipRugbyBall;
                Audio.Play();
            }
            rigBall = GetComponent<Rigidbody>();
            rigBall.velocity = Vector3.zero;
            rigBall.angularVelocity = Vector3.zero;
            rigBall.AddRelativeForce(-generator.transform.forward * power, ForceMode.Impulse);
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
        if(arrow)
        {
            if(Audio != null && clipArrow != null)
            {
                Audio.PlayOneShot(clipArrow);
            }
            rigBall = GetComponent<Rigidbody>();
            rigBall.velocity = Vector3.zero;
            rigBall.angularVelocity = Vector3.zero;
            
            rigBall.AddRelativeForce(generator.transform.up * power, ForceMode.Impulse);
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
        if(tinteBall)
        {
            if (Audio != null && clipTinteBall != null)
            {
                Audio.PlayOneShot(clipTinteBall);
            }
            rigBall = GetComponent<Rigidbody>();
            rigBall.velocity = Vector3.zero;
            rigBall.angularVelocity = Vector3.zero;
            rigBall.AddRelativeForce(-generator.transform.up * power, ForceMode.Impulse);
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
    }
	
	// Update is called once per frame
	void Update () {
        timeLife = timeLife - Time.deltaTime;
        if(timeLife <= 0)
        {
            //Destroy(this.gameObject);
            if (poolObject != null)
            {
                poolObject.Recycle();
            }
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "GeneradorPelotaEnemigo" && other.tag != "Tirador" && other.tag != "Corredor" && other.tag != "PelotaDeTinta" && other.tag != "Kraken" && other.tag != "TraspasablePorPelotaTinta")
        {
            if (poolObject != null)
            {
                poolObject.Recycle();
            }
        }
        if (player != null)
        {
            if (other.gameObject.tag == "Player")
            {
                if (!player.GetImmune())
                {
                    if (player.armor > 0)
                    {
                        player.armor = player.armor - damage;
                    }
                    else
                    {
                        player.life = player.life - damage;
                    }
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "GeneradorPelotaEnemigo" && collision.gameObject.tag != "Tirador" && collision.gameObject.tag != "Corredor" && collision.gameObject.tag != "PelotaDeTinta" && collision.gameObject.tag != "Kraken" && collision.gameObject.tag != "TraspasablePorPelotaTinta")
        {
            if (poolObject != null)
            {
                timeLife = 0.1f;
            }
        }
        if (player != null)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (!player.GetImmune())
                {
                    if (player.armor > 0)
                    {
                        player.armor = player.armor - damage;
                    }
                    else
                    {
                        player.life = player.life - damage;
                    }
                }
            }
        }
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)