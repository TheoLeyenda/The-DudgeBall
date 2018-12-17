using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class Submarino : Enemigo{

    public enum States
    {
        patrol = 0,
        patrolVulnerable,
        patrolShooting,
        follow,
        attackTorpedos,
        attackWithEverything,
        still,
    }
    private Jugador player;
    public PoolPelota poolTorpedos;
    public float dileyShootingTorpedos;
    public float SpeedMov;
    public Transform[] waypoints;
    public GameObject[] GeneratorTorpedos;
    public States state;
    public GameObject particleBubbles;
    public TorretaSubmarino[] turrets;
    public float ReduceDamageCommonBall;
    public float ReduceDamageIceBall;
    public float ReduceDamageMiniBall;
    public float ReduceDamageExplocive;

    private int id;
    private float auxSpeedMov;
    private float timeState;
    private float effectFire;
    private float auxDileyShootingTorpedos;
    private bool WeakPointActived;
    private PoolObject poolObject;
    private Rigidbody rig;

    public void On()
    {
        //PONER LO MISMO QUE EN EL "START();".
        if (Jugador.InstancePlayer != null)
        {
            player = Jugador.InstancePlayer;
        }
        id = 0;
        poolObject = GetComponent<PoolObject>();
        if (effectFrozen != null)
        {
            effectFrozen.SetActive(false);
        }
        if (effectBurned != null)
        {
            effectBurned.SetActive(false);
        }
        if (effectMusic != null)
        {
            effectMusic.SetActive(false);
        }
        rig = GetComponent<Rigidbody>();
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;
        SetEnemyState(EstadoEnemigo.normal);
        auxDileyShootingTorpedos = dileyShootingTorpedos;
        
    }
    void Start () {
        if(Jugador.InstancePlayer!= null)
        {
            player = Jugador.InstancePlayer;
        }
        id = 0;
        poolObject = GetComponent<PoolObject>();
        if (effectFrozen != null)
        {
            effectFrozen.SetActive(false);
        }
        if (effectBurned != null)
        {
            effectBurned.SetActive(false);
        }
        if (effectMusic != null)
        {
            effectMusic.SetActive(false);
        }
        rig = GetComponent<Rigidbody>();
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;
        SetEnemyState(EstadoEnemigo.normal);
        auxDileyShootingTorpedos = dileyShootingTorpedos;
        auxSpeedMov = SpeedMov;
    }
	
	// Update is called once per frame
	void Update () { 
        UpdateHP();
        UpdateStates();
        if (WeakPointActived)
        {
            CheckDead();
        }
        if (timeState > 0)
        {
            if (GetEnemyState() == EstadoEnemigo.dance)
            {
                SetRotateY(90);
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
                if (GetEnemyState() == EstadoEnemigo.frozen)
                {
                    SpeedMov = 0;
                }
            }
            timeState = timeState - Time.deltaTime;
        }

        if (timeState <= 0)
        {
            if (GetEnemyState() == EstadoEnemigo.frozen)
            {

                SpeedMov = auxSpeedMov;
                effectFrozen.SetActive(false);
                effectBurned.SetActive(false);
                effectMusic.SetActive(false);
                SetEnemyState(EstadoEnemigo.normal);

            }
            if (GetEnemyState() == EstadoEnemigo.dance)
            {
                effectMusic.SetActive(false);
                effectBurned.SetActive(false);
                effectFrozen.SetActive(false);
                SetEnemyState(EstadoEnemigo.normal);
            }
            if (GetEnemyState() == EstadoEnemigo.Burned)
            {
                effectBurned.SetActive(false);
                effectMusic.SetActive(false);
                effectFrozen.SetActive(false);
                SetEnemyState(EstadoEnemigo.normal);
            }
        }
    }

    public void UpdateStates()
    {
        switch ((int)state)
        {
            case (int)States.patrol:
                Patrol();
                break;
            case (int)States.patrolVulnerable:
                PatrolVulnerable();
                break;
            case (int)States.patrolShooting:
                PatrolShooting();
                break;
            case (int)States.follow:
                Follow();
                break;
            case (int)States.attackTorpedos:
                AttackTorpedos();
                break;
            case (int)States.attackWithEverything:
                AttackWithEverything();
                break;
            case (int)States.still:
                Still();
                break;
        }
    }

    //------------------TENGO QUE HACER QUE SI TOCA CIERTO WAYPOINT PASE DE ESTADO A ESTADO Y CADA VEZ QUE TOQUE UN WAYPOINT HAGA "id++".-----------------------------//

    public void Still()
    {
        if (particleBubbles != null)
        {
            particleBubbles.SetActive(false);
        }
    }
   
    //(HECHO)
    //Patrullar: patrulla moviéndose por los distintos waypoints(no tiene activo
    //Su punto débil)
    // TAG PARA ENTRAR EN "Patrullar()" = "WaypointPatrullaje"
    public void Patrol()
    {
        for(int i = 0; i< turrets.Length; i++)
        {
            if(turrets[i] != null)
            {
                turrets[i].SetShooting(false);
            }
        }
        WeakPointActived = false;
        if (waypoints.Length > 0)
        {
            if (id >= waypoints.Length)
            {
                id = 0;
            }
            if (waypoints[id] != null)
            {
                Vector3 target = waypoints[id].position;
                transform.LookAt(target);
                transform.position = transform.position + transform.forward * Time.deltaTime * SpeedMov;
            }
        }

    }
    //(HECHO)
    //PatrullarBulnerable: en este estado el submarino patrulla pero tiene su
    //Punto débil activo.
    // TAG PARA ENTRAR EN "PatrullarBulnerable()" = "WaypointPatrullarBulnerable"
    public void PatrolVulnerable()
    {
        for (int i = 0; i < turrets.Length; i++)
        {
            if (turrets[i] != null)
            {
                turrets[i].SetShooting(false);
            }
        }
        WeakPointActived = true;
        if (waypoints.Length > 0)
        {
            if (id >= waypoints.Length)
            {
                id = 0;
            }
            if (waypoints[id] != null)
            {
                Vector3 target = waypoints[id].position;
                transform.LookAt(target);
                transform.position = transform.position + transform.forward * Time.deltaTime * SpeedMov;
            }
        }
    }

    
    //(HECHO)
    //AtacarTorpedos: el submarino pasa a este estado cuando pasa
    //Por un waypoints especifico(tiene activo su punto débil), dispara sus
    //Torpedos.
    //TAG PARA ENTRAR EN "AtacarTorpedos()" = "WaypointAtacarTorpedos"
    public void AttackTorpedos()
    {
        for (int i = 0; i < turrets.Length; i++)
        {
            if (turrets[i] != null)
            {
                turrets[i].SetShooting(false);
            }
        }
        WeakPointActived = true;
        if (waypoints.Length > 0)
        {
            if (id >= waypoints.Length)
            {
                id = 0;
            }
            if (waypoints[id] != null)
            {
                Vector3 target = waypoints[id].position;
                transform.LookAt(target);
                transform.position = transform.position + transform.forward * Time.deltaTime * SpeedMov;
                
                if(dileyShootingTorpedos> 0)
                {
                    dileyShootingTorpedos = dileyShootingTorpedos - Time.deltaTime;
                }
                if(dileyShootingTorpedos<= 0)
                {
                    for (int i = 0; i < GeneratorTorpedos.Length; i++)
                    {                                                           // esta condicion del GetId() 
                                                                             //sirve para que no se pase del array
                        if (GeneratorTorpedos[i].activeSelf == true && poolTorpedos.GetId() < poolTorpedos.count)
                        {
                            GameObject go = poolTorpedos.GetObject();
                            Torpedo torpedo = go.GetComponent<Torpedo>();
                            go.transform.position = GeneratorTorpedos[i].transform.position;
                            go.transform.rotation = GeneratorTorpedos[i].transform.rotation;
                            torpedo.On();
                        }
                    }
                    dileyShootingTorpedos = auxDileyShootingTorpedos;
                }
            }
        }
    }
    public void Follow()
    {
        for (int i = 0; i < turrets.Length; i++)
        {
            if (turrets[i] != null)
            {
                turrets[i].SetShooting(false);
            }
        }
        WeakPointActived = true;
        if(player != null)
        {
            transform.LookAt(player.transform.position);
        }
        transform.position = transform.position + transform.forward * Time.deltaTime * SpeedMov;
        if (dileyShootingTorpedos > 0)
        {
            dileyShootingTorpedos = dileyShootingTorpedos - Time.deltaTime;
        }
        if (dileyShootingTorpedos <= 0)
        {
            for (int i = 0; i < GeneratorTorpedos.Length; i++)
            {                                                           // esta condicion del GetId() 
                                                                        //sirve para que no se pase del array
                if (GeneratorTorpedos[i].activeSelf == true && poolTorpedos.GetId() < poolTorpedos.count)
                {
                    GameObject go = poolTorpedos.GetObject();
                    Torpedo torpedo = go.GetComponent<Torpedo>();
                    go.transform.position = GeneratorTorpedos[i].transform.position;
                    go.transform.rotation = GeneratorTorpedos[i].transform.rotation;
                    torpedo.On();
                }
            }
            dileyShootingTorpedos = auxDileyShootingTorpedos;
        }
        // FALTA HACER QUE MIENTRAS SIGA ATAQUE.
    }

    public void PatrolShooting()
    {
        for (int i = 0; i < turrets.Length; i++)
        {
            if (turrets[i] != null)
            {
                turrets[i].SetShooting(true);
            }
        }
        WeakPointActived = true;
        if (waypoints.Length > 0)
        {
            if (id >= waypoints.Length)
            {
                id = 0;
            }
            if (waypoints[id] != null)
            {
                Vector3 target = waypoints[id].position;
                transform.LookAt(target);
                transform.position = transform.position + transform.forward * Time.deltaTime * SpeedMov;
            }
        }
    }

    public void AttackWithEverything()
    {
        for (int i = 0; i < turrets.Length; i++)
        {
            if (turrets[i] != null)
            {
                turrets[i].SetShooting(true);
            }
        }
        WeakPointActived = true;
        if (waypoints.Length > 0)
        {
            if (id >= waypoints.Length)
            {
                id = 0;
            }
            if (waypoints[id] != null)
            {
                Vector3 target = waypoints[id].position;
                transform.LookAt(target);
                transform.position = transform.position + transform.forward * Time.deltaTime * SpeedMov;

                if (dileyShootingTorpedos > 0)
                {
                    dileyShootingTorpedos = dileyShootingTorpedos - Time.deltaTime;
                }
                if (dileyShootingTorpedos <= 0)
                {
                    for (int i = 0; i < GeneratorTorpedos.Length; i++)
                    {                                                           // esta condicion del GetId() 
                                                                                //sirve para que no se pase del array
                        if (GeneratorTorpedos[i].activeSelf == true && poolTorpedos.GetId() < poolTorpedos.count)
                        {
                            GameObject go = poolTorpedos.GetObject();
                            Torpedo torpedo = go.GetComponent<Torpedo>();
                            go.transform.position = GeneratorTorpedos[i].transform.position;
                            go.transform.rotation = GeneratorTorpedos[i].transform.rotation;
                            torpedo.On();
                        }
                    }
                    dileyShootingTorpedos = auxDileyShootingTorpedos;
                }
            }
        }
    }


    public void CheckDead()
    {
        if (life <= 0)
        {
            if (GetDead())
            {
                if (player != null)
                {
                    player.AddScore(250);
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
        if (other.tag == "WaypointPatrullaje")
        {
            state = States.patrol;
            if (id >= waypoints.Length)
            {
                id = 0;
            }
            else
            {
                id++;
            }
        }
        if(other.tag == "WaypointAtacarConTodo")
        {
            state = States.attackWithEverything;
            if (id >= waypoints.Length)
            {
                id = 0;
            }
            else
            {
                id++;
            }
        }
        if (other.tag == "WaypointPatrullarBulnerable")
        {
            state = States.patrolVulnerable;
            if (id >= waypoints.Length)
            {
                id = 0;
            }
            else
            {
                id++;
            }
        }
        if (other.tag == "WaypointPatrullarDisparando")
        {
            state = States.patrolShooting;
            if (id >= waypoints.Length)
            {
                id = 0;
            }
            else
            {
                id++;
            }
        }
        if (other.tag == "WaypointAtacarTorpedos")
        {
            state = States.attackTorpedos;
            if (id >= waypoints.Length)
            {
                id = 0;
            }
            else
            {
                id++;
            }
        }
        if (WeakPointActived)
        {
            if (other.gameObject.tag == "PelotaComun")
            {
                if (player != null)
                {
                    life = life - (GetDamageCommonBall() + player.GetAdditionalDamageCommonBall() - ReduceDamageCommonBall);
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
                    life = life - (GetDamageIceBall() + player.GetAdditionalDamageIceBall()-ReduceDamageIceBall);
                }
                IsDead();
                if (SpeedMov > 0)
                {
                    SpeedMov = SpeedMov - 5;

                    //velMovimiento = 0;
                }
                if (SpeedMov <= 0)
                {
                    SetEnemyState(EstadoEnemigo.frozen);
                    effectFrozen.SetActive(true);
                    timeState = 2.5f;//tiempo por el cual el enemigo "Corredor" estara congelado
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
                    life = life - (GetDamageMiniBall() + player.GetAditionalDamageMiniBalls() - ReduceDamageMiniBall);
                    IsDead();
                }
            }
            if (other.gameObject.tag == "PelotaDanzarina")
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
                SpeedMov = auxSpeedMov;
            }
            if (other.gameObject.tag == "PelotaExplociva")
            {
                if (player != null)
                {
                    if (player.GetDoblePoints())
                    {
                        player.AddScore(20 * 2);
                    }
                    else
                    {
                        player.AddScore(20);
                    }
                    life = life - ((GetDamageExplociveBall() + player.GetAdditionalDamageExplociveBall()) - ReduceDamageExplocive);
                }
                IsDead();

            }
        }
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)