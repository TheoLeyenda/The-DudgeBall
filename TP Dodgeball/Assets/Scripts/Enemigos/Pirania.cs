using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
public class Pirania : Enemigo {

    // Use this for initialization
    public enum States
    {
        Swiming = 0,
        Attack,
        Still,
    }
    private int id = 0;

    public States states;
    public float AtackSpeed;
    public float MovementSpeed;
    public float damage;
    public bool ActivePiranha;
    private Vector3 initialPoint;
    private Quaternion initialRotation;
    public Transform[] waypoints;
    public Transform waypointPlayerAndroid;
    public Transform waypointPlayerWindows;
    private Jugador instancePlayer;
    void Start () {
        instancePlayer = Jugador.InstancePlayer;
        initialPoint = transform.position;
        initialRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
      
        if (ActivePiranha)
        {
            UpdateStates();
           
        }
        if (GetDead())
        {
            if (!i_AmInPool)
            {
                gameObject.SetActive(false);
            }
        }
    }
    public void SetInitialPoint(Vector3 Point)
    {
        initialPoint = Point;
    }
    public void SetInitialRotation(Quaternion rotInitial)
    {
        initialRotation = rotInitial;
    }
    public Vector3 GetInitialPoint()
    {
        return initialPoint;
    }
    public Quaternion GetInitialRotation()
    {
        return initialRotation;
    }
    public void UpdateStates()
    {

        switch ((int)states)
        {
            case (int)States.Swiming:
                Swim();
                break;
            case (int)States.Attack:
                Attack();
                break;
        }
    }
    public void SetWaypoint(Transform waypointPosition)
    {
        if(Jugador.InstancePlayer != null)
        {
            if(instancePlayer.playerAndroid)
            {
                waypointPlayerAndroid = waypointPosition;
            }
            if(instancePlayer.playerWindows)
            {
                waypointPlayerWindows = waypointPosition;
            }
        }
    }
    public void Attack()
    {
        if (Jugador.GetPlayer() != null)
        {
            if (Jugador.GetPlayer().playerWindows && Jugador.GetPlayer().playerAndroid == false)
            {
                if (waypointPlayerWindows != null)
                {
                    Vector3 target = waypointPlayerWindows.position;
                    transform.LookAt(target);
                    if (transform.position != target)
                    {
                        transform.position = transform.position + transform.forward * Time.deltaTime * AtackSpeed;
                    }
                }
            }
            if (Jugador.GetPlayer().playerAndroid && Jugador.GetPlayer().playerWindows == false)
            {
                if(waypointPlayerAndroid != null)
                {
                    Vector3 target = waypointPlayerAndroid.position;
                    transform.LookAt(target);
                    if (transform.position != target)
                    {
                        transform.position = transform.position + transform.forward * Time.deltaTime * AtackSpeed;
                    }
                }
            }
        }
    }
    public void Swim()
    {
        if (waypoints.Length > 0)
        {

            if (waypoints[id] != null)
            {
                Vector3 target = waypoints[id].position;
                transform.LookAt(target);
                transform.position = transform.position + transform.forward * Time.deltaTime * MovementSpeed;
                Vector3 diff = target - this.transform.position;

                if (diff.magnitude < 0.3f)
                {
                    id++;
                    if(id >= waypoints.Length)
                    {
                        id = 0;
                    }
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
        if(other.tag == "Player")
        {
            if (Jugador.GetPlayer() != null)
            {
                Jugador.GetPlayer().life = Jugador.GetPlayer().life - damage;
            }
        }
        if (other.gameObject.tag == "PelotaComun")
        {
            if (Jugador.GetPlayer() != null)
            {
                life = life - (GetDamageCommonBall() + Jugador.GetPlayer().GetAdditionalDamageCommonBall());
                IsDead();
                if (Jugador.GetPlayer().GetDoblePoints())
                {
                    Jugador.GetPlayer().AddScore(10 * 2);
                }
                else
                {
                    Jugador.GetPlayer().AddScore(10);
                }
            }
        }
        if (other.gameObject.tag == "PelotaDeHielo")
        {
            if (Jugador.GetPlayer() != null)
            {
                if (Jugador.GetPlayer().GetDoblePoints())
                {
                    Jugador.GetPlayer().AddScore(10 * 2);
                }
                else
                {
                    Jugador.GetPlayer().AddScore(10);
                }
                life = life - (GetDamageIceBall() + Jugador.GetPlayer().GetAdditionalDamageIceBall());
            }
            IsDead();
        }
        if (other.gameObject.tag == "MiniPelota")
        {
            if (Jugador.GetPlayer() != null)
            {
                if (Jugador.GetPlayer().GetDoblePoints())
                {
                    Jugador.GetPlayer().AddScore(10 * 2);
                }
                else
                {
                    Jugador.GetPlayer().AddScore(10);
                }
                life = life - (GetDamageMiniBall() + Jugador.GetPlayer().GetAditionalDamageMiniBalls());
                IsDead();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "AntiPirania")
        {
            gameObject.SetActive(false);
        }
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
