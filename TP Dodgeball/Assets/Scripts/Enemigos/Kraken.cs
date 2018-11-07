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
    private float timeEstado;
    //private float auxImpulsoDeAtaque;
    private float efectoFuego;

    public float tiempoDisparando;
    public float dileyDisparo;
    public GameObject[] generadorPelota;
    public Transform[] waypoints;
    public PoolPelota poolPelotasDeTinta;
    public float dileyImpulso;
    public float FuerzaImpulsoMov;
    public BoxCollider puntoDebil;
    public BoxCollider puntoMedioDelCuerpo;
    //public float impulsoDeAtaque;

    void Start () {
        //auxImpulsoDeAtaque = impulsoDeAtaque;
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
        CheckMuerto();
        //CheckEstados();
    }
    public void CheckMuerto()
    {
        if (GetMuerto())
        {
            if (Jugador.GetJugador() != null)
            {
                Jugador.GetJugador().SumarPuntos(250);
            }
            if (!estoyEnPool)
            {
                gameObject.SetActive(false);
            }
            if (estoyEnPool)
            {
                poolObject.Resiclarme();
            }
        }
    }
    /*public void CheckEstados()
    {
        if (timeEstado > 0)
        {
            if (GetEstadoEnemigo() == EstadoEnemigo.bailando)
            {
                SetRotarY(20);
                Rotar();
            }
            if (GetEstadoEnemigo() == EstadoEnemigo.quemado || efectoQuemado.activeSelf)
            {
                efectoFuego = efectoFuego + Time.deltaTime;
                if (efectoFuego >= 1)
                {
                    if (Jugador.GetJugador() != null)
                    {
                        if (Jugador.GetJugador().GetDoblePuntuacion())
                        {
                            Jugador.GetJugador().SumarPuntos(5 * 2);
                        }
                        else
                        {
                            Jugador.GetJugador().SumarPuntos(5);
                        }
                        vida = vida - (GetDanioBolaFuego() + Jugador.GetJugador().GetDanioAdicionalPelotaFuego());
                    }
                    efectoFuego = 0;
                }
                if (GetEstadoEnemigo() == EstadoEnemigo.congelado)
                {
                   FuerzaImpulsoMov = 0;
                }
            }
            timeEstado = timeEstado - Time.deltaTime;
        }
        if (timeEstado <= 0)
        {
            
            if (GetEstadoEnemigo() == EstadoEnemigo.quemado)
            {
                efectoQuemado.SetActive(false);
                SetEstadoEnemigo(EstadoEnemigo.normal);
            }
        }
    }*/
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
                break;
            case (int)ROTACION.RotAtaque:
                SetDatosRotacion(transform.rotation.x +(-90), 0, 0);
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
        if (Jugador.GetJugador() != null)
        {
            transform.LookAt(new Vector3(Jugador.GetJugador().transform.position.x, transform.position.y+90,Jugador.GetJugador().transform.position.z));
        }
    }
    public void Nadar()
    {
        EstadoRotacion = ROTACION.RotNormal;
        puntoMedioDelCuerpo.enabled = true;
        puntoDebil.enabled = false;
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
        puntoMedioDelCuerpo.enabled = false;
        puntoDebil.enabled = true;
        if (tipoAtaque == 1)
        { 
            
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
   
}
