using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiburon : Enemigo {

    // Use this for initialization
    public enum States
    {
        Nadando = 0,
        Seguir,
        Atacar,
        Retirse,
        Count
    }

    public enum Events
    {
        EnVista = 0,
        FueraDeVista,
        EnRangoDeAtaque,
        FueraDelRangoDeAtaque,
        LuegoDeAtacar,
        VolviendoQuieto,
        Count
    }
    public float danio;
    public float potenciaAtaque;
    public float reducirAtaque;
    public float velMovimiento;
    public Transform[] waypoints;

    private States estados;
    private Events eventos;
    private int id = 0;
    private Rigidbody rig;
    private float velAtaque;
    private Vector3 posJugador;
    //private FSM fsm;

    void Start ()
    {
        rig = GetComponent<Rigidbody>();
        estados = States.Nadando;
        if(potenciaAtaque <= 0)
        {
            potenciaAtaque = 1;
        }
        velAtaque = velMovimiento * potenciaAtaque;
        velAtaque = velAtaque - reducirAtaque;
        /*fsm = new FSM((int)States.Count, (int)Events.Count, (int)States.Nadando);

        fsm.SetRelation((int)States.Nadando, (int)Events.EnVista, (int)States.Seguir);
        fsm.SetRelation((int)States.Seguir, (int)Events.EnRangoDeAtaque, (int)States.Atacar);
        fsm.SetRelation((int)States.Seguir, (int)Events.FueraDeVista, (int)States.Nadando);
        fsm.SetRelation((int)States.Atacar, (int)Events.FueraDelRangoDeAtaque, (int)States.Seguir);
        fsm.SetRelation((int)States.Atacar, (int)Events.LuegoDeAtacar, (int)States.Retirse);
        fsm.SetRelation((int)States.Retirse, (int)Events.VolviendoQuieto, (int)States.Nadando);*/
    }
	
	// Update is called once per frame
	void Update () {
        UpdateStates();
        updateHP();
        if (estados != States.Atacar)
        {
            UpdatePositionPlayer();
        }
    }
    public void UpdateStates()
    {
        switch ((int)estados)
        {
            case (int)States.Nadando:
                Nadar();
                break;
            case (int)States.Seguir:
                Seguir();
                break;
            case (int)States.Atacar:
                Atacar();
                break;
            case (int)States.Retirse:
                Retirarse();
                break;
        }
    }
    public void UpdatePositionPlayer()
    {
        if (Jugador.GetJugador() != null)
        {
            posJugador = Jugador.GetJugador().transform.position;
        }
    }
    public void Nadar()
    {
        if (waypoints.Length > 0)
        {
            if (waypoints[id] != null)
            {
                Vector3 target = waypoints[id].position;
                transform.LookAt(target);
                transform.position = transform.position + transform.forward * Time.deltaTime * velMovimiento;

                //Vector3 diff = target - this.transform.position;

                //if (diff.magnitude < 0.5f)
                //{
                    //id++;
                    //if (id >= waypoints.Length)
                    //{
                        //id = 0;
                    //}
                //}
            }
        }
        else
        {
            id = 0;
        }
    }
    public void Seguir()
    {
        if (Jugador.GetJugador() != null)
        {
            transform.LookAt(Jugador.GetJugador().transform.position);
            transform.position = transform.position + transform.forward * Time.deltaTime * velMovimiento;
        }
    }
    public void Atacar()
    {
        if(Jugador.GetJugador() != null)
        {
            transform.LookAt(posJugador);
            transform.position = transform.position + transform.forward * Time.deltaTime * velAtaque;
            if (transform.position.y < Jugador.GetJugador().transform.position.y)
            {
                estados = States.Retirse;
            }
        }
        
    }
    public void Retirarse()
    {
        estados = States.Nadando;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            estados = States.Retirse;
            id++;
            if (id >= waypoints.Length)
            {
                id = 0;
            }
            if(Jugador.GetJugador() != null)
            {
                Jugador.GetJugador().vida = Jugador.GetJugador().vida - danio;
            }
        }
        if (other.tag == "WaypointRandom")
        {
            float random = Random.Range(1, 100);
            if (random > 90)
            {
                estados = States.Atacar;
            }
            else
            {
                id++;
                if (id >= waypoints.Length)
                {
                    id = 0;
                }
            }
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
    // HACER QUE CUANDO LE DE EL GOLPE AL JUGADOR LO CAMBIE A ESTADO NADANDO Y DESACTIVE EL RANGO DE ATAQUE Y SEGUIMIENTO (O SOLO DE ATAQUE)
    // HASTA QUE EL TIBURON LLEGUE AL PROXIMO WAYPOINT

    //FIJARSE DE HACER QUE EL TIBURON ATAQUE NO SOLO CUANDO ESTA EN RANGO SINO TAMBIEN EN CUALQUIER MOMENTO DE FORMA ALEATOREA.

    // PARA HACER QUE EL TIBURON ENTRE EN ESTADO ATAQUE DE FORMA ALEATOREA: cuando el tiburon haga triggered/collisione con un waypoint
    //tirar un random entre 0 y 100. Si en el random aparece como resultado el numero que yo defino para que el tiburon ataque, el tiburon pasa a estado de ataque.

    //ACTIVAR Y DESACTIVAR LOS RANGOS DE VISION Y ATAQUE SEGUN EL CASO(PARA ESTO USAR BOLEANOS).

}
