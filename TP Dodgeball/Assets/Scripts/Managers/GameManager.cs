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
    private bool Empiezo;
    private static GameManager instanciaGameManager;
    private int Ronda;
    public bool supervivencia;
    public bool historia;
    private int muertes;
    public int limiteMuertes;
    public string mapaActual;
    public string mapaSiguiente;
    private bool entrarRonda;
    [HideInInspector]
    public bool pasarNivel = false;
    private void Awake()
    {
        //entrarRonda = true;
        Ronda = 1;
        muertes = 0;
        if (instanciaGameManager == null)
        {
            instanciaGameManager = this;
        }
        else if (instanciaGameManager != null)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        cantEnemigosEnPantalla = 0;
        if (!historia && supervivencia )
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
    }


    // Update is called once per frame
    void Update () {
        if (supervivencia)
        {
            for (int i = 0; i < spawnersEnemigos.Length; i++)
            {
                if (cantEnemigosEnPantalla <= 0)
                {
                    spawnersEnemigos[i].SetEnFuncionamiento(true);
                    if (entrarRonda)
                    {
                        SumarRonda();
                        entrarRonda = false;
                    }
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
            if(muertes >= limiteMuertes)
            {
                SceneManager.LoadScene(mapaSiguiente);
            }
        }
        if(!historia && supervivencia)
        {
            if (TextRonda != null)
            {
                TextRonda.text = "Ronda: " + Ronda;
            }
            if (TextRondaAndroid != null)
            {
                TextRondaAndroid.text = "Ronda: " + Ronda;
            }
            if (mapaActual == "Arena(Supervivencia)")
            {
                if (spawnersEnemigos[4] != null && spawnersEnemigos[5] != null && spawnersEnemigos[6] != null && spawnersEnemigos[7] != null)
                {
                    //spawnersEnemigos[4].gameObject.SetActive(false);
                    //spawnersEnemigos[5].gameObject.SetActive(false);
                    //spawnersEnemigos[6].gameObject.SetActive(false);
                    //spawnersEnemigos[7].gameObject.SetActive(false);
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


    public static GameManager GetGameManager()
    {
        return instanciaGameManager;
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
