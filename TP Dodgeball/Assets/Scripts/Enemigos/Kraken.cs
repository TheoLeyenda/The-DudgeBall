﻿using System.Collections;
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
        RotAtaque1,
        RotAtaque2,
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
    private float auxDileyMovIzquierda;
    private float auxDileyMovDerecha;

    public float danioAumentadoPelotaComun;
    public float danioAumentadoPelotaFuego;
    public float movHorizontal;
    public float DileyMovDerecha;
    public float DileyMovIzquierda;
    public float tiempoDisparando;
    public float dileyDisparo;
    public GameObject[] generadorPelota;
    public Transform[] waypoints;
    public PoolPelota poolPelotasDeTinta;
    public float dileyImpulso;
    public float FuerzaImpulsoMov;
    public BoxCollider puntoDebil;
    public BoxCollider puntoMedioDelCuerpo;
    public float potenciaAtaque;
    //public float impulsoDeAtaque;

    void Start () {
        auxDileyMovDerecha = DileyMovDerecha;
        auxDileyMovIzquierda = DileyMovIzquierda;
        DileyMovDerecha = 0;
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
        if (Time.timeScale > 0)
        {
            if (tiempoDisparando <= 0)
            {
                tipoAtaque = 2;
            }
            updateHP();
            UpdateStates();
            UpdateRotacion();
            UpdatePositionPlayer();
            CheckMuerto();
        }
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
            case (int)ROTACION.RotAtaque1:
                SetDatosRotacion();
                break;
            case (int)ROTACION.RotAtaque2:
                SetDatosRotacion();
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
    public void SetDatosRotacion()
    {

        if (Jugador.GetJugador() != null)
        {
            if (tipoAtaque == 1)
            {
                transform.LookAt(new Vector3(Jugador.GetJugador().transform.position.x, transform.position.y + 90, Jugador.GetJugador().transform.position.z));
            }
            if (tipoAtaque == 2)
            {
                transform.LookAt(posJugador);
                
            }
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
        if (transform.position.y <= Jugador.GetJugador().transform.position.y)
        {
            estados = States.Retirse;
        }
        puntoMedioDelCuerpo.enabled = false;
        puntoDebil.enabled = true;

        if (tipoAtaque == 1)
        {

            EstadoRotacion = ROTACION.RotAtaque1;
            if (dileyDisparo <= 0)
            {
                dileyDisparo = auxDileyDisparo;
                TirarBola();
            }
            if (dileyDisparo > 0)
            {
                dileyDisparo = dileyDisparo - Time.deltaTime;
            }
            if (DileyMovDerecha > 0)
            {
                transform.position = transform.position + transform.right * Time.deltaTime * movHorizontal;
                DileyMovDerecha = DileyMovDerecha - Time.deltaTime;
                if (DileyMovDerecha <= 0)
                {
                    DileyMovIzquierda = auxDileyMovIzquierda;
                    DileyMovDerecha = 0;
                }
            }

            if (DileyMovIzquierda > 0)
            {
                transform.position = transform.position + transform.right * Time.deltaTime * -movHorizontal;
                DileyMovIzquierda = DileyMovIzquierda - Time.deltaTime;
                if (DileyMovIzquierda <= 0)
                {
                    DileyMovDerecha = auxDileyMovDerecha;
                    DileyMovIzquierda = 0;
                }
            }

        }
        
        if (tipoAtaque == 2)
        {
            EstadoRotacion = ROTACION.RotAtaque2;
            transform.position = transform.position + transform.forward * Time.deltaTime * (FuerzaImpulsoMov*potenciaAtaque);
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
            posJugador = new Vector3(Jugador.GetJugador().transform.position.x+7,Jugador.GetJugador().transform.position.y-5, Jugador.GetJugador().transform.position.z);
        }
    }

}