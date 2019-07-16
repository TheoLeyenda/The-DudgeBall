using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Enemy {

    // Use this for initialization
    public bool randomInvoke;
    public bool formationInvoke;
    public int countInvoke;
    public bool WizardElemental;
    public bool WizardInvocador;
    [HideInInspector]
    public Player player;
    public float auxLife;
    public Pool poolWizard;
    public Pool poolImpactSpell;
    public Pool poolFrozenSpell;
    public Pool poolFireSpell;
    private PoolObject poolObject;
    [HideInInspector]
    public float auxLifeTime;
    public float speed;
    public float dilay;
    [HideInInspector]
    public float auxDilay;
    [HideInInspector]
    public bool aviableShoot;
    public GameObject ImpactSpell;//objeto que lanzara el mago cuando castee el hechizo de impacto.
    public GameObject FrozenSpell;//objeto que lanzara el mago cuando castee el hechizo de congelamiento.
    public GameObject FireSpell; //objeto que lanzara el mago cuando castee el hechizo de fuego.
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
    public float damageFireSpell;//daño hechizo de fuego.
    public float damageFrozenSpell;//daño hechizo de hielo.
    public float damageImpactSpell;//daño hechizo de impacto.
    public float powerShoot;
    public AudioSource Audio;
    public AudioClip[] clipImpactSpell;
    public AudioClip[] clipFrozenSpell;
    public AudioClip[] clipFireSpell;
    public Animator animator;
    private float timerDamage;
    private float auxTimerDamage;
    private bool enableTimerDamage;
    private bool enableMovement;
    private float timerDeath;
    private float auxTimerDeath;
    private bool enablePowerUp;

    public Pool poolPoderInmune;
    public Pool poolDoblePuntuacion;
    public Pool poolInstaKill;
    public int patternType;
    private bool enableShoot;
    public float damageEffectBurned;
    private float auxDamageEffectBurned;
    public float timeEffect;
    private float auxTimeEffect;

    public Pool poolEsqueletosArqueros;
    private int contEsqueletosArqueros;

    public Pool poolEsqueletosAtaqueFrontal;
    private int contEsqueletosAtaqueFrontal;

    public Pool poolEsqueletosAtaqueHorizontal;
    private int contEsqueletosAtaqueHorizontal;

    public Pool poolEsqueletosRapidosFrontal;
    private int contEsqueletosRapidosFrontal;

    public Pool poolEsqueletosRapidosHorizontal;
    private int contEsqueletosRapidosHorizontal;

    private bool oneOnce;
    void Start()
    {
        if (WizardInvocador)
        {
            oneOnce = true;
        }
        else {
            oneOnce = false;
        }
        contEsqueletosArqueros = 0;
        contEsqueletosAtaqueFrontal = 0;
        contEsqueletosAtaqueHorizontal = 0;
        contEsqueletosRapidosFrontal = 0;
        contEsqueletosRapidosHorizontal = 0;
        auxDamageEffectBurned = damageEffectBurned;
        auxTimeEffect = timeEffect;
        aviableShoot = false;
        enableShoot = true;
        if (Player.InstancePlayer != null)
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
        /*animator.SetBool("Idle", true);
        animator.SetBool("Run", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Death_A", false);
        animator.SetBool("Death_B", false);
        animator.SetBool("Damage", false);*/
        timerDamage = 0.4f;
        enableTimerDamage = false;
        enableMovement = true;
        auxTimerDamage = timerDamage;
        timerDeath = 3f;
        auxTimerDeath = timerDeath;
        enablePowerUp = true;

        enableShoot = true;
        GetComponent<CapsuleCollider>().enabled = true;
        enableMovement = true;
        lifeBar.SetActive(true);
        framework.SetActive(true);
        //FALTA AGREGAR EL TEMA DE ANIMACIONES

    }
    public void On()
    {
        if (WizardInvocador)
        {
            oneOnce = true;
        }
        else
        {
            oneOnce = false;
        }
        contEsqueletosArqueros = 0;
        contEsqueletosAtaqueFrontal = 0;
        contEsqueletosAtaqueHorizontal = 0;
        contEsqueletosRapidosFrontal = 0;
        contEsqueletosRapidosHorizontal = 0;
        damageEffectBurned = 0.5f;
        auxDamageEffectBurned = damageEffectBurned;
        auxTimeEffect = timeEffect;
        aviableShoot = false;
        enableShoot = true;
        if (Player.InstancePlayer != null)
        {
            player = Player.InstancePlayer;
        }

        enableShoot = true;
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
        /*animator.SetBool("Idle", true);
        animator.SetBool("Run", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Death_A", false);
        animator.SetBool("Death_B", false);
        animator.SetBool("Damage", false);*/
        timerDamage = 0.4f;
        enableTimerDamage = false;
        enableMovement = true;
        timerDeath = 3f;
        auxTimerDeath = timerDeath;
        auxTimerDamage = timerDamage;
        enablePowerUp = true;

        enableShoot = true;
        GetComponent<CapsuleCollider>().enabled = true;
        enableMovement = true;
        lifeBar.SetActive(true);
        framework.SetActive(true);
        //FALTA AGREGAR EL TEMA DE ANIMACIONES


    }
    // Update is called once per frame
    public void CheckVolume()
    {
        if (Player.InstancePlayer != null)
        {
            Audio.volume = Player.InstancePlayer.effectsVolumeController.volume;
        }
    }
    private void CheckMagicalEffect() {
        if (player != null) {
            if (player.effectBurned.activeSelf)
            {
                timeEffect = timeEffect - Time.deltaTime;

                damageEffectBurned = damageEffectBurned - Time.deltaTime;
                if (damageEffectBurned <= 0)
                {
                    if (player.armor > 0)
                    {
                        player.armor = player.armor - damageFireSpell;
                    }
                    else
                    {
                        player.life = player.life - damageFireSpell;
                    }
                    damageEffectBurned = auxDamageEffectBurned;
                }
            }
            else if (player.effectFrozen.activeSelf) {
                timeEffect = timeEffect - Time.deltaTime;
                player.fpc.update = false;

            }
            if (timeEffect <= 0) {
                damageEffectBurned = auxDamageEffectBurned;
                player.effectFrozen.SetActive(false);
                player.effectBurned.SetActive(false);
                player.fpc.update = true;
                timeEffect = auxTimeEffect;
            }
        }
    }
    void Update()
    {
        if (WizardElemental)
        {
            CheckMagicalEffect();
            //ANIMACION DE DAMAGE DE ENEMIGO
            if (timerDamage > 0 && enableTimerDamage)
            {
                //animator.SetBool("Run", false);
                timerDamage = timerDamage - Time.deltaTime;
            }
            if (timerDamage <= 0)
            {
                timerDamage = auxTimerDamage;
                enableTimerDamage = false;
                enableMovement = true;

            }
            //--------------------------------
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
                if (enableMovement && speed > 0 || aviableShoot)
                {
                    if (GetDead() == false)
                    {
                        Movement();
                        transform.LookAt(new Vector3(Player.GetPlayer().transform.position.x, transform.position.y, Player.GetPlayer().transform.position.z));
                    }
                }
                else
                {
                    animator.SetBool("Idle", true);
                }
            }
            if (aviableShoot && enableShoot)
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

            if (GetDead())
            {
                timeEffect = 0;
                enableShoot = false;
                GetComponent<CapsuleCollider>().enabled = false;
                enableMovement = false;
                lifeBar.SetActive(false);
                framework.SetActive(false);
                /*animator.SetBool("Run", false);
                animator.SetBool("Damage", false);
                animator.SetBool("Attack", false);
                animator.SetBool("Idle", false);
                animator.SetBool("Death_B", false);
                animator.SetBool("Death_A", false);*/


                if (enablePowerUp)
                {
                    //Animacion de muerte opcion 1
                    //animator.Play("UD_archer_10_death_B");

                    //Animacion de muerte opcion 2
                    //animator.Play("UD_archer_10_death_A");

                    enablePowerUp = false;
                    // Seguir configurando la probabilidad de aparicion de los powers ups
                    float auxiliar = Random.Range(1, 100);
                    if (auxiliar > 90 && auxiliar <= 94)
                    {
                        GameObject go = poolPoderInmune.GetObject();
                        if (go != null)
                        {
                            poolPoderInmune.SubstractId();
                            go.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
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
                }
                if (timerDeath > 0)
                {
                    timerDeath = timerDeath - Time.deltaTime;
                }
                if (timerDeath <= 0)
                {

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

            }
            if (timeState > 0)
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
                if (timeState <= 0 && GetEnemyState() == EstadoEnemigo.dance)
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
                                player.AddScore(5 * 2);
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
            if (timeState <= 0 && GetEnemyState() == EstadoEnemigo.frozen)
            {
                speed = auxSpeed;
                dilay = auxDilay;
                SetEnemyState(EstadoEnemigo.normal);
            }
            if (timeState <= 0 && GetEnemyState() == EstadoEnemigo.Burned)
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
        else if (WizardInvocador)
        {
            //ANIMACION DE DAMAGE DE ENEMIGO
            if (timerDamage > 0 && enableTimerDamage)
            {
                //animator.SetBool("Run", false);
                timerDamage = timerDamage - Time.deltaTime;
            }
            if (timerDamage <= 0)
            {
                timerDamage = auxTimerDamage;
                enableTimerDamage = false;
                enableMovement = true;

            }
            //--------------------------------
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
                if (enableMovement && speed > 0 || aviableShoot)
                {
                    Movement();
                }
                else
                {
                    animator.SetBool("Idle", true);
                }
            }
            if (((aviableShoot && enableShoot) || (oneOnce && aviableShoot)) && (GetDead() == false))
            {
                if (oneOnce) {
                    dilay = 0;
                }
                if (dilay <= 0)
                {
                    if (contEsqueletosArqueros < poolEsqueletosArqueros.count 
                        && contEsqueletosAtaqueFrontal < poolEsqueletosAtaqueFrontal.count 
                        && contEsqueletosAtaqueHorizontal < poolEsqueletosAtaqueHorizontal.count
                        && contEsqueletosRapidosFrontal < poolEsqueletosRapidosFrontal.count
                        && contEsqueletosRapidosHorizontal < poolEsqueletosRapidosHorizontal.count)
                    {
                        InvokeEnemys();//Aca ponemos que invoque mayonesos(Esqueletos)
                    }
                    dilay = auxDilay;
                    oneOnce = false;
                }
                if (dilay > 0)
                {
                    dilay = dilay - Time.deltaTime;
                }
            }

            if (GetDead())
            {
                enableShoot = false;
                GetComponent<CapsuleCollider>().enabled = false;
                enableMovement = false;
                lifeBar.SetActive(false);
                framework.SetActive(false);
                /*animator.SetBool("Run", false);
                animator.SetBool("Damage", false);
                animator.SetBool("Attack", false);
                animator.SetBool("Idle", false);
                animator.SetBool("Death_B", false);
                animator.SetBool("Death_A", false);*/


                if (enablePowerUp)
                {
                    //Animacion de muerte opcion 1
                    //animator.Play("UD_archer_10_death_B");

                    //Animacion de muerte opcion 2
                    //animator.Play("UD_archer_10_death_A");

                    enablePowerUp = false;
                    // Seguir configurando la probabilidad de aparicion de los powers ups
                    float auxiliar = Random.Range(1, 100);
                    if (auxiliar > 90 && auxiliar <= 94)
                    {
                        GameObject go = poolPoderInmune.GetObject();
                        if (go != null)
                        {
                            poolPoderInmune.SubstractId();
                            go.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
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
                }
                if (timerDeath > 0)
                {
                    timerDeath = timerDeath - Time.deltaTime;
                }
                if (timerDeath <= 0)
                {

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

            }
            if (timeState > 0)
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
                if (timeState <= 0 && GetEnemyState() == EstadoEnemigo.dance)
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
                                player.AddScore(5 * 2);
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
            if (timeState <= 0 && GetEnemyState() == EstadoEnemigo.frozen)
            {
                speed = auxSpeed;
                dilay = auxDilay;
                SetEnemyState(EstadoEnemigo.normal);
            }
            if (timeState <= 0 && GetEnemyState() == EstadoEnemigo.Burned)
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
                    /*animator.SetBool("Idle", false);
                    animator.SetBool("Run", true);
                    animator.SetBool("Attack", false);
                    animator.SetBool("Death_A", false);
                    animator.SetBool("Death_B", false);
                    animator.SetBool("Damage", false);*/

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

                /*animator.SetBool("Idle", false);
                animator.SetBool("Run", true);
                animator.SetBool("Attack", false);
                animator.SetBool("Death_A", false);
                animator.SetBool("Death_B", false);
                animator.SetBool("Damage", false);*/
            }
        }
        else if (aviableShoot)
        {
            if (patternType == 0)
            {
                rig.velocity = Vector3.zero;
                rig.angularVelocity = Vector3.zero;
                transform.LookAt(new Vector3(Player.GetPlayer().transform.position.x, transform.position.y, Player.GetPlayer().transform.position.z));

                /*animator.SetBool("Run", false);
                animator.SetBool("Idle", true);
                animator.SetBool("Attack", false);
                animator.SetBool("Death_A", false);
                animator.SetBool("Death_B", false);
                animator.SetBool("Damage", false);*/
            }
            else
            {
                rig.velocity = Vector3.zero;
                rig.angularVelocity = Vector3.zero;
                /*animator.SetBool("Run", false);
                animator.SetBool("Idle", true);
                animator.SetBool("Attack", false);
                animator.SetBool("Death_A", false);
                animator.SetBool("Death_B", false);
                animator.SetBool("Damage", false);*/
            }

        }
    }
    public void InvokeEnemys()
    {
        //Basandome en la funcion ThrowBall desarrollar esta funcion que invoca enemigos.
        //Solo falta completar esto la funcion ya esta llamada.
        if (randomInvoke)
        {

            float x = 5;
            float z = 5;
            int randomEnemy;
            GameObject go;
            
            for (int i = 0; i < countInvoke; i++)
            {
                
                randomEnemy = Random.Range(0, 5);
                x = Random.Range(-x, x);
                z = Random.Range(-z,z);
                if (x < 1 && x > 0)
                {
                    x = 1;
                }
                if (z < 1 && z > 0)
                {
                    z = 1;
                }
                if (x < 0 && x > -1)
                {
                    x = -1;
                }
                if (z < 0 && z > -1)
                {
                    z = -1;
                }
                switch (randomEnemy) {
                    case 0:
                        contEsqueletosArqueros++;
                        go = poolEsqueletosArqueros.GetObject();
                        Shooter shooter = go.GetComponent<Shooter>();
                        if (x < 0)
                        {
                            go.transform.position = transform.position + new Vector3(x - 1, 0, -6.5f);
                        }
                        else {
                            go.transform.position = transform.position + new Vector3(x + 1, 0, -6.5f);
                        }
                        go.transform.rotation = transform.rotation;
                        shooter.On();
                        break;
                    case 1:
                        contEsqueletosAtaqueFrontal++;
                        go = poolEsqueletosAtaqueFrontal.GetObject();
                        Runner runner1 = go.GetComponent<Runner>();
                        go.transform.position = transform.position + new Vector3(x, 1.5f, z);
                        go.transform.rotation = transform.rotation;
                        runner1.On();
                        break;
                    case 2:
                        contEsqueletosAtaqueHorizontal++;
                        go = poolEsqueletosAtaqueHorizontal.GetObject();
                        Runner runner2 = go.GetComponent<Runner>();
                        go.transform.position = transform.position + new Vector3(x, 1.5f, z);
                        go.transform.rotation = transform.rotation;
                        runner2.On();
                        break;
                    case 3:
                        contEsqueletosRapidosFrontal++;
                        go = poolEsqueletosRapidosFrontal.GetObject();
                        Runner runner3 = go.GetComponent<Runner>();
                        go.transform.position = transform.position + new Vector3(x, 1.55f, z);
                        go.transform.rotation = transform.rotation;
                        runner3.On();
                        break;
                    case 4:

                        contEsqueletosRapidosHorizontal++;
                        go = poolEsqueletosRapidosHorizontal.GetObject();
                        Runner runner4 = go.GetComponent<Runner>();
                        go.transform.position = transform.position + new Vector3(x, 1.55f, z);
                        go.transform.rotation = transform.rotation;
                        runner4.On();
                        break;
                }
                
            }
        }
        else if(formationInvoke){

        }
        

    }
    public void ThrowBall()
    {
        GameObject go;
        EnemyBall Spell;
       
        int randomAttack = Random.Range(0,3);
        switch (randomAttack)
        {
            case 0:
                go = poolImpactSpell.GetObject();
                Spell = go.GetComponent<EnemyBall>();
                go.transform.position = generatorBall.transform.position;
                go.transform.rotation = generatorBall.transform.rotation;
                int randomClipImpactSpell = Random.Range(0, clipImpactSpell.Length);
                if (damageImpactSpell > 0)
                {
                    Spell.damage = damageImpactSpell;
                }
                if (powerShoot > 0)
                {
                    Spell.power = powerShoot;
                }
                //animator.Play("UD_archer_07_attack_A");
                //animator.Play("WK_archer_07_attack_A");
                if (Audio != null && clipImpactSpell[randomClipImpactSpell] != null)
                {
                    Audio.PlayOneShot(clipImpactSpell[randomClipImpactSpell]);
                }
                Spell.Shoot();
                break;

            case 1:
                go = poolFrozenSpell.GetObject();
                Spell = go.GetComponent<EnemyBall>();
                go.transform.position = generatorBall.transform.position;
                go.transform.rotation = generatorBall.transform.rotation;
                int randomClipFrozenSpell = Random.Range(0, clipFrozenSpell.Length);
                if (damageFrozenSpell > 0)
                {
                    Spell.damage = damageFrozenSpell;
                }
                if (powerShoot > 0)
                {
                    Spell.power = powerShoot;
                }
                //animator.Play("UD_archer_07_attack_A");
                //animator.Play("WK_archer_07_attack_A");
                if (Audio != null && clipFrozenSpell[randomClipFrozenSpell] != null)
                {
                    Audio.PlayOneShot(clipFrozenSpell[randomClipFrozenSpell]);
                }
                Spell.Shoot();
                break;

            case 2:
                go = poolFireSpell.GetObject();
                Spell = go.GetComponent<EnemyBall>();
                go.transform.position = generatorBall.transform.position;
                go.transform.rotation = generatorBall.transform.rotation;
                int randomClipFireSpell = Random.Range(0, clipFireSpell.Length);
                if (damageFireSpell > 0)
                {
                    Spell.damage = damageFireSpell;
                }
                if (powerShoot > 0)
                {
                    Spell.power = powerShoot;
                }
                //animator.Play("UD_archer_07_attack_A");
                //animator.Play("WK_archer_07_attack_A");
                if (Audio != null && clipFireSpell[randomClipFireSpell] != null)
                {
                    Audio.PlayOneShot(clipFireSpell[randomClipFireSpell]);
                }
                Spell.Shoot();
                break;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PelotaComun" && gameObject.tag == "Tirador")
        {
            //ANIMACION DE DAMAGE

            //animator.Play("UD_archer_09_take_damage");
            enableTimerDamage = true;
            enableMovement = false;
            //animator.SetBool("Run", false);


            //---------------------------------------
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
            //ANIMACION DE DAMAGE

            //animator.Play("UD_archer_09_take_damage");
            enableTimerDamage = true;
            enableMovement = false;
            animator.SetBool("Run", false);


            //---------------------------------------
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
            //ANIMACION DE DAMAGE

            //animator.Play("UD_archer_09_take_damage");
            enableTimerDamage = true;
            enableMovement = false;
            animator.SetBool("Run", false);


            //---------------------------------------
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