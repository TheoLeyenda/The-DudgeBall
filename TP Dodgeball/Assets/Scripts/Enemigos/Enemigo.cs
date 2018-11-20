using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EstadoEnemigo
{
    congelado,
    quemado,
    normal,
    bailando,
}
public class Enemigo : MonoBehaviour {

    // Use this for initialization
    public float vida;
    public float maxVida;
    public GameObject BarraVida;
    public GameObject marcoBarra;
    private bool muerto;
    private EstadoEnemigo estadoEnemigo;
    private float danioBolaComun = 30;
    private float danioBolaDeHielo = 5;
    private float danioMiniBolas = 18;
    private float danioBolaDanzarina = 0;
    private float danioBolaDeFuego = 10;
    private float danioBolaExplociva = 50;
    private float rotarX = 0;
    private float rotarY = 0;
    private float rotarZ = 0;
    public GameObject efectoQuemado;
    public GameObject efectoCongelado;
    public GameObject efectoMusica;
    private bool tocandoSuelo;
    private bool esquivar;
    public bool estoyEnPool;
    void Start() {
        muerto = false;
        estadoEnemigo = EstadoEnemigo.normal;
        
    }

    // Update is called once per frame
    void Update() {
        if (Jugador.GetJugador() != null)
        {
            if (Jugador.GetJugador().GetPowerUpDobleDanio())
            {
                Jugador.GetJugador().SetDanioAdicionalPelotaComun(danioBolaComun);
                Jugador.GetJugador().SetDanioAdicionalMiniPelota(danioMiniBolas);
                Jugador.GetJugador().SetDanioAdicionalPelotaHielo(danioBolaDeHielo);
                Jugador.GetJugador().SetDanioAdicionalPelotaFuego(danioBolaDeFuego);
                Jugador.GetJugador().SetDanioAdicionalPelotaExplosiva(danioBolaExplociva);
                Jugador.GetJugador().SetPowerUpDobleDanio(false);
            }
           
        }
        
    }
    public void Esquivar(int lado, float velocidad, float velAgregado)//tiempo = 1 y tiempoFinal = 2
    {
        
        if (lado == 1)
        {

            transform.position = transform.position + transform.right * Time.deltaTime * (velocidad+ velAgregado);
            transform.position += transform.forward * Time.deltaTime *velocidad;
        }
        if(lado == 2)
        {
           
            transform.position = transform.position - transform.right * Time.deltaTime * (velocidad + velAgregado);
            transform.position += transform.forward * Time.deltaTime * velocidad;
        }
    }
    public void updateHP()
    {
        if (BarraVida != null)
        {
            float z = (float)vida / (float)maxVida;
            Vector3 ScaleBar = new Vector3(1, 1, z);
            BarraVida.transform.localScale = ScaleBar;
        }
    }
    public void EstaMuerto()
    {
        if (vida <= 0)
        {
            muerto = true;
        }
    }
    public void SetMuerto(bool _muerto)
    {
        muerto = _muerto;
    }
    public bool GetMuerto()
    {
        return muerto;
    }
    public void SetEstadoEnemigo(EstadoEnemigo estado)
    {
        estadoEnemigo = estado;
    }
    public EstadoEnemigo GetEstadoEnemigo()
    {
        return estadoEnemigo;
    }
    public void SetRotarX(float rotX)
    {
        rotarX = rotX;
    }
    public void SetRotarY(float rotY)
    {
        rotarY = rotY;
    }
    public void SetRotarZ(float rotZ)
    {
        rotarZ = rotZ;
    }
    public float GetRotarX()
    {
        return rotarX;
    }
    public float GetRotarY()
    {
        return rotarY;
    }
    public float GetRotarZ()
    {
        return rotarZ;
    }
    public void Rotar()
    {
        transform.Rotate(rotarX, rotarY, rotarZ);
    }
    public void SetTocandoSuelo(bool tocando)
    {
        tocandoSuelo = tocando;
    }
    public bool GetTocandoSuelo()
    {
        return tocandoSuelo;
    }
    public void SetDanioBolaComun(float danioNuevo)
    {
        danioBolaComun = danioNuevo;
    }
    public void SetDanioBolaHielo(float danioNuevo)
    {
        danioBolaDeHielo = danioNuevo;
    }
    public void SetDanioMiniBola(float danioNuevo)
    {
        danioMiniBolas = danioNuevo;
    }
    public void SetDanioBolaDanzarina(float danioNuevo)
    {
        danioBolaDanzarina = danioNuevo;
    }
    public void SetDanioBolaFuego(float danioNuevo)
    {
        danioBolaDeFuego = danioNuevo;
    }
    public void SetDanioBolaExplociva(float danioNuevo)
    {
        danioBolaExplociva = danioNuevo;
    }

    public float GetDanioBolaComun()
    {
        return danioBolaComun;
    }
    public float GetDanioBolaHielo()
    {
        return danioBolaDeHielo;
    }
    public float GetDanioMiniBola()
    {
        return danioMiniBolas;
    }
    public float GetDanioBolaDanzarina()
    {
        return danioBolaDanzarina;
    }
    public float GetDanioBolaFuego()
    {
        return danioBolaDeFuego;
    }
    public float GetDanioBolaExplociva()
    {
        return danioBolaExplociva;
    }
    public void SetEsquivar(bool _esquivar)
    {
        esquivar = _esquivar;
    }
    public bool GetEsquivar()
    {
        return esquivar;
    }

}
