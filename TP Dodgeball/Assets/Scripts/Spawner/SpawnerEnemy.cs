using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour {

    // Use this for initialization
    public Pool poolEnemigo;
    private PoolObject poolObject;
    //public GameObject Enemigo;
    public int initialEnemyAmount;
    public float dileyCreation;
    private int TOP_CREATION;
    private float auxDileyCreation;
    public float rangeX;
    public float rangeZ;
    private int creations;
    public float enemySpeed;
    private bool inWorking;
    public float incrementCreation;
    private int TOP_MAXIMUM;
    public int enemyType;
    public int enemyPattern;
    public float damageShooter;
    public float powerShooter;
    public float enemyVisionRange;
    public bool avoidInstantCreation;
    public bool activedInstantCreation;
    public float DileyShooter;
    public float substractHeigtEnemy1;
    public float substractHeigtEnemy2;
    void Start() {
        auxDileyCreation = dileyCreation;
        TOP_CREATION = initialEnemyAmount;
        inWorking = true;
        TOP_MAXIMUM = poolEnemigo.count;
        if (!avoidInstantCreation)
        {
            dileyCreation = 0;
        }
    }

    // Update is called once per frame
    void Update() {
        if(GameManager.GetGameManager().checkVictory)
        {
            GameManager.GetGameManager().CheckVictory();
        }
        if (GameManager.GetGameManager().enemyAmountOnScreen <= 0)
        {
            GameManager.GetGameManager().SetEntrarRonda(true);
        }
        if (inWorking && TOP_CREATION < TOP_MAXIMUM && GameManager.GetGameManager().survival && GameManager.GetGameManager().GetVictory() == false && poolEnemigo.GetId() < poolEnemigo.count)
        {
            if (dileyCreation > 0)
            {
                dileyCreation = dileyCreation - Time.deltaTime;
            }
            if (dileyCreation <= 0 && creations < TOP_CREATION)
            {
                if (GameManager.GetGameManager() != null)
                {
                    GameManager.GetGameManager().AddEnemyAmoutOnScreen();
                }
                creations++;
                if (enemyType == 1)
                {
                    GameObject go = poolEnemigo.GetObject();
                    Runner runner = go.GetComponent<Runner>();
                    go.transform.position = transform.position + new Vector3(Random.Range(0, rangeX), (transform.position.y - substractHeigtEnemy1), Random.Range(0, rangeZ));
                    go.transform.rotation = transform.rotation;
                    runner.On();
                    runner.rangeEnemyVision = enemyVisionRange;
                    runner.PatternOfMovement = enemyPattern;
                    if (GameManager.GetGameManager().GetRound() > 0)
                    {
                        runner.AddSpeed();
                    }
                }
                if (enemyType == 2)
                {
                    GameObject go = poolEnemigo.GetObject();
                    Shooter shooter = go.GetComponent<Shooter>();
                    go.transform.position = transform.position + new Vector3(Random.Range(0, rangeX), (transform.position.y - substractHeigtEnemy2), Random.Range(0, rangeZ));
                    go.transform.rotation = transform.rotation;
                    if (DileyShooter > 0)
                    {
                        shooter.dilay = DileyShooter;
                    }
                    shooter.On();
                    shooter.enemyVisionRange = enemyVisionRange;
                    shooter.patternType = enemyPattern;
                    if (damageShooter > 0)
                    {
                        shooter.damage = damageShooter;
                    }
                    if(powerShooter> 0)
                    {
                        shooter.powerShoot = powerShooter;
                    }
                    if (GameManager.GetGameManager().GetRound() > 1)
                    {
                        shooter.AddSpeed();
                    }
                }
                dileyCreation = auxDileyCreation;
            }
            if (creations >= TOP_CREATION)
            {
                creations = 0;
                TOP_CREATION = TOP_CREATION + (int)incrementCreation;
                inWorking = false;
            }
        }
        if (inWorking && GameManager.GetGameManager().history && activedInstantCreation == false && GameManager.GetGameManager().GetVictory() == false && poolEnemigo.GetId() < poolEnemigo.count)
        {
            if (dileyCreation > 0)
            {
                dileyCreation = dileyCreation - Time.deltaTime;
            }
            if (dileyCreation <= 0)
            {
                
                if (enemyType == 1)
                {

                    GameObject go = poolEnemigo.GetObject();
                    Runner runner = go.GetComponent<Runner>();
                    go.transform.position = transform.position + new Vector3(Random.Range(0, rangeX), transform.position.y - substractHeigtEnemy1, Random.Range(0, rangeZ));
                    go.transform.rotation = transform.rotation;
                    runner.On();
                    runner.speed = enemySpeed;
                    runner.PatternOfMovement = enemyPattern;
                    if (GameManager.GetGameManager().GetRound() > 0)
                    {
                        runner.AddSpeed();
                    }
                }
                if (enemyType == 2)
                {
                    GameObject go = poolEnemigo.GetObject();
                    Shooter shooter = go.GetComponent<Shooter>();
                    go.transform.position = transform.position + new Vector3(Random.Range(0, rangeX), transform.position.y - substractHeigtEnemy2, Random.Range(0, rangeZ));
                    go.transform.rotation = transform.rotation;
                    if (damageShooter > 0)
                    {
                        shooter.damage = damageShooter;
                    }
                    if (powerShooter > 0)
                    {
                        shooter.powerShoot = powerShooter;
                    }
                    shooter.On();
                    shooter.speed = enemySpeed;
                    shooter.patternType = enemyPattern;
                    if (DileyShooter > 0)
                    {
                        shooter.dilay = DileyShooter;
                    }
                    if (GameManager.GetGameManager().GetRound() > 1)
                    {
                        shooter.AddSpeed();
                    }
                }
                
                dileyCreation = auxDileyCreation;
            }
        }
    }
    public void SetInWorking(bool _inWorking)
    {
        inWorking = _inWorking;
    }
    public bool GetInWorking()
    {
        return inWorking;
    }
    public int GetCreations()
    {
        return creations;
    }
    public void SetCreations(int _creations)
    {
        creations = _creations;
    }
    public void SetTopCreation(int _TOP_CREATION)
    {
        TOP_CREATION = _TOP_CREATION;
    }
    public int GetTopCreation()
    {
        return TOP_CREATION;
    }
}