using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

    // Use this for initialization

    public Text TextRonda;
    public Text TextRondaAndroid;
    [HideInInspector]
    public int cantEnemigosEnPantalla;
    public SpawnerEnemigos[] spawnersEnemigos;
    public TiradorEstatico[] torretas;
    private bool Empiezo;
    private static GameManager instanciaGameManager;
    private int Ronda;
    public bool supervivencia;
    public bool historia;
    public bool verificarRonda;
    public bool verificarVictoria;
    public int RondaVictoria;
    private int muertes;
    public int limiteMuertes;
    public string mapaActual;
    public string mapaSiguiente;
    private bool entrarRonda;
    private bool victoria;
    private int cantTorretasEnPantalla;
    private bool unaVezPorRonda;
    [HideInInspector]
    public bool pasarNivel = false;
    [HideInInspector]
    public bool pausa;

    public static GameManager GetGameManager()
    {
        return instanciaGameManager;
    }
    private void Awake()
    {
        //entrarRonda = true;
        Ronda = 1;
        muertes = 0;
        unaVezPorRonda = false;
        if (instanciaGameManager == null)
        {
            instanciaGameManager = this;
        }
        else if (instanciaGameManager != null)
        {
            this.gameObject.SetActive(false);            
        }
    }
    private void Start()
    {
        cantEnemigosEnPantalla = 0;
        if (!historia && supervivencia)
        {
            if (mapaActual == "Arena(Supervivencia)")
            {
                if (spawnersEnemigos[4] != null && spawnersEnemigos[5] != null && spawnersEnemigos[6] != null && spawnersEnemigos[7] != null)
                {
                    spawnersEnemigos[4].gameObject.SetActive(false);
                    spawnersEnemigos[5].gameObject.SetActive(false);
                    spawnersEnemigos[6].gameObject.SetActive(false);
                    spawnersEnemigos[7].gameObject.SetActive(false);
                }
            }
        }
        if (torretas != null)
        {
            for(int i = 0; i<torretas.Length; i++)
            {
                if(torretas[i] != null)
                {
                    torretas[i].gameObject.SetActive(false);
                }
            }
        }
    }


    // Update is called once per frame
    void Update () {
        CheckTorreta();
        VerificarVictoria();
        if (pausa)
        {
            Time.timeScale = 0;
        }
        if(!pausa)
        {
            Time.timeScale = 1;
        }
        if (supervivencia)
        {

            for (int i = 0; i < spawnersEnemigos.Length; i++)
            {
                
                if (cantEnemigosEnPantalla <= 0 && cantTorretasEnPantalla <= 0)
                {
                    
                    spawnersEnemigos[i].SetEnFuncionamiento(true);
                    if (entrarRonda)
                    {
                        unaVezPorRonda = true;
                        SumarRonda();
                        entrarRonda = false;
                    }
                }
                if (mapaActual == "Arena(Historia)" && unaVezPorRonda)
                {
                    VerificarVictoria();
                    if (!victoria)
                    {
                        ActivacionTorretas();
                    }
                    unaVezPorRonda = false;
                }
            }
        }

        if(mapaActual == "Laberinto")
        {
            if (TextRonda != null)
            {
                TextRonda.text = "";
            }

        }
        
        if(historia && !supervivencia && pasarNivel == true)
        {
            MostrarRonda();
            /*if(muertes >= limiteMuertes)
            {
                SceneManager.LoadScene(mapaSiguiente);
            }*/
        }
        if(!historia && supervivencia)
        {
            MostrarRonda();
            if (mapaActual == "Arena(Supervivencia)")
            {
                if (spawnersEnemigos[4] != null && spawnersEnemigos[5] != null && spawnersEnemigos[6] != null && spawnersEnemigos[7] != null)
                {
                    if (Ronda % 5 == 0)
                    {
                        spawnersEnemigos[4].gameObject.SetActive(true);
                        spawnersEnemigos[5].gameObject.SetActive(true);
                        spawnersEnemigos[6].gameObject.SetActive(true);
                        spawnersEnemigos[7].gameObject.SetActive(true);
                    }
                    if(Ronda % 5 != 0)
                    {
                        spawnersEnemigos[4].gameObject.SetActive(false);
                        spawnersEnemigos[5].gameObject.SetActive(false);
                        spawnersEnemigos[6].gameObject.SetActive(false);
                        spawnersEnemigos[7].gameObject.SetActive(false);
                    }
                }
            }
        }
	}
    public void ActivacionTorretas()
    {
        if (Ronda == 2)
        {
            torretas[0].gameObject.SetActive(true);
            torretas[1].gameObject.SetActive(true);
        }
        if (Ronda == 3)
        {
            torretas[0].gameObject.SetActive(true);
            torretas[1].gameObject.SetActive(true);
            torretas[2].gameObject.SetActive(true);
            torretas[3].gameObject.SetActive(true);
        }
        if (Ronda == 4)
        {
            torretas[4].gameObject.SetActive(true);
            torretas[5].gameObject.SetActive(true);
            torretas[6].gameObject.SetActive(true);
            torretas[7].gameObject.SetActive(true);
            torretas[8].gameObject.SetActive(true);
            torretas[9].gameObject.SetActive(true);
            torretas[10].gameObject.SetActive(true);
            torretas[11].gameObject.SetActive(true);
        }
        if (Ronda >= 5)
        {
            torretas[0].gameObject.SetActive(true);
            torretas[1].gameObject.SetActive(true);
            torretas[2].gameObject.SetActive(true);
            torretas[3].gameObject.SetActive(true);
            torretas[4].gameObject.SetActive(true);
            torretas[5].gameObject.SetActive(true);
            torretas[6].gameObject.SetActive(true);
            torretas[7].gameObject.SetActive(true);
            torretas[8].gameObject.SetActive(true);
            torretas[9].gameObject.SetActive(true);
            torretas[10].gameObject.SetActive(true);
            torretas[11].gameObject.SetActive(true);
        }
    }
    public bool GetVictoria()
    {
        return victoria;
    }
    public void VerificarVictoria()
    {
        if(Ronda >= RondaVictoria)
        {
            victoria = true;
            for (int i = 0; i < spawnersEnemigos.Length; i++)
            {
                if(spawnersEnemigos[i] != null)
                {
                    spawnersEnemigos[i].SetEnFuncionamiento(false);
                    spawnersEnemigos[i].gameObject.SetActive(false);
                }
            }
        }
    }
    public void CheckTorreta()
    {
        if (torretas != null)
        {
            cantTorretasEnPantalla = torretas.Length;
            for (int i = 0; i < torretas.Length; i++)
            {
                if (torretas[i] != null)
                {
                    if (torretas[i].gameObject.activeSelf == false && cantTorretasEnPantalla > 0)
                    {
                        cantTorretasEnPantalla--;
                    }
                    
                }
            }
        }
    }
    public void MostrarRonda()
    {
        if (TextRonda != null)
        {
            TextRonda.text = "RONDA: " + Ronda;
        }
        if (TextRondaAndroid != null)
        {
            TextRondaAndroid.text = "RONDA: " + Ronda;
        }
    }
    public void SetEnemigosEnPantalla(int enemigosEnPantalla)
    {
        cantEnemigosEnPantalla = enemigosEnPantalla;
    }
    public void SumarEnemigoEnPantalla()
    {
        cantEnemigosEnPantalla++;
    }
    public void RestarEnemigoEnPantalla()
    {
        cantEnemigosEnPantalla--;
    }
    public int GetEnemigosEnPantalla()
    {
        return cantEnemigosEnPantalla;
    }
    public void SumarRonda()
    {
        Ronda = Ronda + 1;
    }
    public int GetRonda()
    {
        return Ronda;
    }
    public void SetRonda(int _Ronda)
    {
        Ronda = _Ronda;
    }
    public void SumarMuertes()
    {
        muertes = muertes + 1;
    }
    public void SetEntrarRonda(bool _entrarRonda)
    {
        entrarRonda = _entrarRonda;
    }
}
