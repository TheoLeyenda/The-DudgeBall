using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPuertaSegunVidaEnemigo : MonoBehaviour {

    // Use this for initialization
    public PuertaRejas puertaRejas;
    public Enemigo[] enemigos;
    private int cantEnemigosMuertos;
    private bool abrirPuerta;
	void Start () {
        abrirPuerta = false;
	}
	
	// Update is called once per frame
	void Update () {
        CheckEnemigosMuertos();
        CheckAbrirPuerta();
	}
    public void CheckAbrirPuerta()
    {
        if(abrirPuerta)
        {
            if(puertaRejas != null)
            {
                puertaRejas.SetAbrirPuerta(true);
            }
        }
    }
    public void CheckEnemigosMuertos()
    {
        for(int i = 0; i< enemigos.Length; i++)
        {
            if (enemigos[i] != null)
            {
                if (enemigos[i].vida <= 0)
                {
                    cantEnemigosMuertos++;
                }
            }
        }
        if(cantEnemigosMuertos >= enemigos.Length)
        {
            abrirPuerta = true;
        }
        else
        {
            cantEnemigosMuertos = 0;
        }
    }
}
