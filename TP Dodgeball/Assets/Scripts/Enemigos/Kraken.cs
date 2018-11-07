using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraken : Enemigo {

    public struct DatosRotacion
    {
        public float rotacionX;
        public float rotacionY;
        public float rotacionZ;
    }
    public enum ROTACION
    {
        RotNormal = 0,
        RotAtaque,
        Count
    }
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
    private DatosRotacion datRotacion;
    private ROTACION EstadoRotacion; 

    public float tiempoDisparando;
    public float dileyDisparo;
    public GameObject[] generadorPelota;
    public PoolPelota poolPelotasDeTinta;
    public float dileyImpulso;
    public float FuerzaImpulsoMov;
    public Transform[] waypoints;

    void Start () {

        auxTiempoDisparando = tiempoDisparando;
        tiempoDisparando = 0;
        auxDileyDisparo = dileyDisparo;
        auxDileyImpulso = dileyImpulso;
        auxFuerzaImpulsoMov = FuerzaImpulsoMov;
        id = 0;
        //estados = States.Nadando;
        EstadoRotacion = ROTACION.RotNormal;
        rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        updateHP();
        UpdateStates();
        UpdateRotacion();
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
    public void UpdateRotacion()
    {
        switch((int)EstadoRotacion)
        {
            case (int)ROTACION.RotNormal:
                SetDatosRotacion(0, 90, 0);
                break;
            case (int)ROTACION.RotAtaque:
                SetDatosRotacion(-90, 90, 0);
                break;
        }
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
    public void SetDatosRotacion(float x, float y, float z)
    {
        datRotacion.rotacionX = x;
        datRotacion.rotacionY = y;
        datRotacion.rotacionZ = z;
        gameObject.transform.rotation = new Quaternion(x, y, z,Quaternion.identity.w);
    }
    public void Nadar()
    {
        EstadoRotacion = ROTACION.RotNormal;
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
        //FALTA HACER QUE EL KRAKEN VAYA HACIA AL JUGADOR Y LE DE UNA OSTIA QUE LO DEJE AL OTRO LADO DEL MAPA(QUE LE APLIQUE UNA FUERZA AL JUGADOR QUE LO
        //SAQUE VOLANTO), LUEGO DE ESTO HACER QUE EL KRAKEN PASE AL ESTADO  RETIRARSE
        
        if(tipoAtaque == 1)
        {
            EstadoRotacion = ROTACION.RotAtaque;
            if (Jugador.GetJugador() != null)
            {
                transform.LookAt(Jugador.GetJugador().transform.position);
            }
            EstadoRotacion = ROTACION.RotAtaque;
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
        estados = States.Nadando;
        id++;
        if (id >= waypoints.Length)
        {
            id = 0;
        }
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
