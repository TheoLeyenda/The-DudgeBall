using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticShooter : Enemy
{

    // Use this for initialization
    private Player player;
    public float auxLife;
    public Pool rugbyBalls;
    private PoolObject poolObject;
    private float auxLifeTime;
    public float dilay;
    private float auxDilay;
    public GameObject Ball;
    public GameObject generatorBalls;
    public GameObject shooter;
    private float timeState;
    private float effectFire;
    private Rigidbody rig;
    private float dileyInsta;
    public int movementType;
    public float damage;
    public AudioSource Audio;
    public AudioClip clip;

    void Start()
    {
        if(Player.InstancePlayer != null)
        {
            player = Player.InstancePlayer;
        }
        dileyInsta = 1;
        auxLife = life;
        auxDilay = dilay;
        timeState = 0;
        SetEnemyState(EstadoEnemigo.normal);
        effectFire = 0;
        effectFrozen.SetActive(false);
        effectMusic.SetActive(false);
        rig = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        life = maxLife;
        SetDead(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (player.GetInstaKill())
            {
                life = 1;
            }
            if (!player.GetInstaKill() && player.GetActiveInstaKill())
            {
                life = auxLife;
                if (dileyInsta > 0)
                {
                    dileyInsta = dileyInsta - Time.deltaTime;
                }
                if (dileyInsta <= 0)
                {
                    player.SetActiveInstaKill(false);
                    dileyInsta = 1;
                }
            }
        }
        UpdateHP();
        rig.Sleep();
        //EstaMuerto();
        if (GetEnemyState() != EstadoEnemigo.frozen && GetEnemyState() != EstadoEnemigo.dance)
        {
            Movimiento();
        }
        if (dilay <= 0)
        {
            dilay = auxDilay;
            ThrowBall();
        }
        if (dilay > 0)
        {
            dilay = dilay - Time.deltaTime;
        }
        if (GetDead())
        {
            if (!i_AmInPool)
            {
                gameObject.SetActive(false);
            }
        }
        if (timeState > 0)
        {
            timeState = timeState - Time.deltaTime;

            if (GetEnemyState() == EstadoEnemigo.frozen)
            {
                dilay = 200000;
            }
            if (GetEnemyState() == EstadoEnemigo.dance)
            {
                SetRotateY(20);
                Rotate();
            }
            if (GetEnemyState() == EstadoEnemigo.Burned || effectBurned.activeSelf)
            {
                effectFire = effectFire + Time.deltaTime;
                if (effectFire >= 1)
                {
                    if (player != null)
                    {
                        if (player.GetDoblePoints())
                        {
                            player.AddScore(5*2);
                        }
                        else
                        {
                            player.AddScore(5);
                        }
                        life = life - (GetDamageFireBall() + player.GetAdditionalDamageFireBall());
                        IsDead();
                    }
                    effectFire = 0;
                }
            }
            if (timeState <= 0)
            {
                dilay = auxDilay;
                SetEnemyState(EstadoEnemigo.normal);
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                
            }
        }
        if (GetEnemyState() != EstadoEnemigo.Burned && GetEnemyState() != EstadoEnemigo.dance)
        {
            effectBurned.SetActive(false);
        }
        if (GetEnemyState() != EstadoEnemigo.frozen)
        {
            effectFrozen.SetActive(false);
        }
        if (GetEnemyState() != EstadoEnemigo.dance)
        {
            effectMusic.SetActive(false);
        }

    }
    public void Movimiento()
    {
        if (player != null)
        {
            if (movementType == 0)
            {
                transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            }
            if(movementType == 1)
            {
                transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));
            }
        }
    }
    public void ThrowBall()
    {
        if (Audio != null && clip != null)
        {
            //Audio.volume = 0;
            Audio.PlayOneShot(clip);
        }
        GameObject go = rugbyBalls.GetObject();
        EnemyBall ball = go.GetComponent<EnemyBall>();
        go.transform.position = generatorBalls.transform.position;
        go.transform.rotation = generatorBalls.transform.rotation;
        ball.Shoot();
        if (damage > 0)
        {
            ball.damage = damage;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PelotaComun")
        {
            if (player != null)
            {
                life = life - (GetDamageCommonBall() + player.GetAdditionalDamageCommonBall());
                IsDead();
                if (player.GetDoblePoints())
                {
                    player.AddScore(10*2);
                }
                else
                {
                    player.AddScore(10);
                }
            }
        }
        if (other.gameObject.tag == "PelotaDeHielo")
        {
            if (player != null)
            {
                if (player.GetDoblePoints())
                {
                    player.AddScore(10 * 2);
                }
                else
                {
                    player.AddScore(10);
                }
                life = life - (GetDamageIceBall() + player.GetAdditionalDamageIceBall());
            }
            IsDead();
            if (GetEnemyState() != EstadoEnemigo.frozen)
            {
                timeState = 5;//tiempo por el cual el enemigo "Corredor" estara congelado
            }
            effectFrozen.SetActive(true);
            SetEnemyState(EstadoEnemigo.frozen);
        }
        if(other.gameObject.tag == "MiniPelota")
        {
            if (player != null)
            {
                if (player.GetDoblePoints())
                {
                    player.AddScore(10*2);
                }
                else
                {
                    player.AddScore(10);
                }
                life = life - (GetDamageMiniBall() + player.GetAditionalDamageMiniBalls());
                IsDead();
            }
        }
        if (other.gameObject.tag == "PelotaDanzarina")
        {
            if (player.GetDoblePoints())
            {
                player.AddScore(5*2);
            }
            else
            {
                player.AddScore(5);
            }
            if (GetEnemyState() != EstadoEnemigo.dance)
            {
                timeState = 7;//tiempo por el cual el enemigo estara bailando
            }
            SetEnemyState(EstadoEnemigo.dance);
            life = life - GetDamageDanceBall();
            IsDead();
            effectMusic.SetActive(true);
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
            dilay = auxDilay;
        }
        if (other.gameObject.tag == "PelotaExplociva")
        {
            IsDead();
            if (player != null)
            {
                if (player.GetDoblePoints())
                {
                    player.AddScore(20*2);
                }
                else
                {
                    Player.GetPlayer().AddScore(20);
                }
                life = life - (GetDamageExplociveBall() + player.GetAdditionalDamageExplociveBall());
            }
        }
    }
}
