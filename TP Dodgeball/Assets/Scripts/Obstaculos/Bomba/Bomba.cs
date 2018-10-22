using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour {

    // Use this for initialization
    public AudioSource sonido;
    public AudioClip sonidoExplocion;
    public GameObject bomba;
    public GameObject RangoExplocion;
    public GameObject efectoExplocion;
    public float TiempoExplocion;
    private bool ActivarBomba;
    private bool ActivarDiley;
    private float dileyDesaparicion = 1;
    private bool unaVez = true;
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (ActivarBomba)
        {
            TiempoExplocion = TiempoExplocion - Time.deltaTime;
            if (TiempoExplocion <= 0)
            {
                RangoExplocion.SetActive(true);
                efectoExplocion.SetActive(true);
                ActivarDiley = true;
                ActivarBomba = false;
                if (sonido != null && sonidoExplocion != null && unaVez)
                {
                    sonido.PlayOneShot(sonidoExplocion);
                    unaVez = false;
                }
            }
        }
        if(ActivarDiley)
        {
            dileyDesaparicion = dileyDesaparicion - Time.deltaTime;
            if(dileyDesaparicion <= 0)
            {
                bomba.SetActive(false);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(Jugador.GetJugador() != null)
            {
                ActivarBomba = true;
            }
        }
    }
}
