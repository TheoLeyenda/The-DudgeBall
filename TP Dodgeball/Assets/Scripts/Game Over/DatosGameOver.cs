using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatosGameOver : MonoBehaviour {

    // Use this for initialization
    private EstructuraDatosAuxiliares estructuraDatosAuxiliares;
    
    private float puntaje;
    private float oportunidadesRestantes;
    private int EnemigosAbatidos;
    private float vidaRestante;

    public Text textPuntaje;
    public Text textOportunidadesRestantes;
    public Text textEnemigosAbatidos;
    public Text textVidaRestante;

    public 
	void Start () {
		if(EstructuraDatosAuxiliares.estructuraDatosAuxiliares != null)
        {
            estructuraDatosAuxiliares = EstructuraDatosAuxiliares.estructuraDatosAuxiliares;
            puntaje = estructuraDatosAuxiliares.DatosJugador.puntos;
            oportunidadesRestantes = estructuraDatosAuxiliares.DatosJugador.oportunidades;
            EnemigosAbatidos = estructuraDatosAuxiliares.DatosJugador.cantEnemigosAbatidos;
            vidaRestante = estructuraDatosAuxiliares.DatosJugador.vida;
        }
        
        MostrarDatos();

    }

    // Update is called once per frame
    public void MostrarDatos()
    {
        if (textPuntaje != null)
        {
            textPuntaje.text = "Puntaje: " + puntaje;
        }
        if (textOportunidadesRestantes != null)
        {
            textOportunidadesRestantes.text = "Oportunidades Restantes: " + oportunidadesRestantes;
        }
        if (textEnemigosAbatidos != null)
        {
            textEnemigosAbatidos.text = "Enemigos Abatidos:" + EnemigosAbatidos;
        }
        if (textVidaRestante)
        {
            textVidaRestante.text = "Vida Restante: " + vidaRestante; 
        }
    }
}
