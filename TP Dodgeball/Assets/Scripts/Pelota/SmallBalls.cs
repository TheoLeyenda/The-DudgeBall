using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBalls : MonoBehaviour {

    // Use this for initialization
    public FragmentBall Base;
    public float power;
    public float timeLife;
    public float auxTimeLife;
    public int typeRoute;
    public GameObject BaseBall;
    private float angle;
    private Rigidbody rigBall;
    private float rotateX;
    private float rotateY;
    private float rotateZ;
    private bool recycle;
    public AudioSource Audio;
    public AudioClip clip;

    void OnEnable()
    {
        //Audio = GetComponent<AudioSource>();
        //Audio.PlayOneShot(clip);
        recycle = false;
        rotateX = 0;
        rotateY = 4;
        rotateZ = 0;
        rigBall = GetComponent<Rigidbody>();
        rigBall.velocity = Vector3.zero;
        rigBall.angularVelocity = Vector3.zero;

        if (typeRoute == 1)
        {
            //rigBola.AddRelativeForce(transform.forward * potencia, ForceMode.Impulse);
            rigBall.AddForce(transform.right * power, ForceMode.Impulse);
        }
        if(typeRoute == 2)
        {
            transform.Rotate(rotateX, -rotateY, rotateZ);
            //rigBola.AddRelativeForce(transform.forward * potencia, ForceMode.Impulse);
            rigBall.AddForce(transform.right * power, ForceMode.Impulse);
        }
        if (typeRoute == 3)
        {
            transform.Rotate(rotateX, rotateY, rotateZ);
            // rigBola.AddRelativeForce(transform.forward * potencia, ForceMode.Impulse);
            rigBall.AddForce(transform.right * power, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {

        timeLife = timeLife - Time.deltaTime;

        if (timeLife <= 0)
        {
            //Destroy(this.gameObject);
            recycle = true;
            timeLife = auxTimeLife;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "PelotaFragmentadora" && other.gameObject.tag != "MiniPelota" && other.tag != "Player" && other.tag != "GeneradorPelotaEnemigo")
        {
            //Destroy(this.gameObject);
            recycle = true;
            timeLife = auxTimeLife;
        }
    }
    public bool GetRecycle()
    {
        return recycle;
    }
    public void SetRecycle(bool _recycle)
    {
        recycle = _recycle;
    }
    public void SetTimeLife(float time)
    {
        timeLife = time;
    }
    public float GetTimeLife()
    {
        return timeLife;
    }
}
