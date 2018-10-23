using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaberintoManager : MonoBehaviour {

    public int[] cantSpawnersPorZona;
    public int[] cantSpawnersDestruidosPorZona;
    public GameObject[] puertas;
    public Text objetivoWindows;
    public GameObject imagenTiradorWindows;
    public GameObject imagenCorredorWindows;
    public Text objetivoAndroid;
    public GameObject imagenTiradorAndroid;
    public GameObject imagenCorredorAndroid;

    private static LaberintoManager instanciaLaberintoManager;
    // Use this for initialization
    private void Awake()
    {
        //entrarRonda = true;

        if (instanciaLaberintoManager == null)
        {
            instanciaLaberintoManager = this;
        }
        else if (instanciaLaberintoManager != null)
        {
            this.gameObject.SetActive(false);
        }
    }
    public static LaberintoManager GetLaberintoManager()
    {
        return instanciaLaberintoManager;
    } 
    

    void Start () {
        cantSpawnersDestruidosPorZona = new int[cantSpawnersPorZona.Length];
	}
	
	// Update is called once per frame
	void Update () {
        CheckZona1();
    }
    public void SumarSpawnerDestruidos(int zona)
    {
        switch (zona)
        {
            case 0:
                cantSpawnersDestruidosPorZona[0]++;
                break;
        }
    }
    public void CheckZona1()
    {
        if (Jugador.GetJugador().jugadorWindows)
        {
            if (objetivoWindows != null && imagenCorredorWindows != null && imagenTiradorWindows != null)
            {
                objetivoWindows.text = cantSpawnersDestruidosPorZona[0] + "/" + cantSpawnersPorZona[0] + " " + imagenCorredorWindows + " " + imagenTiradorWindows;
                //imagenCorredor.SetActive(true);
                //imagenTirador.SetActive(true);
            }
        }
        if (Jugador.GetJugador().jugadorAndroid)
        {
            if (objetivoAndroid != null && imagenCorredorAndroid != null && imagenTiradorAndroid != null)
            {
                objetivoAndroid.text = cantSpawnersDestruidosPorZona[0] + "/" + cantSpawnersPorZona[0] + " " + imagenCorredorAndroid + " " + imagenTiradorAndroid;
            }
        }
        if (cantSpawnersPorZona[0] <= cantSpawnersDestruidosPorZona[0])
        {
            if (puertas[0] != null)
            {
                puertas[0].SetActive(false);
            }
            if (Jugador.GetJugador().jugadorWindows)
            {
                objetivoWindows.text = " ";
                imagenCorredorWindows.SetActive(false);
                imagenTiradorWindows.SetActive(false);
            }
            if(Jugador.GetJugador().jugadorAndroid)
            {
                objetivoAndroid.text = " ";
                imagenCorredorAndroid.SetActive(false);
                imagenTiradorAndroid.SetActive(false);
            }
        }
    }
}
