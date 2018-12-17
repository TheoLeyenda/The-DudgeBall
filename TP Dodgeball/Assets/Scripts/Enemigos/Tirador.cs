using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
public class Tirador : Enemigo {

    // Use this for initialization
    private Jugador player;
    public float auxLife;
    public PoolPelota poolShooter;
    public PoolPelota rugbyBalls;
    private PoolObject poolObject;
    private float auxLifeTime;
    public float speed;
    public float dilay;
    private float auxDilay;
    public GameObject ball;
    public GameObject generatorBall;
    public GameObject shooter;
    private float timeState;
    private float auxSpeed;
    private float effectFire;
    private Rigidbody rig;
    private float dileyInsta;
    public float rangeDouble;
    public float enemyVisionRange;
    public float damage;
    public float powerShoot;
    public AudioSource Audio;
    public AudioClip clip;

    public PoolPelota poolPoderInmune;
    public PoolPelota poolDoblePuntuacion;
    public PoolPelota poolInstaKill;

    public int patternType;

    void Start () {
        if(Jugador.InstancePlayer != null)
        {
            player = Jugador.InstancePlayer;
        }
        dileyInsta = 1;
        SetEnemyState(EstadoEnemigo.normal);
        rig = GetComponent<Rigidbody>();
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;
        auxDilay = dilay;
        auxSpeed = speed;
        timeState = 0;
        effectFire = 0;
        effectFrozen.SetActive(false);
        effectMusic.SetActive(false);
        effectBurned.SetActive(false);
    }
    public void Prendido()
    {
        if (Jugador.InstancePlayer != null)
        {
            player = Jugador.InstancePlayer;
        }
        dileyInsta = 1;
        SetEnemyState(EstadoEnemigo.normal);
        life = auxLife;
        rig = GetComponent<Rigidbody>();
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;
        auxSpeed = speed;
        effectFire = 0;
        effectFrozen.SetActive(false);
        effectBurned.SetActive(false);
        effectMusic.SetActive(false);
        poolObject = GetComponent<PoolObject>();
    }
    // Update is called once per frame
    void Update () {
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
        //EstaMuerto();
        UpdateHP();
        if (GetEnemyState() != EstadoEnemigo.frozen && GetEnemyState() != EstadoEnemigo.dance)
        {
            Movement();
        }
        if(dilay <= 0)
        {
            dilay = auxDilay;
            ThrowBall();
        }
        if (dilay > 0)
        {
            dilay = dilay - Time.deltaTime;
        }
        if(GetDead())
        {
            // Seguir configurando la probabilidad de aparicion de los powers ups
            float auxiliar = Random.Range(1, 100);
            if (auxiliar > 90 && auxiliar <= 94)
            {
                GameObject go = poolPoderInmune.GetObject();
                if (go != null)
                {
                    poolPoderInmune.SubstractId();
                    go.transform.position = transform.position;
                    go.transform.rotation = transform.rotation;
                }
            }
            if (auxiliar > 12 && auxiliar <= 25)
            {

                GameObject go = poolDoblePuntuacion.GetObject();
                if (go != null)
                {
                    poolDoblePuntuacion.SubstractId();
                    go.transform.position = transform.position;
                    go.transform.rotation = transform.rotation;
                }
            }
            if (auxiliar > 30 && auxiliar <= 35)
            {
                GameObject go = poolInstaKill.GetObject();
                if (go != null)
                {
                    poolInstaKill.SubstractId();
                    go.transform.position = transform.position;
                    go.transform.rotation = transform.rotation;
                }
            }
            player.AddScore(60);
            GameManager.GetGameManager().AddDeath();
            if (GameManager.GetGameManager() != null && i_AmInPool)
            {
                GameManager.GetGameManager().SubstractEnemyAmountOnScreen();
            }
            SetDead(false);
            if (!i_AmInPool)
            {
                gameObject.SetActive(false);
            }
            if (i_AmInPool)
            {
                if (poolObject != null)
                {
                    poolObject.Recycle();
                }
            }
        }
        if(timeState > 0)
        {
            timeState = timeState - Time.deltaTime;
            if (GetEnemyState() == EstadoEnemigo.frozen)
            {
                dilay = 1000000000;
            }
            if (GetEnemyState() == EstadoEnemigo.dance)
            {
                SetRotateY(20);
                Rotate();
            }
            if(timeState <= 0 && GetEnemyState() == EstadoEnemigo.dance)
            {
                SetEnemyState(EstadoEnemigo.normal);
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
        }
        if(timeState <= 0 && GetEnemyState() == EstadoEnemigo.frozen)
        {
            speed = auxSpeed;
            dilay = auxDilay;
            SetEnemyState(EstadoEnemigo.normal);
        }
        if(timeState <= 0 && GetEnemyState() == EstadoEnemigo.Burned)
        {
            SetEnemyState(EstadoEnemigo.normal);
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
    public void Movement()
    {
        if (patternType == 0)
        {
            if (player != null)
            {
                rig.velocity = Vector3.zero;
                rig.angularVelocity = Vector3.zero;
                transform.LookAt(new Vector3(Jugador.GetPlayer().transform.position.x, transform.position.y, Jugador.GetPlayer().transform.position.z));
                transform.position += transform.forward * Time.deltaTime * speed; //si comento esto es una torreta y sino es un jugador de rugby
            }
        }
        if(patternType == 1)
        {
            rig.velocity = Vector3.zero;
            rig.angularVelocity = Vector3.zero;
            transform.position += transform.forward * Time.deltaTime * speed;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, rangeDouble))
            {
                if (hit.collider.gameObject.tag != "PoderInmune" && hit.collider.gameObject.tag != "DoblePuntuacion" && hit.collider.gameObject.tag != "InstaKill" && hit.collider.gameObject.tag != "Player" && hit.collider.gameObject.tag != "PelotaComun" && hit.collider.gameObject.tag != "MiniPelota" && hit.collider.gameObject.tag != "PelotaDeHielo" && hit.collider.gameObject.tag != "PelotaDeFuego" && hit.collider.gameObject.tag != "PelotaDanzarina" && hit.collider.gameObject.tag != "SpawnerEnemigo")
                {
                    float opcion = Random.Range(0, 2);
                    if (opcion >= 1)
                    {
                        transform.Rotate(0, 90, 0);
                    }
                    else
                    {
                        transform.Rotate(0, -90, 0);
                    }
                }
            }
        }
    }
    public void ThrowBall()
    {
        //Instantiate(Bola,generadorPelota.transform.position ,generadorPelota.transform.rotation);
        if(Audio != null && clip != null)
        {
            Audio.PlayOneShot(clip);
        }
        GameObject go = rugbyBalls.GetObject();
        PelotaEnemigo Ball = go.GetComponent<PelotaEnemigo>();
        go.transform.position = generatorBall.transform.position;
        go.transform.rotation = generatorBall.transform.rotation;
        if (damage > 0)
        {
            Ball.damage = damage;
        }
        if (powerShoot > 0)
        {
            Ball.power = powerShoot;
        }
        Ball.Shoot();
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
                    player.AddScore(10* 2);
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
                    player.AddScore(10*2);
                }
                else
                {
                    player.AddScore(10);
                }
                life = life - (GetDamageIceBall() + player.GetAdditionalDamageIceBall());
            }
            IsDead();
            if (speed > 0)
            {
                //velocidad = velocidad - 0.2f;
                speed = 0;
            }
            if (speed <= 0)
            {

                if (GetEnemyState() != EstadoEnemigo.frozen)
                {
                    timeState = 5;//tiempo por el cual el enemigo "Corredor" estara congelado
                }
                SetEnemyState(EstadoEnemigo.frozen);
                effectFrozen.SetActive(true);
            }
        }
        if (other.gameObject.tag == "MiniPelota")
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
                life = life - (GetDamageMiniBall() + player.GetAditionalDamageMiniBalls());
                IsDead();
            }
        }
        if (other.gameObject.tag == "PelotaDanzarina")
        {
           
            if (GetEnemyState() != EstadoEnemigo.dance)
            {
                timeState = 7;//tiempo por el cual el enemigo estara bailando
            }
            SetEnemyState(EstadoEnemigo.dance);
            effectMusic.SetActive(true);
            life = life - GetDamageDanceBall();
            IsDead();
            if (player.GetDoblePoints())
            {
                player.AddScore(5*2);
            }
            else
            {
                player.AddScore(5);
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
            speed = auxSpeed;
            dilay = auxDilay;
        }
        if(other.gameObject.tag == "PelotaExplociva")
        {
            
            if (player != null)
            {
                if (player.GetDoblePoints())
                {
                    player.AddScore(20*2);
                }
                else
                {
                    player.AddScore(10);
                }
                life = life - (GetDamageExplociveBall() + player.GetAdditionalDamageExplociveBall());
                IsDead();
            }

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Corredor" || collision.gameObject.tag == "Tirador" && patternType == 1)
        {
            collision.gameObject.transform.Rotate(0, 180, 0);
        }
        if (collision.gameObject.tag == "Pared")
        {
            float opcion = Random.Range(0, 2);
            if (opcion >= 1)
            {
                transform.Rotate(0, 90, 0);
            }
            else
            {
                transform.Rotate(0, -90, 0);
            }
        }
    }
    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }
    public void SetAuxSpeed(float _auxSpeed)
    {
        auxSpeed = _auxSpeed;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public float GetAuxSpeed()
    {
        return auxSpeed;
    }
    public void AddSpeed()
    {
        speed = speed + 0.02f;
        auxSpeed = speed;
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
