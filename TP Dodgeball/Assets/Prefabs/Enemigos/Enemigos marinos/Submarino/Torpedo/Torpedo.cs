using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
public class Torpedo : Enemy {

    // Use this for initialization
    public float speed;
    public float initialSpeed;
    public float damage;
    public Pool pool;
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
    public void CheckVolume()
    {
        if (Player.InstancePlayer != null)
        {
            Audio.volume = Player.InstancePlayer.effectsVolumeController.volume;
        }
    }
    void Update()
    {
        CheckVolume();
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
                if (Player.GetPlayer() != null)
                {
                    transform.LookAt(Player.GetPlayer().transform.position);
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
                if (Player.GetPlayer() != null)
                {
                    Player.GetPlayer().AddScore(250);
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
            if (Player.GetPlayer() != null)
            {
                life = life - (GetDamageCommonBall() + Player.GetPlayer().GetAdditionalDamageCommonBall());
                IsDead();
                if (Player.GetPlayer().GetDoblePoints())
                {
                    Player.GetPlayer().AddScore(1 * 2);
                }
                else
                {
                    Player.GetPlayer().AddScore(1);
                }
            }
        }
        if (other.gameObject.tag == "PelotaDeHielo")
        {
            if (Player.GetPlayer() != null)
            {
                if (Player.GetPlayer().GetDoblePoints())
                {
                    Player.GetPlayer().AddScore(1 * 2);
                }
                else
                {
                    Player.GetPlayer().AddScore(1);
                }
                life = life - (GetDamageIceBall() + Player.GetPlayer().GetAdditionalDamageIceBall());
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
            if (Player.GetPlayer() != null)
            {
                if (Player.GetPlayer().GetDoblePoints())
                {
                    Player.GetPlayer().AddScore(1 * 2);
                }
                else
                {
                    Player.GetPlayer().AddScore(1);
                }
                life = life - (GetDamageMiniBall() + Player.GetPlayer().GetAditionalDamageMiniBalls());
                IsDead();
            }
        }
        if (other.gameObject.tag == "PelotaDanzarina")
        {
            if (Player.GetPlayer() != null)
            {
                if (Player.GetPlayer().GetDoblePoints())
                {
                    Player.GetPlayer().AddScore(1 * 2);
                }
                else
                {
                    Player.GetPlayer().AddScore(1);
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
            if (Player.GetPlayer() != null)
            {
                if (Player.GetPlayer().GetDoblePoints())
                {
                    Player.GetPlayer().AddScore(1 * 2);
                }
                else
                {
                    Player.GetPlayer().AddScore(1);
                }
                life = life - (GetDamageExplociveBall() + Player.GetPlayer().GetAdditionalDamageExplociveBall());
            }
            IsDead();

        }
        if(other.gameObject.tag == "Player")
        {
            if(Player.GetPlayer() != null)
            {
                Player.InstancePlayer.DamageMeSound();
                if (Player.InstancePlayer.armor > 0)
                {
                    Player.InstancePlayer.armor = Player.InstancePlayer.armor - (damage/2);
                }
                else
                {
                    Player.GetPlayer().life = Player.GetPlayer().life - damage;
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
