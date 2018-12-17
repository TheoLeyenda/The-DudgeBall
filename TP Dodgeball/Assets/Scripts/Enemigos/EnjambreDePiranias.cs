using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
public class EnjambreDePiranias : MonoBehaviour {

    // Use this for initialization

    public enum States
    {
        swiming = 0,
        follow,
        still,
    }

    public States states;
    private int id = 0;

    private Jugador player;
    public bool backHomePoint;
    public Transform[] waypoints;
    public Pirania[] piranha;
    private Vector3 initialPoint;
    private Quaternion initialRotation;
    public float movementSpeed;
    private bool selfDestruction;
    void Start()
    {
        if (Jugador.InstancePlayer != null)
        {
            player = Jugador.InstancePlayer;
        }
        initialPoint = transform.position;
        initialRotation = transform.rotation;
        if (piranha.Length > 0)
        {
            for (int i = 0; i < piranha.Length; i++)
            {
                if (piranha[i] != null)
                {
                    piranha[i].ActivePiranha = false;
                }
            }
            selfDestruction = false;
        }
    }
    void Update()
    {
        if (backHomePoint)
        {
            if (player != null)
            {
                if (player.life <= 0)
                {
                    gameObject.SetActive(false);
                    ReturnInitialPoint();
                }
            }
        }
        if (!selfDestruction)
        {
            UpdateStates();
        }
    }
    public void UpdateStates()
    {
        
        switch ((int)states)
        {
            case (int)States.swiming:
                Swiming();
                break;
            case (int)States.follow:
                Follow();
                break;
        }
    }

    public void Swiming()
    {
        if (waypoints.Length > 0)
        {
            
            if (waypoints[id] != null)
            {
                Vector3 target = waypoints[id].position;
                transform.LookAt(target);
                transform.position = transform.position + transform.forward * Time.deltaTime * movementSpeed;
                Vector3 diff = target - this.transform.position;

                if (diff.magnitude < 0.3f)
                {
                    id++;
                    if (id >= waypoints.Length)
                    {
                        for (int i = 0; i < piranha.Length; i++)
                        {
                            piranha[i].ActivePiranha = true;
                        }
                        selfDestruction = true;
                    }
                }
            }
        }
    }
    public void ReturnInitialPoint()
    {
        transform.position = initialPoint;
        states = States.swiming;
        selfDestruction = false;
        id = 0;
        transform.rotation = initialRotation;
        for(int i = 0; i< piranha.Length; i++)
        {
            piranha[i].transform.position = piranha[i].GetInitialPoint();
            piranha[i].transform.rotation = piranha[i].GetInitialRotation();
            piranha[i].ActivePiranha = false;
            
        }
    }
    public void Follow()
    {
        if (Jugador.GetPlayer() != null)
        {
            Vector3 target = player.transform.position;
            transform.LookAt(target);
            transform.position = transform.position + transform.forward * Time.deltaTime * movementSpeed;
        }
    }
    
	// Update is called once per frame
	
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
