using UnityEngine;

public class Shooter : Enemy {

    // Use this for initialization
    [HideInInspector]
    public Player player;
    public float auxLife;
    public Pool poolShooter;
    public Pool rugbyBalls;
    private PoolObject poolObject;
    [HideInInspector]
    public float auxLifeTime;
    public float speed;
    public float dilay;
    [HideInInspector]
    public float auxDilay;
    [HideInInspector]
    public bool aviableShoot;
    public GameObject ball;
    public GameObject generatorBall;
    public GameObject shooter;
    [HideInInspector]
    public float timeState;
    [HideInInspector]
    public float auxSpeed;
    [HideInInspector]
    public float effectFire;
    private Rigidbody rig;
    private float dileyInsta;
    public float rangeDouble;
    public float enemyVisionRange;
    public float damage;
    public float powerShoot;
    public AudioSource Audio;
    public AudioClip clip;
    public Animator animator;

    public Pool poolPoderInmune;
    public Pool poolDoblePuntuacion;
    public Pool poolInstaKill;
    public int patternType;

   

    void Start () {
        aviableShoot = false;
        if(Player.InstancePlayer != null)
        {
            player = Player.InstancePlayer;
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
        animator.SetBool("Idle", true);
        animator.SetBool("Run", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Death_A", false);
        animator.SetBool("Death_B", false);
        animator.SetBool("Damage", false);
    }
    public void On()
    {
        if (Player.InstancePlayer != null)
        {
            player = Player.InstancePlayer;
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
        animator.SetBool("Idle", true);
        animator.SetBool("Run", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Death_A", false);
        animator.SetBool("Death_B", false);
        animator.SetBool("Damage", false);
    }
    // Update is called once per frame
    public void CheckVolume()
    {
        if (Player.InstancePlayer != null)
        {
            Audio.volume = Player.InstancePlayer.effectsVolumeController.volume;
        }
    }
    void Update () {
        CheckVolume();
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
        if(aviableShoot)
        {
            if (dilay <= 0)
            {
                dilay = auxDilay;
                ThrowBall();
            }
            if (dilay > 0)
            {
                dilay = dilay - Time.deltaTime;
            }
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
                    go.transform.position =  new Vector3 (transform.position.x,transform.position.y+0.8f,transform.position.z);
                    go.transform.rotation = transform.rotation;
                }
            }
            if (auxiliar > 12 && auxiliar <= 25)
            {

                GameObject go = poolDoblePuntuacion.GetObject();
                if (go != null)
                {
                    poolDoblePuntuacion.SubstractId();
                    go.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
                    go.transform.rotation = transform.rotation;
                }
            }
            if (auxiliar > 30 && auxiliar <= 35)
            {
                GameObject go = poolInstaKill.GetObject();
                if (go != null)
                {
                    poolInstaKill.SubstractId();
                    go.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
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
        //Debug.Log(aviableShoot);
        if (!aviableShoot)
        {
            if (patternType == 0)
            {
                if (player != null)
                {
                    rig.velocity = Vector3.zero;
                    rig.angularVelocity = Vector3.zero;
                    transform.LookAt(new Vector3(Player.GetPlayer().transform.position.x, transform.position.y, Player.GetPlayer().transform.position.z));
                    transform.position += transform.forward * Time.deltaTime * speed; //si comento esto es una torreta y sino es un jugador de rugby
                    animator.SetBool("Idle", false);
                    animator.SetBool("Run", true);
                    animator.SetBool("Attack", false);
                    animator.SetBool("Death_A", false);
                    animator.SetBool("Death_B", false);
                    animator.SetBool("Damage", false);

                }
            }
            if (patternType == 1)
            {
                rig.velocity = Vector3.zero;
                rig.angularVelocity = Vector3.zero;
                transform.position += transform.forward * Time.deltaTime * speed;
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, rangeDouble))
                {
                    if (hit.collider.gameObject.tag != "PoderInmune" && hit.collider.gameObject.tag != "DoblePuntuacion" && hit.collider.gameObject.tag != "InstaKill" && hit.collider.gameObject.tag != "Player" && hit.collider.gameObject.tag != "PelotaComun" && hit.collider.gameObject.tag != "MiniPelota" && hit.collider.gameObject.tag != "PelotaDeHielo" && hit.collider.gameObject.tag != "PelotaDeFuego" && hit.collider.gameObject.tag != "PelotaDanzarina" && hit.collider.gameObject.tag != "SpawnerEnemigo")
                    {
                        float opcion;
                        
                        opcion = Random.Range(0, 2);
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

                animator.SetBool("Idle", false);
                animator.SetBool("Run", true);
                animator.SetBool("Attack", false);
                animator.SetBool("Death_A", false);
                animator.SetBool("Death_B", false);
                animator.SetBool("Damage", false);
            }
        }
        else if(aviableShoot)
        {
            if (patternType == 0)
            {
                rig.velocity = Vector3.zero;
                rig.angularVelocity = Vector3.zero;
                transform.LookAt(new Vector3(Player.GetPlayer().transform.position.x, transform.position.y, Player.GetPlayer().transform.position.z));

                animator.SetBool("Run", false);
                animator.SetBool("Idle", true);
                animator.SetBool("Attack", false);
                animator.SetBool("Death_A", false);
                animator.SetBool("Death_B", false);
                animator.SetBool("Damage", false);
            }
            else
            {
                rig.velocity = Vector3.zero;
                rig.angularVelocity = Vector3.zero;
                animator.SetBool("Run", false);
                animator.SetBool("Idle", true);
                animator.SetBool("Attack", false);
                animator.SetBool("Death_A", false);
                animator.SetBool("Death_B", false);
                animator.SetBool("Damage", false);
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
        EnemyBall Ball = go.GetComponent<EnemyBall>();
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
        animator.Play("UD_archer_07_attack_A");
        Ball.Shoot();
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
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PelotaComun" && gameObject.tag == "Tirador")
        {
            //Debug.Log("ENTRE");
            if (player != null)
            {
                life = life - (GetDamageCommonBall() + player.GetAdditionalDamageCommonBall());
                //Debug.Log("Hize Daño");
                IsDead();
                if (player.GetDoblePoints())
                {
                    player.AddScore(10 * 2);
                }
                else
                {
                    player.AddScore(10);
                }
            }
        }
        if (other.gameObject.tag == "PelotaDeHielo" && gameObject.tag == "Tirador")
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
        if (other.gameObject.tag == "MiniPelota" && gameObject.tag == "Tirador")
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
        if (other.gameObject.tag == "PelotaDanzarina" && gameObject.tag == "Tirador")
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
                player.AddScore(5 * 2);
            }
            else
            {
                player.AddScore(5);
            }
        }
        if (other.gameObject.tag == "PelotaDeFuego" && gameObject.tag == "Tirador")
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
        if (other.gameObject.tag == "PelotaExplociva" && gameObject.tag == "Tirador")
        {

            if (player != null)
            {
                if (player.GetDoblePoints())
                {
                    player.AddScore(20 * 2);
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
