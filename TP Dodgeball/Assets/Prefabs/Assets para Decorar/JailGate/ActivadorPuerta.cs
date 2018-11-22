using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorPuerta : MonoBehaviour {

    // Use this for initialization
    public PuertaRejas puerta;
    public bool cerrar;
    public bool abrir;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (cerrar)
            {
                if (puerta != null)
                {
                    puerta.SetCerrarPuerta(true);
                    puerta.SetAbrirPuerta(false);
                }
            }
            if(abrir)
            {
                if(puerta != null)
                {
                    puerta.SetCerrarPuerta(false);
                    puerta.SetAbrirPuerta(true);
                }
            }
        }
    }
}
