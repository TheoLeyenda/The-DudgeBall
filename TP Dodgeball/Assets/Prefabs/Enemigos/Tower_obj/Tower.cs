using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Enemy {

    // Use this for initialization
    public Pool arrows;
    private PoolObject poolObject;
    public float dilayShoot;
    private float auxDilayShoot;
    private float timeState;
    private float effectFire;
    public float damage;
    public float powerArrow;
    public AudioSource Audio;
    public AudioClip clip;
    public GameObject generatorBall;
    public float range;
    private bool shooting;
    public SphereCollider sphere;
    public GameObject tower;

    private Rigidbody rig;
    void Start () {
        range = 10;
        auxDilayShoot = dilayShoot;
        timeState = 0;
        SetEnemyState(EstadoEnemigo.normal);
        effectFire = 0;
        effectFrozen.SetActive(false);
        rig = GetComponent<Rigidbody>();
        shooting = false;
        if (sphere != null)
        {
            //sphere.radius = range;
            if (range <= 1)
            {
                //sphere.enabled = false;
            }
        }
    }

    // Update is called once per frame
    public void CheckVolume()
    {
        if (Player.InstancePlayer != null)
        {
            Audio.volume = Player.InstancePlayer.effectsVolumeController.volume;
        }
    }
    void Update() {
        CheckVolume();
        UpdateHP();
        if(life <= 0)
        {
            tower.SetActive(false);
        }
        if (GetDead())
        {
            if (!i_AmInPool)
            {
                gameObject.SetActive(false);
            }
        }
        CheckStateTower();
        if (shooting)
        {
            if (GetEnemyState() != EstadoEnemigo.frozen && GetEnemyState() != EstadoEnemigo.dance)
            {
                CheckShoot();
            }
        }
        
    }
    public void SetShooting(bool _shooting)
    {
        shooting = _shooting;
    }
    public bool GetShooting()
    {
        return shooting;
    }
    public void CheckShoot()
    {
        if (dilayShoot > 0)
        {
            dilayShoot = dilayShoot - Time.deltaTime;
        }
        if(dilayShoot <= 0)
        {
            dilayShoot = auxDilayShoot;
            ThrowArrow();
        }
    }
    public void ThrowArrow()
    {
        //Instantiate(Bola,generadorPelota.transform.position ,generadorPelota.transform.rotation);
        if (Audio != null && clip != null)
        {
            Audio.PlayOneShot(clip);
        }
        GameObject go = arrows.GetObject();
        EnemyBall ball = go.GetComponent<EnemyBall>();
        go.transform.position = generatorBall.transform.position;
        go.transform.rotation = generatorBall.transform.rotation;
        if (damage > 0)
        {
            ball.damage = damage;
        }
        if(powerArrow > 0)
        {
            ball.power = powerArrow;
        }
        ball.Shoot();
    }
    public void CheckStateTower()
    {
        if (timeState > 0)
        {
            timeState = timeState - Time.deltaTime;
            if (GetEnemyState() == EstadoEnemigo.frozen)
            {
                dilayShoot = 1000000000;
            }
            if (GetEnemyState() == EstadoEnemigo.dance)
            {
                SetRotateY(20);
                Rotate();
            }
            if (timeState <= 0 && GetEnemyState() == EstadoEnemigo.dance)
            {
                SetEnemyState(EstadoEnemigo.normal);
            }
            if (GetEnemyState() == EstadoEnemigo.Burned || effectBurned.activeSelf)
            {
                effectFire = effectFire + Time.deltaTime;
                if (effectFire >= 1)
                {
                    if (Player.GetPlayer() != null)
                    {
                        if (Player.GetPlayer().GetDoblePoints())
                        {
                            Player.GetPlayer().AddScore(5 * 2);
                        }
                        else
                        {
                            Player.GetPlayer().AddScore(5);
                        }
                        life = life - (GetDamageFireBall() + Player.GetPlayer().GetAdditionalDamageFireBall());
                        IsDead();
                    }
                    effectFire = 0;
                }
            }
        }
        if (timeState <= 0 && GetEnemyState() == EstadoEnemigo.frozen)
        {

            dilayShoot = auxDilayShoot;
            SetEnemyState(EstadoEnemigo.normal);
        }
        if (timeState <= 0 && GetEnemyState() == EstadoEnemigo.Burned)
        {
            SetEnemyState(EstadoEnemigo.normal);
        }
        if (GetEnemyState() != EstadoEnemigo.Burned && GetEnemyState() != EstadoEnemigo.dance && effectBurned != null)
        {
            effectBurned.SetActive(false);
        }
        if (GetEnemyState() != EstadoEnemigo.frozen && effectFrozen != null)
        {
            effectFrozen.SetActive(false);
        }
        if (GetEnemyState() != EstadoEnemigo.dance && effectMusic != null)
        {
            effectMusic.SetActive(false);
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
                    Player.GetPlayer().AddScore(10 * 2);
                }
                else
                {
                    Player.GetPlayer().AddScore(10);
                }
            }
        }
        if (other.gameObject.tag == "PelotaDeHielo")
        {
            if (Player.GetPlayer() != null)
            {
                if (Player.GetPlayer().GetDoblePoints())
                {
                    Player.GetPlayer().AddScore(10 * 2);
                }
                else
                {
                    Player.GetPlayer().AddScore(10);
                }
                life = life - (GetDamageIceBall() + Player.GetPlayer().GetAdditionalDamageIceBall());
            }
            IsDead();
            if (GetEnemyState() != EstadoEnemigo.frozen)
            {
                timeState = 5;//tiempo por el cual el enemigo "Corredor" estara congelado
            }
            SetEnemyState(EstadoEnemigo.frozen);
            effectFrozen.SetActive(true);
            
        }
        if (other.gameObject.tag == "MiniPelota")
        {
            if (Player.GetPlayer() != null)
            {
                if (Player.GetPlayer().GetDoblePoints())
                {
                    Player.GetPlayer().AddScore(10 * 2);
                }
                else
                {
                    Player.GetPlayer().AddScore(10);
                }
                life = life - (GetDamageMiniBall() + Player.GetPlayer().GetAditionalDamageMiniBalls());
                IsDead();
            }
        }
        
        if (other.gameObject.tag == "PelotaDeFuego")
        {

            if (GetEnemyState() != EstadoEnemigo.Burned)
            {
                timeState = 7;
            }
            if (GetEnemyState() != EstadoEnemigo.dance)
            {
                SetEnemyState(EstadoEnemigo.Burned);
            }
            effectBurned.SetActive(true);
            dilayShoot = auxDilayShoot;
        }
        if (other.gameObject.tag == "PelotaExplociva")
        {
            IsDead();
            if (Player.GetPlayer() != null)
            {
                if (Player.GetPlayer().GetDoblePoints())
                {
                    Player.GetPlayer().AddScore(20 * 2);
                }
                else
                {
                    Player.GetPlayer().AddScore(10);
                }
                life = life - (GetDamageExplociveBall() + Player.GetPlayer().GetAdditionalDamageExplociveBall());
            }

        }
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)