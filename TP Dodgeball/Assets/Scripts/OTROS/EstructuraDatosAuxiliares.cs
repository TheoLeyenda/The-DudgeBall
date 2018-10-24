using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstructuraDatosAuxiliares : MonoBehaviour {

    // Use this for initialization
    private static EstructuraDatosAuxiliares estructuraDatosAuxiliares;
    [HideInInspector]
    public bool Guardar;
    [HideInInspector]
    public bool soloUnaVez= true;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (estructuraDatosAuxiliares == null)
        {
            estructuraDatosAuxiliares = this;
        }
        else if (estructuraDatosAuxiliares != null)
        {
            this.gameObject.SetActive(false);
        }
    }
    public static EstructuraDatosAuxiliares GetEstructuraDatosAuxiliares()
    {
        return estructuraDatosAuxiliares;
    }

    public struct datosJugador
    {
        public int TOPE_MUNICION;// = 500
        public float vida;
        public float maxVida;
        public float blindaje;
        public int tipoPelota;
        public int puntos;
        public int municionPelotaDeHielo;
        public int municionPelotaDeFuego;
        public int municionPelotaFragmentadora;
        public int municionPelotaDanzarina;
        public int municionPelotaExplosiva;
        public bool powerUpAumentarVida;
        public bool powerUpChalecoAntiGolpes;
        public bool powerUpDobleDanio;
        public bool Inmune;
        public bool doblePuntuacion;
        public bool InstaKill;
        public bool activoInstaKill;
        public float contInmune;
        public float contDoblePuntuacion;
        public float contInstaKill;
        public float dileyActivacion;
        public int oportunidades;
    }

    [HideInInspector]
    public datosJugador DatosJugador;

    public void SetDatosJugador(Jugador jugador)
    {
        DatosJugador.TOPE_MUNICION = jugador.GetTOPEMUNICION();
        DatosJugador.vida = jugador.vida;
        DatosJugador.maxVida = jugador.maxVida;
        DatosJugador.blindaje = jugador.blindaje;
        DatosJugador.tipoPelota = jugador.tipoPelota;
        DatosJugador.puntos = jugador.GetPuntos();
        DatosJugador.municionPelotaDeHielo = jugador.GetMunicionPelotaHielo();
        DatosJugador.municionPelotaDeFuego = jugador.GetMunicionPelotaFuego();
        DatosJugador.municionPelotaFragmentadora = jugador.GetMunicionPelotaFragmentadora();
        DatosJugador.municionPelotaDanzarina = jugador.GetMunicionPelotaDanzarina();
        DatosJugador.municionPelotaExplosiva = jugador.GetMunicionPelotaExplosiva();
        DatosJugador.powerUpAumentarVida = jugador.GetPowerUpAumentarVida();
        DatosJugador.powerUpChalecoAntiGolpes = jugador.GetPowerUpChalecoAntiGolpes();
        DatosJugador.powerUpDobleDanio = jugador.GetPowerUpDobleDanio();
        DatosJugador.Inmune = jugador.GetInmune();
        DatosJugador.doblePuntuacion = jugador.GetDoblePuntuacion();
        DatosJugador.InstaKill = jugador.GetInstaKill();
        DatosJugador.activoInstaKill = jugador.GetActivarInstaKill();
        DatosJugador.contInmune = jugador.GetContInmune();
        DatosJugador.contDoblePuntuacion = jugador.GetContDoblePuntuacion();
        DatosJugador.contInstaKill = jugador.GetContInstaKill();
        DatosJugador.dileyActivacion = jugador.GetDileyActivacion();
        DatosJugador.oportunidades = jugador.oportunidades;
    }

    public void SetValoresDelJugador(Jugador jugador)
    {
        jugador.SetTOPEMUNICION(DatosJugador.TOPE_MUNICION);
        jugador.vida = DatosJugador.vida;
        jugador.maxVida = DatosJugador.maxVida;
        jugador.blindaje = DatosJugador.blindaje;
        jugador.tipoPelota = DatosJugador.tipoPelota;
        jugador.SetPuntos(DatosJugador.puntos);
        jugador.SetMunicionPelotaDeHielo(DatosJugador.municionPelotaDeHielo);
        jugador.SetMunicionPelotaDeFuego(DatosJugador.municionPelotaDeFuego);
        jugador.SetMunicionFragmentadora(DatosJugador.municionPelotaFragmentadora);
        jugador.SetMunicionDanzarina(DatosJugador.municionPelotaDanzarina);
        jugador.SetMunicionExplociva(DatosJugador.municionPelotaExplosiva);
        jugador.SetChalecoAntiGolpes(DatosJugador.powerUpChalecoAntiGolpes);
        jugador.SetDobleDanio(DatosJugador.powerUpDobleDanio);
        jugador.SetInmune(DatosJugador.Inmune);
        jugador.SetDoblePuntuacion(DatosJugador.doblePuntuacion);
        jugador.SetInstaKill(DatosJugador.InstaKill);
        jugador.SetActivarInstaKill(DatosJugador.activoInstaKill);
        jugador.SetCountInmune(DatosJugador.contInmune);
        jugador.SetCountDoblePuntiacion(DatosJugador.contDoblePuntuacion);
        jugador.SetCountInstaKill(DatosJugador.contInstaKill);
        jugador.SetDileyActivacion(DatosJugador.dileyActivacion);
        jugador.oportunidades = DatosJugador.oportunidades;
    }

}
