using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarArma : MonoBehaviour {

    // Use this for initialization
    public Material Negro;
    public Material Hielo;
    public Material Normal;
    public Material Fragmentadora;
    public Material Danzarina;
    public Material Fuego;
    public GameObject GeneradorExplocivos;
    public GameObject GeneradorPelotaComun;
    public bool JugadorWindows;

    private Renderer ren;

	void Start () {

        gameObject.GetComponent<Renderer>().material = Normal;
	}
	
	// Update is called once per frame
	void Update () {
        if (JugadorWindows)
        {
            CheckArma();
        }
        if(Jugador.GetJugador().tipoPelota == 1 && Normal != null)
        {
            GeneradorExplocivos.SetActive(false);
            GeneradorPelotaComun.SetActive(true);
            gameObject.GetComponent<Renderer>().material = Normal;
        }
        if(Jugador.GetJugador().tipoPelota == 2 && Hielo != null)
        {
            GeneradorExplocivos.SetActive(false);
            GeneradorPelotaComun.SetActive(true);
            gameObject.GetComponent<Renderer>().material = Hielo;
        }
        if(Jugador.GetJugador().tipoPelota == 3 && Fragmentadora != null)
        {
            GeneradorExplocivos.SetActive(false);
            GeneradorPelotaComun.SetActive(true);
            gameObject.GetComponent<Renderer>().material = Fragmentadora;
        }
        if(Jugador.GetJugador().tipoPelota == 4 && Danzarina != null)
        {
            GeneradorExplocivos.SetActive(false);
            GeneradorPelotaComun.SetActive(true);
            gameObject.GetComponent<Renderer>().material = Danzarina;
        }
        if (Jugador.GetJugador().tipoPelota == 5 && Fuego != null)
        {
            GeneradorExplocivos.SetActive(false);
            GeneradorPelotaComun.SetActive(true);
            gameObject.GetComponent<Renderer>().material = Fuego;
        }
        if(Jugador.GetJugador().tipoPelota == 6 && GeneradorExplocivos != null)
        {
            GeneradorPelotaComun.SetActive(false);
            gameObject.GetComponent<Renderer>().material = Negro;
            GeneradorExplocivos.SetActive(true);
            
        }
    }
    public void CheckArma()
    {
        if(Input.GetKey(KeyCode.Alpha1))
        {
            Jugador.GetJugador().tipoPelota = 1;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            Jugador.GetJugador().tipoPelota = 2;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            Jugador.GetJugador().tipoPelota = 3;
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            Jugador.GetJugador().tipoPelota = 4;
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            Jugador.GetJugador().tipoPelota = 5;
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            Jugador.GetJugador().tipoPelota = 6;
        }
    }
}
