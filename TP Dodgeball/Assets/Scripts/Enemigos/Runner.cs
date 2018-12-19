﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Runner : Enemy
{

    // Use this for initialization
    public float auxLife;
    public Pool pool;
    private PoolObject poolObject;
    public float speed;
    private float timeState;
    private float effectFire;
    private float auxSpeed;
    private Rigidbody rig;
    private float dileyInsta;
    public float MaxSpeed;
    public int PatternOfMovement;
    public float rangeBend;
    public float rangeEnemyVision;
    private Player player;

    public Pool poolPowerImmune;
    public Pool poolDoblePoints;
    public Pool poolInstaKill;

    void Start()
    {
        if (Player.InstancePlayer != null)
        {
            player = Player.InstancePlayer;
        }
        dileyInsta = 1;
        SetDodge(false);
        SetEnemyState(EstadoEnemigo.normal);
        rig = GetComponent<Rigidbody>();
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;
        auxSpeed = speed;
        effectFire = 0;
        effectFrozen.SetActive(false);
        effectBurned.SetActive(false);
        effectMusic.SetActive(false);
    }

    // Update is called once per frame
    public void On()
    {
        if (Player.InstancePlayer != null)
        {
            player = Player.InstancePlayer;
        }
        dileyInsta = 1;
        SetDodge(false);
        SetEnemyState(EstadoEnemigo.normal);
        SetDead(false);
        life = auxLife;
        life = maxLife;
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
    void Update()
    {
        if (Player.GetPlayer() != null)
        {
            if (!player.GetActiveInstaKill())
            {
                dileyInsta = 1;
            }
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
                }
            }
        }
        
        UpdateHP();
        if (GetEnemyState() != EstadoEnemigo.frozen && GetEnemyState() != EstadoEnemigo.dance)
        {
            Movement();
        }
        if (GetDead())
        {
            // Seguir configurando la probabilidad de aparicion de los powers ups
            float assistant = Random.Range(1, 100);
            if (assistant > 90 && assistant <= 94)
            {
                GameObject go = poolPowerImmune.GetObject();
                if (go != null)
                {
                    poolPowerImmune.SubstractId();
                    go.transform.position = transform.position;
                    go.transform.rotation = transform.rotation;
                }
            }
            if (assistant > 12 && assistant <= 25)
            {
                GameObject go = poolDoblePoints.GetObject();
                if (go != null)
                {
                    poolDoblePoints.SubstractId();
                    go.transform.position = transform.position;
                    go.transform.rotation = transform.rotation;
                }
            }
            if (assistant > 30 && assistant <= 35)
            {
                GameObject go = poolInstaKill.GetObject();
                if (go != null)
                {
                    poolInstaKill.SubstractId();
                    go.transform.position = transform.position;
                    go.transform.rotation = transform.rotation;
                }
            }
            if (player.GetDoblePoints())
            {
                player.AddScore(50 * 2);
            }
            else
            {
                player.AddScore(50);
            }
            if (GameManager.GetGameManager() != null)
            {
                GameManager.GetGameManager().AddDeath();
            }
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
                poolObject.Recycle();
            }
        }
        if (timeState > 0)
        {
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
            timeState = timeState - Time.deltaTime;

        }
        if (timeState <= 0)
        {
            if (GetEnemyState() == EstadoEnemigo.frozen)
            {
                speed = auxSpeed;
                SetEnemyState(EstadoEnemigo.normal);
            }
            if (GetEnemyState() == EstadoEnemigo.dance)
            {
                SetEnemyState(EstadoEnemigo.normal);
            }
            if (GetEnemyState() == EstadoEnemigo.Burned)
            {
                SetEnemyState(EstadoEnemigo.normal);

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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Corredor" || collision.gameObject.tag == "Tirador" && PatternOfMovement == 1)
        {
            collision.gameObject.transform.Rotate(0, 180, 0);
        }
        if(collision.gameObject.tag == "Pared")
        {
            transform.Rotate(0, 90, 0);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Piso")
        {
            SetTouchFloor(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Piso")
        {
            SetTouchFloor(true);
        }
        if (other.gameObject.tag == "PelotaComun")
        {
            if (player != null)
            {
                life = life - (GetDamageCommonBall() + player.GetAdditionalDamageCommonBall());
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
                SetEnemyState(EstadoEnemigo.frozen);
                effectFrozen.SetActive(true);
                timeState = 5;//tiempo por el cual el enemigo "Corredor" estara congelado
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
                life = life - (GetDamageMiniBall() + Player.GetPlayer().GetAditionalDamageMiniBalls());
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
                timeState = 7;//tiempo por el cual el enemigo estara bailando
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
                life = life - (GetDamageExplociveBall() + player.GetAdditionalDamageExplociveBall());
            }
            IsDead();

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
        if (speed < MaxSpeed)
        {
            speed = speed + Random.Range(0.01f, 1f);
            auxSpeed = speed;
        }
    }
    public void Movement()
    {
        if (player != null)
        {
            if (PatternOfMovement == 0)
            {
                rig.velocity = Vector3.zero;
                rig.angularVelocity = Vector3.zero;

                transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
                // si no esta colicionando con el piso que esto no se ejecute
                if (!GetTouchFloor())
                {
                    transform.position += transform.forward * Time.deltaTime * speed;
                }
            }
        }
        if (PatternOfMovement == 1)
        {
            rig.velocity = Vector3.zero;
            rig.angularVelocity = Vector3.zero;
            transform.position += transform.forward * Time.deltaTime * speed;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, rangeBend))
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
}

