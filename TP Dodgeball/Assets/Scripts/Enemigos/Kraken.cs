using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
public class Kraken : Enemy {

    public struct RotationData
    {
        public float rotationX;
        public float rotationY;
        public float rotationZ;
    }
    public enum ROTATION
    {
        RotNormal = 0,
        RotAttack1,
        RotAttack2,
        Count
    }
    public enum States
    {

        Swiming = 0,
        Attacking,
        BackingOut,
        Count
    }

    // Use this for initialization
    private Player Player;
    private States states;
    private PoolObject poolObject;
    private Rigidbody rig;
    private Vector3 posPlayer;
    private int id = 0;
    private float auxPowerImpulseMov;
    private float auxDileyImpulse;
    private int AttackKind;
    private float auxDileyShoot;
    private float auxTimeShooting;
    private RotationData datRotation;
    private ROTATION StateRotation;
    private float timeEstado;
    //private float auxImpulsoDeAtaque;
    private float effectFire;
    private float auxDileyMovLeft;
    private float auxDileyMovRight;

    public float danioIncreasedCommonBall;
    public float danioIncreasedFireBall;
    public float movHorizontal;
    public float DileyMovRight;
    public float DileyMovLeft;
    public float ShootingTime;
    public float dileyShoot;
    public GameObject[] generatorBall;
    public Transform[] waypoints;
    public Pool poolInkBall;
    public float dileyImpulse;
    public float PowerImpulseMov;
    public BoxCollider weakPoint;
    public BoxCollider midpointOfTheBody;
    public float powerAttack;
    //public float impulsoDeAtaque;

    void Start () {
        if (Player.InstancePlayer != null)
        {
            Player = Player.InstancePlayer;
        }
        auxDileyMovRight = DileyMovRight;
        auxDileyMovLeft = DileyMovLeft;
        DileyMovRight = 0;
        auxTimeShooting = ShootingTime;
        ShootingTime = 0;
        auxDileyShoot = dileyShoot;
        auxDileyImpulse = dileyImpulse;
        auxPowerImpulseMov = PowerImpulseMov;
        id = 0;
        //estados = States.Nadando;
        StateRotation = ROTATION.RotNormal;
        rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale > 0)
        {
            if (ShootingTime <= 0)
            {
                AttackKind = 2;
            }
            UpdateHP();
            UpdateStates();
            UpdateRotation();
            UpdatePositionPlayer();
            CheckDead();
        }
        //CheckEstados();
    }
    public void CheckDead()
    {
        if (GetDead())
        {
            if (Player != null)
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
    }
   
    public void ThrowBall()
    {
        
        for (int i = 0; i < generatorBall.Length; i++)
        {
            if (ShootingTime > 0)
            {
                ShootingTime = ShootingTime - Time.deltaTime;
                if (generatorBall[i].activeSelf == true)
                {
                    GameObject go = poolInkBall.GetObject();
                    EnemyBall pelota = go.GetComponent<EnemyBall>();
                    go.transform.position = generatorBall[i].transform.position;
                    go.transform.rotation = generatorBall[i].transform.rotation;
                    pelota.Shoot();
                }
            }
        }
    }
    
    public void ActiveShooting()
    {
        ShootingTime = auxTimeShooting;
    }
    public void UpdateRotation()
    {
        switch((int)StateRotation)
        {
            case (int)ROTATION.RotNormal:
                break;
            case (int)ROTATION.RotAttack1:
                SetDataRotation();
                break;
            case (int)ROTATION.RotAttack2:
                SetDataRotation();
                break;
        }
    }
    public void UpdateStates()
    {
        switch ((int)states)
        {
            case (int)States.Swiming:
                Swiming();
                break;
            case (int)States.Attacking:
                Attacking(AttackKind);
                break;
            case (int)States.BackingOut:
                BackingOut();
                break;
        }
    }
    public void SetDataRotation()
    {

        if (Player != null)
        {
            if (AttackKind == 1)
            {
                transform.LookAt(new Vector3(Player.transform.position.x, transform.position.y + 90, Player.transform.position.z));
            }
            if (AttackKind == 2)
            {
                transform.LookAt(posPlayer);
                
            }
        }
    }
    public void Swiming()
    {
        StateRotation = ROTATION.RotNormal;
        midpointOfTheBody.enabled = true;
        weakPoint.enabled = false;
        if (waypoints.Length > 0)
        {
            if (waypoints[id] != null)
            {
                Vector3 target = waypoints[id].position;
                transform.LookAt(target);
                transform.position = transform.position + transform.forward * Time.deltaTime * PowerImpulseMov;
                Vector3 diff = target - this.transform.position;

                if (diff.magnitude < 0.3f)
                {
                    float random = Random.Range(1, 100);
                    if (random < 75)
                    {
                        id++;
                        PowerImpulseMov = 2;
                        if (id >= waypoints.Length)
                        {
                            id = 0;
                        }
                    }
                    if(random >= 75)
                    {
                        float randomTipoAtaque = Random.Range(1, 100);
                        if(randomTipoAtaque<60)
                        {
                           
                            AttackKind = 1;
                            ActiveShooting();
                            states = States.Attacking;
                        }
                        if(randomTipoAtaque >=60)
                        {
                           
                            AttackKind = 2;
                            states = States.Attacking;
                        }
                            
                    }
                    
                }
                if (PowerImpulseMov > 2)
                {
                    PowerImpulseMov = PowerImpulseMov - (Time.deltaTime* 5.2f);
                }
                if(PowerImpulseMov <= 2)
                {
                    if(dileyImpulse > 2)
                    {
                        dileyImpulse = dileyImpulse - (Time.deltaTime* 5.2f);
                    }
                    if(dileyImpulse <= 2)
                    {
                        dileyImpulse = auxDileyImpulse;
                        PowerImpulseMov = auxPowerImpulseMov;
                    }
                }
            }
        }
    }
    public void Attacking(int AttackKind)
    {
        //FALTA HACER QUE EL KRAKEN VAYA HACIA AL JUGADOR Y LE DE UNA OSTIA QUE LO DEJE AL OTRO LADO DEL MAPA(QUE LE APLIQUE UNA FUERZA AL JUGADOR QUE LO
        //SAQUE VOLANTO), LUEGO DE ESTO HACER QUE EL KRAKEN PASE AL ESTADO  RETIRARSE
        if (transform.position.y <= Player.transform.position.y)
        {
            states = States.BackingOut;
        }
        midpointOfTheBody.enabled = false;
        weakPoint.enabled = true;

        if (AttackKind == 1)
        {

            StateRotation = ROTATION.RotAttack1;
            if (dileyShoot <= 0)
            {
                dileyShoot = auxDileyShoot;
                ThrowBall();
            }
            if (dileyShoot > 0)
            {
                dileyShoot = dileyShoot - Time.deltaTime;
            }
            if (DileyMovRight > 0)
            {
                transform.position = transform.position + transform.right * Time.deltaTime * movHorizontal;
                DileyMovRight = DileyMovRight - Time.deltaTime;
                if (DileyMovRight <= 0)
                {
                    DileyMovLeft = auxDileyMovLeft;
                    DileyMovRight = 0;
                }
            }

            if (DileyMovLeft > 0)
            {
                transform.position = transform.position + transform.right * Time.deltaTime * -movHorizontal;
                DileyMovLeft = DileyMovLeft - Time.deltaTime;
                if (DileyMovLeft <= 0)
                {
                    DileyMovRight = auxDileyMovRight;
                    DileyMovLeft = 0;
                }
            }

        }
        
        if (AttackKind == 2)
        {
            StateRotation = ROTATION.RotAttack2;
            transform.position = transform.position + transform.forward * Time.deltaTime * (PowerImpulseMov*powerAttack);
        }
    }
    public void BackingOut()
    {
        states = States.Swiming;
        id++;
        if (id >= waypoints.Length)
        {
            id = 0;
        }
    }
    public void UpdatePositionPlayer()
    {
        if (Player != null)
        {
            posPlayer = new Vector3(Player.transform.position.x+7, Player.transform.position.y-5, Player.transform.position.z);
        }
    }

}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
