using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : Enemy {

    // Use this for initialization
    public enum States
    {
        swim = 0,
        follow,
        attack,
        backingOut,
        Count
    }

    public enum Events
    {
        inRank = 0,
        outOfRange,
        inRangeOfAttack,
        outSideTheRangeOfAttack,
        afterAttacking,
        goingStill,
        Count
    }
    private Player Player;
    public float damage;
    public float powerAttack;
    public float reduceAttackPower;
    public float reduceDamageCommonBall;
    public float reduceDamageExplociveBall;
    public float reduceDamageFragmentBall;
    public float increaseDamageFireBall;
    public float speed;
    public Transform[] waypoints;
    public Pool pool;
    public BoxCollider weakPoint;

    private PoolObject poolObject;
    private float auxSpeedAttack;
    private float auxSpeed;
    private float timeState;
    private float effectFire;
    private States state;
    private Events _events;
    private int id = 0;
    private Rigidbody rig;
    private float speedAttack;
    private Vector3 posPlayer;
    //private FSM fsm;
    public void Prendido()
    {
        if (Player.InstancePlayer != null)
        {
            Player = Player.InstancePlayer;
        }
        rig = GetComponent<Rigidbody>();
        state = States.swim;
        if (powerAttack <= 0)
        {
            powerAttack = 1;
        }
        speedAttack = speed * powerAttack;
        speedAttack = speedAttack - reduceAttackPower;
        auxSpeedAttack = speedAttack;
        auxSpeed = speed;
    }
    void Start()
    {
        if(Player.InstancePlayer != null)
        {
            Player = Player.InstancePlayer;
        }
        rig = GetComponent<Rigidbody>();
        state = States.swim;
        if(powerAttack <= 0)
        {
            powerAttack = 1;
        }
        speedAttack = speed * powerAttack;
        speedAttack = speedAttack - reduceAttackPower;
        auxSpeedAttack = speedAttack;
        auxSpeed = speed;
    }
	
	// Update is called once per frame
	void Update () {
        UpdateStates();
        UpdateHP();
        if (state != States.attack)
        {
            UpdatePositionPlayer();
        }

        if (GetDead())
        {
            if(Player != null)
            {
                Player.AddScore(250);
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
                    if (Player != null)
                    {
                        if (Player.GetDoblePoints())
                        {
                            Player.AddScore(5 * 2);
                        }
                        else
                        {
                            Player.AddScore(5);
                        }
                        life = life - (GetDamageFireBall() + Player.GetAdditionalDamageFireBall() + increaseDamageFireBall);
                        IsDead();
                    }
                    effectFire = 0;
                }
                if(GetEnemyState() == EstadoEnemigo.frozen)
                {
                    speedAttack = 0;
                    speed = 0;
                }
            }
            timeState = timeState - Time.deltaTime;
        }

        if (timeState <= 0)
        {
            if (GetEnemyState() == EstadoEnemigo.frozen)
            {
                speedAttack = auxSpeedAttack;
                speed = auxSpeed;
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
            case (int)States.swim:
                Swim();
                break;
            case (int)States.follow:
                Follow();
                break;
            case (int)States.attack:
                Attack();
                break;
            case (int)States.backingOut:
                BackingOut();
                break;
        }
    }
    public void UpdatePositionPlayer()
    {
        if (Player != null)
        {
            posPlayer = Player.transform.position;
        }
    }
    public void Swim()
    {
        if (weakPoint != null)
        {
            weakPoint.enabled = false;
        }
        if (waypoints.Length > 0)
        {
            if (waypoints[id] != null)
            {
                Vector3 target = waypoints[id].position;
                transform.LookAt(target);
                transform.position = transform.position + transform.forward * Time.deltaTime * speed; 
                if (id >= waypoints.Length)
                {
                    id = 0;
                }
                
            }
        }
        else
        {
            id = 0;
        }
    }
    public void Follow()
    {
        if (Player != null)
        {
            transform.LookAt(Player.transform.position);
            transform.position = transform.position + transform.forward * Time.deltaTime * speed;
        }
    }
    public void Attack()
    {
        if (weakPoint != null)
        {
            weakPoint.enabled = true;
        }
        if(Player != null)
        {
            transform.LookAt(posPlayer);
            transform.position = transform.position + transform.forward * Time.deltaTime * speedAttack;
            if (transform.position.y < Player.transform.position.y)
            {
                state = States.backingOut;
            }
        }
        
    }
    public void BackingOut()
    {
        state = States.swim;
        id++;
        if (id >= waypoints.Length)
        {
            id = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (weakPoint.enabled == true)
        {
            if (other.gameObject.tag == "PelotaComun")
            {
                if (Player != null)
                {
                    life = life - ((GetDamageCommonBall() + Player.GetAdditionalDamageCommonBall()) - reduceDamageCommonBall);
                    IsDead();
                    if (Player.GetDoblePoints())
                    {
                        Player.AddScore(10 * 2);
                    }
                    else
                    {
                        Player.AddScore(10);
                    }
                }
            }
            if (other.gameObject.tag == "PelotaDeHielo")
            {
                if (Player != null)
                {
                    if (Player.GetDoblePoints())
                    {
                        Player.AddScore(10 * 2);
                    }
                    else
                    {
                        Player.AddScore(10);
                    }
                    life = life - (GetDamageIceBall() + Player.GetAdditionalDamageIceBall());
                }
                IsDead();
                if (speed > 0 || speedAttack > 0)
                {
                    speed = speed - 20f;
                    speedAttack = speedAttack - 20f;
                    //velMovimiento = 0;
                }
                if (speed <= 0 || speedAttack <= 0)
                {
                    SetEnemyState(EstadoEnemigo.frozen);
                    effectFrozen.SetActive(true);
                    timeState = 2.5f;//tiempo por el cual el enemigo "Corredor" estara congelado
                }
            }
            if (other.gameObject.tag == "MiniPelota")
            {
                if (Player != null)
                {
                    if (Player.GetDoblePoints())
                    {
                        Player.AddScore(10 * 2);
                    }
                    else
                    {
                        Player.AddScore(10);
                    }
                    life = life - ((GetDamageMiniBall() + Player.GetAditionalDamageMiniBalls())- reduceDamageFragmentBall);
                    IsDead();
                }
            }
            if (other.gameObject.tag == "PelotaDanzarina")
            {
                if (Player != null)
                {
                    if (Player.GetDoblePoints())
                    {
                        Player.AddScore(5 * 2);
                    }
                    else
                    {
                        Player.AddScore(5);
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
                speed = auxSpeed;
                speedAttack = auxSpeedAttack;
            }
            if (other.gameObject.tag == "PelotaExplociva")
            {
                if (Player != null)
                {
                    if (Player.GetDoblePoints())
                    {
                        Player.AddScore(20 * 2);
                    }
                    else
                    {
                        Player.AddScore(20);
                    }
                    life = life - ((GetDamageExplociveBall() + Player.GetAdditionalDamageExplociveBall()) - reduceDamageExplociveBall);
                }
                IsDead();

            }
        }
        if (other.tag == "Player")
        {
            state = States.backingOut;
            id++;
            if (id >= waypoints.Length)
            {
                id = 0;
            }
            if(Player != null)
            {
                if (Player.armor > 0)
                {
                    Player.armor = Player.armor - damage;
                }
                else
                {
                    Player.life = Player.life - damage;
                }
            }
        }
        if (other.tag == "WaypointRandom")
        {
            float random = Random.Range(1, 100);
            if (random >= 80)
            {
                state = States.attack;
            }
            if(random < 80)
            {
                id++;
                if (id >= waypoints.Length)
                {
                    id = 0;
                }
            }
            random = 0;
        }
        if(other.tag == "Waypoint")
        {
            id++;
            if (id >= waypoints.Length)
            {
                id = 0;
            }
        }
    }
}
