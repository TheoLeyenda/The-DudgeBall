using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Jugador : MonoBehaviour {

    // Use this for initialization
    [HideInInspector]
    public bool enPausa;
    private static Jugador instanciaJugador;
    private int TOPE_MUNICION = 500;
    public float vida;
    public float maxVida;
    public float blindaje;
    [HideInInspector]
    public int tipoPelota;
    private int puntos;
    [HideInInspector]
    public bool contar;
    public PelotaEnemigo PelotaComunEnemigo;
    public Enemigo corredor;
    public Enemigo tirador;
    public Enemigo tiradorEstatico;
    //public Enemigo Danio;
    public Pinchos pinchos;
    public bool jugadorWindows;
    public bool jugadorAndroid;
    public Rigidbody rigJugador;
    public int oportunidades;
    public Transform posRespawn;

    private float danioAdicionalPelotaComun;
    private float danioAdicionalPelotaHielo;
    private float danioAdicionalPelotaFuego;
    private float danioAdicionalPelotaExplosiva;
    private float danioAdicionalMiniPelota;

    public Text textVida;
    public Text textPuntos;
    public Text textBlindaje;
    public Text TextOportunidades;

    private int municionPelotaDeHielo = 0;
    private int municionPelotaDeFuego = 0;
    private int municionPelotaFragmentadora = 0;
    private int municionPelotaDanzarina = 0;
    private int municionPelotaExplosiva = 0;

    public Text textMunicionPelotaDeHielo;
    public Text textMunicionPelotaDeFuego;
    public Text textMunicionPelotaFragmentadora;
    public Text textMunicionPelotaDanzarina;
    public Text textMunicionPelotaExplosiva;

    public GameObject blockHielo;
    public GameObject blockFuego;
    public GameObject blockDanzarina;
    public GameObject blockFragmentadora;
    public GameObject blockExplosiva;

    public GameObject desbloqueadoHielo;
    public GameObject desbloqueadoFuego;
    public GameObject desbloqueadoDanzarina;
    public GameObject desbloqueadoFragmentadora;
    public GameObject desbloqueadoExplosiva;

    private bool powerUpAumentarVida;
    public bool powerUpChalecoAntiGolpes;
    private bool powerUpDobleDanio;

    public GameObject logoBlindaje;
    public GameObject logoDobleDanio;

    public bool EnTienda;

    public GameObject logoInmulnerabilidad;
    public GameObject logoDoblePuntaje;
    public GameObject logoInstaKill;
    //private bool powerUpDoblePelota;

    private bool Inmune;
    private bool doblePuntuacion;
    private bool InstaKill;
    private bool activoInstaKill;
    //private float auxContInmune;
    private float contInmune;
    private float contDoblePuntuacion;
    private float contInstaKill;
    private float dileyActivacion;
    
    public void AumentarVida()
    {
        powerUpAumentarVida = true;
    }
    public void ChalecoAntiGolpes()
    {
        powerUpChalecoAntiGolpes = true;
        if(logoBlindaje != null)
        {
            logoBlindaje.SetActive(true);
        }
    }
    public void DobleDanio()
    {
        powerUpDobleDanio = true;
        if(logoDobleDanio != null)
        {
            logoDobleDanio.SetActive(true);
        }
    }
    public void RestarPuntos(int _puntos)
    {
        puntos = puntos - _puntos;
    }
    void Start() {
        
        EnTienda = false;
        contInmune = 0;
        danioAdicionalPelotaComun = 0;
        danioAdicionalMiniPelota = 0;
        danioAdicionalPelotaExplosiva = 0;
        danioAdicionalPelotaFuego = 0;
        danioAdicionalPelotaHielo = 0;
        //blindaje = 0;
        if (textBlindaje != null)
        {
            textBlindaje.gameObject.SetActive(false);
        }
        Inmune = false;
        contar = true;
        puntos = 0;
        textVida.text = ""+((int)vida);
        instanciaJugador = this;
        tipoPelota = 1;
        municionPelotaDeHielo = 0;
        if (EstructuraDatosAuxiliares.GetEstructuraDatosAuxiliares() != null)
        {
            if (EstructuraDatosAuxiliares.GetEstructuraDatosAuxiliares().soloUnaVez)
            {
                EstructuraDatosAuxiliares.GetEstructuraDatosAuxiliares().soloUnaVez = false;
            }
            else
            {
                EstructuraDatosAuxiliares.GetEstructuraDatosAuxiliares().SetValoresDelJugador(Jugador.GetJugador());
            }
        }
    }

    // Update is called once per frame
    public void ControlCursor()
    {
        if (EnTienda || jugadorAndroid || enPausa)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (!EnTienda && !jugadorAndroid && !enPausa)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    void Update() {
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
            //LeanRodrigez98
        }
        ControlCursor();
        if(doblePuntuacion)
        {
            logoDoblePuntaje.SetActive(true);
        }
        if(doblePuntuacion == false)
        {
            logoDoblePuntaje.SetActive(false);
        }
        if (Inmune == true)
        {
            logoInmulnerabilidad.SetActive(true);
            contInmune = contInmune - Time.deltaTime;
            if(contInmune <= 0)
            {
                Inmune = false;
            }
        }
        if(Inmune == false)
        {
            logoInmulnerabilidad.SetActive(false);
        }
        if(InstaKill == false)
        {
            logoInstaKill.SetActive(false);
        }
        if(doblePuntuacion == true)
        {
            contDoblePuntuacion = contDoblePuntuacion - Time.deltaTime;
            if (contDoblePuntuacion <= 0)
            {
                doblePuntuacion = false;
            }
        }
        if(InstaKill == true)
        {
            logoInstaKill.SetActive(true);
            contInstaKill = contInstaKill - Time.deltaTime;
            if(contInstaKill <= 0)
            {
                InstaKill = false;
            }
        }
        if (municionPelotaDanzarina > 0)
        {
            blockDanzarina.SetActive(false);
            desbloqueadoDanzarina.SetActive(true);
        }
        if(municionPelotaDeFuego > 0)
        {
            blockFuego.SetActive(false);
            desbloqueadoFuego.SetActive(true);
        }
        if(municionPelotaDeHielo > 0)
        {
            blockHielo.SetActive(false);
            desbloqueadoHielo.SetActive(true);
        }
        if(municionPelotaExplosiva > 0)
        {
            blockExplosiva.SetActive(false);
            desbloqueadoExplosiva.SetActive(true);
        }
        if(municionPelotaFragmentadora > 0)
        {
            blockFragmentadora.SetActive(false);
            desbloqueadoFragmentadora.SetActive(true);
        }
        if(blindaje <= 0)
        {
            blindaje = 0;
            if(logoBlindaje != null)
            {
                logoBlindaje.SetActive(false);
            }
        }
        if(powerUpAumentarVida)
        {
            vida = maxVida;
            powerUpAumentarVida = false;

        }
        if(powerUpChalecoAntiGolpes)
        {
            logoBlindaje.SetActive(true);
            textBlindaje.gameObject.SetActive(true);
            blindaje = 100;
            powerUpChalecoAntiGolpes = false;
        }
        if(powerUpDobleDanio)
        {
            //definido en enemigo
        }
        textVida.text = "VIDA: " + (int)vida;
        textPuntos.text = "PUNTOS: " + puntos;
        if (oportunidades > -1 && TextOportunidades != null)
        {
            TextOportunidades.text = "OPORTUNIDADES: " + oportunidades;
        }
        if(textBlindaje != null)
        {
            textBlindaje.text = "BLINDAJE:" + (int)blindaje;
        }
        if (vida <= 0)
        {
            oportunidades = oportunidades - 1;
            if (vida <= 0 && oportunidades < 0)
            {
                SceneManager.LoadScene("GameOver");
                gameObject.SetActive(false);
            }
            else
            {
                if (posRespawn != null)
                {
                    transform.position = posRespawn.position;
                    vida = 100;
                }
                else
                {
                    SceneManager.LoadScene("GameOver");
                    gameObject.SetActive(false);
                }
            }
        }
        if (textMunicionPelotaDeHielo != null)
        {
            textMunicionPelotaDeHielo.text = municionPelotaDeHielo + "";
        }
        if (textMunicionPelotaDeFuego != null)
        {
            textMunicionPelotaDeFuego.text = municionPelotaDeFuego + "";
        }
        if (textMunicionPelotaFragmentadora != null)
        {
            textMunicionPelotaFragmentadora.text = municionPelotaFragmentadora + "";
        }
        if (textMunicionPelotaDanzarina != null)
        {
            textMunicionPelotaDanzarina.text = municionPelotaDanzarina + "";
        }
        if (textMunicionPelotaExplosiva != null)
        {
            textMunicionPelotaExplosiva.text = municionPelotaExplosiva + "";
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "RompeObjetos")
        {
            SceneManager.LoadScene("GameOver");
        }
        if(other.tag == "ZonaRespawn")
        {
            if (EstructuraDatosAuxiliares.GetEstructuraDatosAuxiliares() != null)
            {
                
                EstructuraDatosAuxiliares.GetEstructuraDatosAuxiliares().SetDatosJugador(Jugador.GetJugador());
                if (blindaje > 0)
                {
                    EstructuraDatosAuxiliares.GetEstructuraDatosAuxiliares().DatosJugador.blindaje = blindaje;
                    logoBlindaje.SetActive(true);
                    textBlindaje.gameObject.SetActive(true);
                }
            }
            posRespawn = other.gameObject.transform;
        }
        if(other.tag == "PoderInmune")
        {
            other.gameObject.SetActive(false);
            Inmune = true;
            contInmune = 15;
        }
        if(other.tag == "DoblePuntuacion")
        {
            other.gameObject.SetActive(false);
            doblePuntuacion = true;
            contDoblePuntuacion = 20;

        }
        if (other.tag == "InstaKill")
        {
            other.gameObject.SetActive(false);
            InstaKill = true;
            activoInstaKill = true;
            contInstaKill = 12;
        }
        if (other.tag == "PickUpHielo")
        {
            municionPelotaDeHielo = municionPelotaDeHielo + 12;
            contar = true;
            if (blockHielo != null)
            {
                blockHielo.SetActive(false);
            }
            if (desbloqueadoHielo != null)
            {
                desbloqueadoHielo.SetActive(true);
            }
            //Destroy(other.gameObject);// destruyo la municion cada vez que la toco 
        }
        if (other.tag == "PickUpFuego")
        {
            municionPelotaDeFuego = municionPelotaDeFuego + 12;
            contar = true;
            if (blockFuego != null)
            {
                blockFuego.SetActive(false);
            }
            if (desbloqueadoFuego != null)
            {
                desbloqueadoFuego.SetActive(true);
            }
            //Destroy(other.gameObject);// destruyo la municion cada vez que la toco 
        }
        if (other.tag == "PickUpDanzarina")
        {
            municionPelotaDanzarina = municionPelotaDanzarina + 8;
            contar = true;
            if (blockDanzarina != null)
            {
                blockDanzarina.SetActive(false);
            }
            if (desbloqueadoDanzarina != null)
            {
                desbloqueadoDanzarina.SetActive(true);
            }
            //Destroy(other.gameObject);// destruyo la municion cada vez que la toco 
        }
        if (other.tag == "PickUpFragmentadora")
        {
            municionPelotaFragmentadora = municionPelotaFragmentadora + 30;
            contar = true;
            if (blockFragmentadora != null)
            {
                blockFragmentadora.SetActive(false);
            }
            if (desbloqueadoFragmentadora != null)
            {
                desbloqueadoFragmentadora.SetActive(true);
            }
            //Destroy(other.gameObject);// destruyo la municion cada vez que la toco 
        }
        if (other.tag == "PickUpExplosivo")
        {
            municionPelotaExplosiva = municionPelotaExplosiva + 10;
            contar = true;
            if (blockExplosiva != null)
            {
                blockExplosiva.SetActive(false);
            }
            if (desbloqueadoExplosiva != null)
            {
                desbloqueadoExplosiva.SetActive(true);
            }
        }

        //AQUI RESIVO DAÑO
        if (other.tag == "PelotaEnemigoComun")
        {
            if (!Inmune)
            {
                if (blindaje > 0)
                {
                    blindaje = blindaje - PelotaComunEnemigo.danio;
                }
                else
                {
                    vida = vida - PelotaComunEnemigo.danio;
                }
            }
        }
    }
    public static Jugador GetJugador()
    {
        return instanciaJugador;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Roca")
        {
            if (blindaje > 0)
            {
                blindaje = 0;
            }
            else
            {
                vida = 0;
            }
        }
        
    }
    private void OnCollisionStay(Collision collision)
    {
        if (!Inmune)
        {
            if (collision.gameObject.tag == "Corredor")
            {
                if (blindaje > 0)
                {
                    blindaje = blindaje - 1;
                }
                else
                {
                    vida = vida - 1;
                }
            }
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "ObjetoDestruible")
        {
            other.gameObject.SetActive(false);
        }
        if (!Inmune)
        {
            if (other.tag == "Pinchos")
            {
                if (blindaje > 0)
                {
                    blindaje = blindaje - pinchos.danio;
                }
                else
                {
                    vida = vida - pinchos.danio;
                }
            }
            if (other.gameObject.tag == "Corredor")
            {
                if (blindaje > 0)
                {
                    blindaje = blindaje - 1;
                }
                else
                {
                    vida = vida - 1;
                }
            }
        }
    }
    public void SumarMunicionPelotaFuego(int municion)
    {
        if (municionPelotaDeFuego <= TOPE_MUNICION)
        {
            municionPelotaDeFuego = municionPelotaDeFuego + municion;
        }
    }
    public void SumarMunicionPelotaHielo(int municion)
    {
        if (municionPelotaDeHielo <= TOPE_MUNICION)
        {
            municionPelotaDeHielo = municionPelotaDeHielo + municion;
        }
    }
    public void SumarMunicionPelotaFragmentadora(int municion)
    {
        if (municionPelotaFragmentadora <= TOPE_MUNICION)
        {
            municionPelotaFragmentadora = municionPelotaFragmentadora + municion;
        }
    }
    public void SumarMunicionPelotaDanzarina(int municion)
    {
        if (municionPelotaDanzarina <= TOPE_MUNICION)
        {
            municionPelotaDanzarina = municionPelotaDanzarina + municion;
        }
    }
    public void SumarMunicionPelotaExplosiva(int municion)
    {
        if (municionPelotaExplosiva <= TOPE_MUNICION)
        {
            municionPelotaExplosiva = municionPelotaExplosiva + municion;
        }
    }
    public int GetTOPEMUNICION()
    {
        return TOPE_MUNICION;
    }
    public int GetMunicionPelotaFuego()
    {
        return municionPelotaDeFuego;
    }
    public int GetMunicionPelotaHielo()
    {
        return municionPelotaDeHielo;
    }
    public int GetMunicionPelotaFragmentadora()
    {
        return municionPelotaFragmentadora;
    }
    public int GetMunicionPelotaDanzarina()
    {
        return municionPelotaDanzarina;
    }
    public int GetMunicionPelotaExplosiva()
    {
        return municionPelotaExplosiva;
    }
    public void RestarMunicionFuego()
    {
        municionPelotaDeFuego = municionPelotaDeFuego - 1;
    }
    public void RestarMunicionDanzarina()
    {
        municionPelotaDanzarina = municionPelotaDanzarina - 1;
    }
    public void RestarMunicionHielo()
    {
        municionPelotaDeHielo = municionPelotaDeHielo - 1;
    }
    public void SetTOPEMUNICION(int topeMunicion)
    {
        TOPE_MUNICION = topeMunicion;
    }
    public void SetMunicionPelotaDeHielo(int municion)
    {
        municionPelotaDeHielo = municion;
    }
    public void SetMunicionPelotaDeFuego(int municion)
    {
        municionPelotaDeFuego = municion;
    }
    public void SetMunicionDanzarina(int municion)
    {
        municionPelotaDanzarina = municion;
    }
    public void SetMunicionFragmentadora(int municion)
    {
        municionPelotaExplosiva = municion;
    }
    public void SetChalecoAntiGolpes(bool _chalecoAntiGolpes)
    {
        powerUpChalecoAntiGolpes = _chalecoAntiGolpes;
    }
    public void SetDobleDanio(bool _dobleDanio)
    {
        powerUpDobleDanio = _dobleDanio;
    }
    public void SetInmune(bool _inmune)
    {
        Inmune = _inmune;
    }
    public void SetDoblePuntuacion(bool _doblePuntuacion)
    {
        doblePuntuacion = _doblePuntuacion;
    }
    public void SetInstaKill(bool _instaKill)
    {
        InstaKill = _instaKill;
    }
    public void SetCountInmune(float _contInmune)
    {
        contInmune = _contInmune;
    }
    public void SetCountDoblePuntiacion(float _countDoblePuntuacion)
    {
        contDoblePuntuacion = _countDoblePuntuacion;
    }
    public void SetMunicionExplociva(int municion)
    {
        municionPelotaExplosiva = municion;
    }
    public void SetDileyActivacion(float diley)
    {
        dileyActivacion = diley;
    }
    public void SetCountInstaKill(float _contInstaKill)
    {
        contInstaKill = _contInstaKill;
    }
    public void SetPuntos(int _puntos)
    {
        puntos = _puntos;
    }
    public void RestarMunicionFragmentadora()
    {
        municionPelotaFragmentadora = municionPelotaFragmentadora - 1;
    }
    public void RestarMunicionExplosiva()
    {
        municionPelotaExplosiva = municionPelotaExplosiva - 1;
    }
    public int GetPuntos()
    {
        return puntos;
    }
    public void SumarPuntos(int sumaPuntos)
    {
        puntos = puntos + sumaPuntos;
    }
    public void SetDanioAdicionalPelotaComun(float _adicionalPelotaComun)
    {
        danioAdicionalPelotaComun = _adicionalPelotaComun;
    }
    public void SetDanioAdicionalMiniPelota(float _adicionalMiniPelota)
    {
        danioAdicionalMiniPelota = _adicionalMiniPelota;
    }
    public void SetDanioAdicionalPelotaExplosiva(float _adicionalPelotaExplociva)
    {
        danioAdicionalPelotaExplosiva = _adicionalPelotaExplociva;
    }
    public void SetDanioAdicionalPelotaFuego(float _adicionalPelotaFuego)
    {
        danioAdicionalPelotaFuego = _adicionalPelotaFuego;
    }
    public void SetDanioAdicionalPelotaHielo(float _adicionalPelotaHielo)
    {
        danioAdicionalPelotaHielo = _adicionalPelotaHielo;
    }
    public float GetDanioAdicionalPelotaComun()
    {
        return danioAdicionalPelotaComun;
    }
    public float GetDanioAdicionalMiniPelota()
    {
        return danioAdicionalMiniPelota;
    }
    public float GetDanioAdicionalPelotaExplociva()
    {
        return danioAdicionalPelotaExplosiva;
    }
    public float GetDanioAdicionalPelotaFuego()
    {
        return danioAdicionalPelotaFuego;
    }
    public float GetDanioAdicionalPelotaHielo()
    {
        return danioAdicionalPelotaHielo;
    }
    public bool GetPowerUpDobleDanio()
    {
        return powerUpDobleDanio;
    }
    public void SetPowerUpDobleDanio(bool _powerUpDobleDanio)
    {
        powerUpDobleDanio = _powerUpDobleDanio;
    }
    public void SetPowerUpMasVida(bool _vida)
    {
        powerUpAumentarVida = _vida;
    }
    public void SetPowerUpBlindaje(bool _blindaje)
    {
        powerUpChalecoAntiGolpes = _blindaje;
    }
    public bool GetDoblePuntuacion()
    {
        return doblePuntuacion;
    }
    public bool GetInstaKill()
    {
        return InstaKill;
    }
    public bool GetActivarInstaKill()
    {
        return activoInstaKill;
    }
    public void SetActivarInstaKill(bool _InstaKill)
    {
        activoInstaKill = _InstaKill;
    }
    public bool GetPowerUpAumentarVida()
    {
        return powerUpAumentarVida;
    }
    public bool GetPowerUpChalecoAntiGolpes()
    {
        return powerUpChalecoAntiGolpes;
    }
    public float GetContInmune()
    {
        return contInmune;
    }
    public bool GetInmune()
    {
        return Inmune;
    }
    public float GetContDoblePuntuacion()
    {
        return contDoblePuntuacion;
    }
    public float GetContInstaKill()
    {
        return contInstaKill;
    }
    public float GetDileyActivacion()
    {
        return dileyActivacion;
    }
}
