using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReiniciarTodo : MonoBehaviour {

    // Use this for initialization
    private EstructuraDatosAuxiliares estructura;
	void Start () {
		if(EstructuraDatosAuxiliares.estructuraDatosAuxiliares != null)
        {
            estructura = EstructuraDatosAuxiliares.estructuraDatosAuxiliares;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Reiniciar()
    {
        if(estructura != null)
        {
            estructura.DatosJugador.powerUpAumentarVida = false;
            estructura.DatosJugador.powerUpChalecoAntiGolpes = false;
            estructura.DatosJugador.powerUpDobleDanio = false;
            estructura.DatosJugador.blindaje = 0;
            estructura.DatosJugador.cantEnemigosAbatidos = 0;
            estructura.DatosJugador.municionPelotaDanzarina = 0;
            estructura.DatosJugador.municionPelotaDeFuego = 0;
            estructura.DatosJugador.municionPelotaDeHielo = 0;
            estructura.DatosJugador.municionPelotaExplosiva = 0;
            estructura.DatosJugador.municionPelotaFragmentadora = 0;
            estructura.DatosJugador.oportunidades = 10;
            estructura.DatosJugador.puntos = 0;
            estructura.DatosJugador.vida = estructura.DatosJugador.maxVida;
        }
    }
}
