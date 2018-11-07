using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraken : Enemigo {

    // Use this for initialization
    private States estados;
    private PoolObject poolObject;
    private Rigidbody rig;
    private Vector3 posJugador;
    private int id = 0;
    private float auxFuerzaImpulsoMov;
    private float auxDileyImpulso;
    private int tipoAtaque;
    private float auxDileyDisparo;
    private float auxTiempoDisparando;

    public float tiempoDisparando;
    public float dileyDisparo;
    public GameObject[] generadorPelota;
    public PoolPelota poolPelotasDeTinta;
    public float dileyImpulso;
    public float FuerzaImpulsoMov;
    public Transform[] waypoints;

    public enum States
    {
        
        Nadando = 0,
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

    void Start () {
        auxTiempoDisparando = tiempoDisparando;
        tiempoDisparando = 0;
        auxDileyDisparo = dileyDisparo;
        auxDileyImpulso = dileyImpulso;
        auxFuerzaImpulsoMov = FuerzaImpulsoMov;
        id = 0;
        estados = States.Nadando;
        rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        updateHP();
        UpdateStates();
        
    }
    public void TirarBola()
    {
        
        for (int i = 0; i < generadorPelota.Length; i++)
        {
            if (tiempoDisparando > 0)
            {
                tiempoDisparando = tiempoDisparando - Time.deltaTime;
                if (generadorPelota[i].activeSelf == true)
                {
                    GameObject go = poolPelotasDeTinta.GetObject();
                    PelotaEnemigo pelota = go.GetComponent<PelotaEnemigo>();
                    go.transform.position = generadorPelota[i].transform.position;
                    go.transform.rotation = generadorPelota[i].transform.rotation;
                    pelota.Disparar();
                }
            }
        }
    }
    public void ActivarDisparo()
    {
        tiempoDisparando = auxTiempoDisparando;
    }
    public void UpdateStates()
    {
        switch ((int)estados)
        {
            case (int)States.Nadando:
                Nadar();
                break;
            case (int)States.Atacar:
                Atacar(tipoAtaque);
                break;
            case (int)States.Retirse:
                Retirarse();
                break;
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
                transform.position = transform.position + transform.forward * Time.deltaTime * FuerzaImpulsoMov;
                Vector3 diff = target - this.transform.position;

                if (diff.magnitude < 0.3f)
                {
                    float random = Random.Range(1, 100);
                    if (random < 75)
                    {
                        id++;
                        FuerzaImpulsoMov = 2;
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
                            tipoAtaque = 1;
                            ActivarDisparo();
                            estados = States.Atacar;
                        }
                        if(randomTipoAtaque >=60)
                        {
                            tipoAtaque = 2;
                            estados = States.Atacar;
                        }
                            
                    }
                    
                }
                if (FuerzaImpulsoMov > 2)
                {
                    FuerzaImpulsoMov = FuerzaImpulsoMov - (Time.deltaTime* 5.2f);
                }
                if(FuerzaImpulsoMov <= 2)
                {
                    if(dileyImpulso > 2)
                    {
                        dileyImpulso = dileyImpulso - (Time.deltaTime* 5.2f);
                    }
                    if(dileyImpulso <= 2)
                    {
                        dileyImpulso = auxDileyImpulso;
                        FuerzaImpulsoMov = auxFuerzaImpulsoMov;
                    }
                }
            }
        }
    }
    public void Atacar(int tipoAtaque)
    {
        Debug.Log("Tipo de ataque:" + tipoAtaque);
        if(tipoAtaque == 1)
        {
            if (dileyDisparo <= 0)
            {
                dileyDisparo = auxDileyDisparo;
                TirarBola();
            }
            if (dileyDisparo > 0)
            {
                dileyDisparo = dileyDisparo - Time.deltaTime;
            }
        }
    }
    public void Retirarse()
    {

    }
    public void UpdatePositionPlayer()
    {
        if (Jugador.GetJugador() != null)
        {
            posJugador = Jugador.GetJugador().transform.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Waypoint")
        {
            id++;
            if (id >= waypoints.Length)
            {
                id = 0;
            }
        }
    }
}
