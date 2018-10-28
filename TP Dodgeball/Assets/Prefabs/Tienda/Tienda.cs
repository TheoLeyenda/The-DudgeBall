using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tienda : MonoBehaviour
{
    public GameObject boton;
    public Text texto;
    public Text textoPrecioHieloAndroid;
    public Text textoPrecioHieloWindoiws;
    public Text textoPrecioFuegoAndroid;
    public Text textoPrecioFuegoWindows;
    public Text textoPrecioDanzarinaAndroid;
    public Text textoPrecioDanzarinaWindows;
    public Text textoPrecioFragmentadoraAndroid;
    public Text textoPrecioFragmentadoraWindows;
    public Text textoPrecioExplocivoAndroid;
    public Text textoPrecioExplocivoWindows;
    public Text textoPrecioBlindajeAndroid;
    public Text textoPrecioBlindajeWindows;
    public Text textoPrecioVidaAndroid;
    public Text textoPrecioVidaWindows;
    public Text textoPrecioDobleDanioAndroid;
    public Text textoPrecioDobleDanioWindows;
    public int PrecioMunicionHielo;
    public int PrecioMunicionFuego;
    public int PrecioMunicionDanzarina;
    public int PrecioMunicionFragmentadora;
    public int PrecioMunicionExplociva;
    public int PrecioBlindaje;
    public int PrecioDanioDoble;
    public int PrecioVida;
    public GameObject camvasTiendaWindows;
    public GameObject camvasTiendaAndroid;
    private bool tiendaAbierta;
    public GameObject menuArmasAndroid;
    // Use this for initialization
    void Start()
    {
        tiendaAbierta = false;
        if (camvasTiendaAndroid != null && camvasTiendaWindows != null)
        {
            camvasTiendaWindows.SetActive(false);
            camvasTiendaAndroid.SetActive(false);
        }
        if (textoPrecioVidaAndroid != null
            && textoPrecioHieloAndroid != null
            && textoPrecioFuegoAndroid != null
            && textoPrecioFragmentadoraAndroid != null
            && textoPrecioExplocivoAndroid != null
            && textoPrecioDobleDanioAndroid != null
            && textoPrecioDanzarinaAndroid != null
            && textoPrecioBlindajeAndroid != null)
        {
            textoPrecioVidaAndroid.text = PrecioVida + " $";
            textoPrecioHieloAndroid.text = PrecioMunicionHielo + " $";
            textoPrecioFuegoAndroid.text = PrecioMunicionFuego + " $";
            textoPrecioFragmentadoraAndroid.text = PrecioMunicionFragmentadora + " $";
            textoPrecioExplocivoAndroid.text = PrecioMunicionExplociva + " $";
            textoPrecioDobleDanioAndroid.text = PrecioDanioDoble + " $";
            textoPrecioDanzarinaAndroid.text = PrecioMunicionDanzarina + " $";
            textoPrecioBlindajeAndroid.text = PrecioBlindaje + " $";
        }
        if (textoPrecioHieloWindoiws != null
            && textoPrecioFuegoWindows != null
            && textoPrecioDanzarinaWindows != null
            && textoPrecioFragmentadoraWindows != null
            && textoPrecioExplocivoWindows != null
            && textoPrecioBlindajeWindows != null
            && textoPrecioVidaWindows != null
            && textoPrecioDobleDanioWindows != null)
        {
            textoPrecioVidaWindows.text = PrecioVida + " $";
            textoPrecioHieloWindoiws.text = PrecioMunicionHielo + " $";
            textoPrecioFuegoWindows.text = PrecioMunicionFuego + " $";
            textoPrecioFragmentadoraWindows.text = PrecioMunicionFragmentadora + " $";
            textoPrecioExplocivoWindows.text = PrecioMunicionExplociva + " $";
            textoPrecioDobleDanioWindows.text = PrecioDanioDoble + " $";
            textoPrecioDanzarinaWindows.text = PrecioMunicionDanzarina + " $";
            textoPrecioBlindajeWindows.text = PrecioBlindaje + " $";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(camvasTiendaAndroid.activeSelf == true || camvasTiendaWindows.activeSelf == true && GameManager.GetGameManager() != null)
        {
            GameManager.GetGameManager().pausa = true;
        }
        else
        {
            //GameManager.GetGameManager().pausa = false;
        }
       
    }
    public void AbrirTienda()
    {
        if (Jugador.GetJugador() != null)
        {
            if (Jugador.GetJugador().jugadorWindows && Jugador.GetJugador().jugadorAndroid == false)
            {
                texto.gameObject.SetActive(true);
                if (Input.GetKey(KeyCode.E))
                {
                    AbrirCamvasTienda();
                    //prende el camvas de tienda
                }
            }
            if (Jugador.GetJugador().jugadorAndroid && Jugador.GetJugador().jugadorWindows == false)
            {
                boton.SetActive(true);
                //al apretar el boton en pantalla prende la tienda
            }
        }
    }
    public void AbrirCamvasTienda()
    {
        if(Jugador.GetJugador() != null)
        {
            Jugador.GetJugador().EnTienda = true;
        }
        tiendaAbierta = true;
        if (Jugador.GetJugador().jugadorWindows && Jugador.GetJugador().jugadorAndroid == false)
        {
            camvasTiendaWindows.SetActive(true);
            texto.gameObject.SetActive(false);
            Jugador.GetJugador().EnTienda = true;
        }
        if (Jugador.GetJugador().jugadorAndroid && Jugador.GetJugador().jugadorWindows == false)
        {
            camvasTiendaAndroid.SetActive(true);
            boton.SetActive(false);
            if (menuArmasAndroid != null)
            {
                menuArmasAndroid.SetActive(true);
            }

        }
        if (GameManager.GetGameManager() != null)
        {
            GameManager.GetGameManager().pausa = true;
        }
    }
    public void CerrarTienda()
    {
        tiendaAbierta = false;
        if (Jugador.GetJugador() != null)
        {
            Jugador.GetJugador().EnTienda = false;
        }
        if (Jugador.GetJugador().jugadorWindows && Jugador.GetJugador().jugadorAndroid == false)
        {
            camvasTiendaWindows.SetActive(false);
            Jugador.GetJugador().EnTienda = false;
        }
        if (Jugador.GetJugador().jugadorAndroid && Jugador.GetJugador().jugadorWindows == false)
        {
            camvasTiendaAndroid.SetActive(false);
            if (menuArmasAndroid != null)
            {
                menuArmasAndroid.SetActive(false);
            }
        }
        if (GameManager.GetGameManager() != null)
        {
            GameManager.GetGameManager().pausa = false;
        }
        //que resiva el camvas de la tienda y la apague
    }
    public void Comprar(int queComprar)
    {
        if (Jugador.GetJugador() != null)
        {
            if (queComprar == 0)
            {
                CerrarTienda();
            }
            if (PrecioMunicionHielo <= Jugador.GetJugador().GetPuntos() && queComprar == 1)
            {
                Jugador.GetJugador().SumarMunicionPelotaHielo(12);
                Jugador.GetJugador().RestarPuntos(PrecioMunicionHielo);
            }
            if (PrecioMunicionFuego <= Jugador.GetJugador().GetPuntos() && queComprar == 2)
            {
                Jugador.GetJugador().SumarMunicionPelotaFuego(12);
                Jugador.GetJugador().RestarPuntos(PrecioMunicionFuego);
            }
            if (PrecioMunicionFragmentadora <= Jugador.GetJugador().GetPuntos() && queComprar == 3)
            {
                Jugador.GetJugador().SumarMunicionPelotaFragmentadora(30);
                Jugador.GetJugador().RestarPuntos(PrecioMunicionFragmentadora);
            }
            if (PrecioMunicionDanzarina <= Jugador.GetJugador().GetPuntos() && queComprar == 4)
            {
                Jugador.GetJugador().SumarMunicionPelotaDanzarina(8);
                Jugador.GetJugador().RestarPuntos(PrecioMunicionDanzarina);
            }
            if (PrecioMunicionExplociva <= Jugador.GetJugador().GetPuntos() && queComprar == 5)
            {
                Jugador.GetJugador().SumarMunicionPelotaExplosiva(10);
                Jugador.GetJugador().RestarPuntos(PrecioMunicionExplociva);
            }
            if (PrecioVida <= Jugador.GetJugador().GetPuntos() && queComprar == 6)
            {
                Jugador.GetJugador().AumentarVida();
                Jugador.GetJugador().RestarPuntos(PrecioVida);
            }
            if (PrecioBlindaje <= Jugador.GetJugador().GetPuntos() && queComprar == 7)
            {
                Jugador.GetJugador().ChalecoAntiGolpes();
                Jugador.GetJugador().RestarPuntos(PrecioBlindaje);
            }
            if (PrecioDanioDoble <= Jugador.GetJugador().GetPuntos() && queComprar == 8)
            {
                Jugador.GetJugador().DobleDanio();
                Jugador.GetJugador().RestarPuntos(PrecioDanioDoble);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            AbrirTienda();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (Jugador.GetJugador().jugadorWindows && Jugador.GetJugador().jugadorAndroid == false)
        {
            texto.gameObject.SetActive(false);
        }
        if (Jugador.GetJugador().jugadorAndroid && Jugador.GetJugador().jugadorWindows == false)
        {
            boton.SetActive(false);
        }
    }
}
