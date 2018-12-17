using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
public class Torpedo : Enemigo {

    // Use this for initialization
    public float speed;
    public float initialSpeed;
    public float damage;
    public PoolPelota pool;
    public float dileyForward;
    public GameObject bubbles;

    private float auxInitaialSpeed;
    private float auxSpeed;
    private float timeState;
    private float effectFire;
    private float auxDileyForward;
    private float auxLife;
    private Rigidbody rig;
    public AudioSource Audio;
    public AudioClip clipTorpedo;

    private PoolObject poolObject;
    void Start() {
        poolObject = GetComponent<PoolObject>();
        rig = GetComponent<Rigidbody>();
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;
        auxDileyForward = dileyForward;
        life = maxLife;
        auxInitaialSpeed = initialSpeed;
        auxSpeed = speed;
    }

    public void On()
    {
        Audio.PlayOneShot(clipTorpedo);
        SetEnemyState(EstadoEnemigo.normal);
        poolObject = GetComponent<PoolObject>();
        rig = GetComponent<Rigidbody>();
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;
        auxDileyForward = dileyForward;
        life = maxLife;
        timeState = 0;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        CheckDead();
        if (timeState > 0)
        {
            if (GetEnemyState() == EstadoEnemigo.dance)
            {
                SetRotateY(90);
                Rotate();
            }
            if (GetEnemyState() == EstadoEnemigo.frozen)
            {
                speed = 0;
                initialSpeed = 0;
            }
            timeState = timeState - Time.deltaTime;
        }
        if (timeState <= 0)
        {
            if (GetEnemyState() == EstadoEnemigo.frozen)
            {
                speed = auxSpeed;
                initialSpeed = auxInitaialSpeed;
                if (effectFrozen != null && effectMusic != null)
                {
                    effectFrozen.SetActive(false);
                    effectMusic.SetActive(false);
                }
                SetEnemyState(EstadoEnemigo.normal);

            }
            if (GetEnemyState() == EstadoEnemigo.dance)
            {
                if (effectFrozen != null && effectMusic != null)
                {
                    effectMusic.SetActive(false);
                    effectFrozen.SetActive(false);
                }
                SetEnemyState(EstadoEnemigo.normal);
            }
            if (GetEnemyState() == EstadoEnemigo.Burned)
            {
                if ( effectFrozen != null && effectMusic != null)
                {
                    effectMusic.SetActive(false);
                    effectFrozen.SetActive(false);
                }
                SetEnemyState(EstadoEnemigo.normal);
            }
        }
    }
    public void Move()
    {
        if (GetEnemyState() != EstadoEnemigo.frozen && GetEnemyState() != EstadoEnemigo.dance)
        {
            if (dileyForward > 0)
            {
                transform.position = transform.position + transform.forward * Time.deltaTime * initialSpeed;
                bubbles.SetActive(false);
            }
            if (dileyForward < 0)
            {
                bubbles.SetActive(true);
                if (Jugador.GetPlayer() != null)
                {
                    transform.LookAt(Jugador.GetPlayer().transform.position);
                    transform.position = transform.position + transform.forward * Time.deltaTime * speed;
                }
            }
            dileyForward = dileyForward - Time.deltaTime;
        }
    }
    public void CheckDead()
    {
        if (life <= 0)
        {
            if (GetDead())
            {
                if (Jugador.GetPlayer() != null)
                {
                    Jugador.GetPlayer().AddScore(250);
                }
                if (!i_AmInPool)
                {
                    gameObject.SetActive(false);
                }
                if (i_AmInPool)
                {
                    poolObject.Recycle();
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PelotaComun")
        {
            if (Jugador.GetPlayer() != null)
            {
                life = life - (GetDamageCommonBall() + Jugador.GetPlayer().GetAdditionalDamageCommonBall());
                IsDead();
                if (Jugador.GetPlayer().GetDoblePoints())
                {
                    Jugador.GetPlayer().AddScore(1 * 2);
                }
                else
                {
                    Jugador.GetPlayer().AddScore(1);
                }
            }
        }
        if (other.gameObject.tag == "PelotaDeHielo")
        {
            if (Jugador.GetPlayer() != null)
            {
                if (Jugador.GetPlayer().GetDoblePoints())
                {
                    Jugador.GetPlayer().AddScore(1 * 2);
                }
                else
                {
                    Jugador.GetPlayer().AddScore(1);
                }
                life = life - (GetDamageIceBall() + Jugador.GetPlayer().GetAdditionalDamageIceBall());
            }
            IsDead();
            if (speed > 0 || initialSpeed > 0)
            {
                speed = speed - 2f;
                initialSpeed = initialSpeed - 4f;
                //velMovimiento = 0;
            }
            if (speed <= 0 || initialSpeed <= 0)
            {
                SetEnemyState(EstadoEnemigo.frozen);
                effectFrozen.SetActive(true);
                timeState = 2.5f;//tiempo por el cual el enemigo "Corredor" estara congelado
            }
        }
        if (other.gameObject.tag == "MiniPelota")
        {
            if (Jugador.GetPlayer() != null)
            {
                if (Jugador.GetPlayer().GetDoblePoints())
                {
                    Jugador.GetPlayer().AddScore(1 * 2);
                }
                else
                {
                    Jugador.GetPlayer().AddScore(1);
                }
                life = life - (GetDamageMiniBall() + Jugador.GetPlayer().GetAditionalDamageMiniBalls());
                IsDead();
            }
        }
        if (other.gameObject.tag == "PelotaDanzarina")
        {
            if (Jugador.GetPlayer() != null)
            {
                if (Jugador.GetPlayer().GetDoblePoints())
                {
                    Jugador.GetPlayer().AddScore(1 * 2);
                }
                else
                {
                    Jugador.GetPlayer().AddScore(1);
                }
            }
            if (GetEnemyState() != EstadoEnemigo.dance)
            {
                timeState = 1.5f;//tiempo por el cual el enemigo estara bailando
            }
            SetEnemyState(EstadoEnemigo.dance);
            effectMusic.SetActive(true);
            life = life - GetDamageDanceBall();
            IsDead();

        }
        if (other.gameObject.tag == "PelotaExplociva")
        {
            if (Jugador.GetPlayer() != null)
            {
                if (Jugador.GetPlayer().GetDoblePoints())
                {
                    Jugador.GetPlayer().AddScore(1 * 2);
                }
                else
                {
                    Jugador.GetPlayer().AddScore(1);
                }
                life = life - (GetDamageExplociveBall() + Jugador.GetPlayer().GetAdditionalDamageExplociveBall());
            }
            IsDead();

        }
        if(other.gameObject.tag == "Player")
        {
            if(Jugador.GetPlayer() != null)
            {
                if (Jugador.InstancePlayer.armor > 0)
                {
                    Jugador.InstancePlayer.armor = Jugador.InstancePlayer.armor - damage;
                }
                else
                {
                    Jugador.GetPlayer().life = Jugador.GetPlayer().life - damage;
                }
                if (i_AmInPool)
                {
                    poolObject.Recycle();
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
