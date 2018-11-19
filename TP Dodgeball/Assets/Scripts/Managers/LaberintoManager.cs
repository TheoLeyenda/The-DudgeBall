using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaberintoManager : MonoBehaviour
{

    public int[] cantSpawnersPorZona;
    public int[] cantSpawnersDestruidosPorZona;
    public GameObject[] puertas;
    public Text objetivoWindows;
    public GameObject imagenTiradorWindows;
    public GameObject imagenCorredorWindows;
    public Text objetivoAndroid;
    public GameObject imagenTiradorAndroid;
    public GameObject imagenCorredorAndroid;
    public int ZonaActual;

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
        Jugador.GetJugador().oportunidades = 3;
    }
    public static LaberintoManager GetLaberintoManager()
    {
        return instanciaLaberintoManager;
    }


    void Start()
    {
        cantSpawnersDestruidosPorZona = new int[cantSpawnersPorZona.Length];
        Jugador.GetJugador().oportunidades = 3;
    }

    // Update is called once per frame
    void Update()
    {
        switch (ZonaActual)
        {
            case 0:
                CheckZona0();
                break;
            case 1:
                CheckZona1();
                break;
            case 2:
                CheckZona2();
                break;
            case 3:
                CheckZona3();
                break;
            case 4:
                CheckZona4();
                break;
        }

    }
    public void SumarSpawnerDestruidos(int zona)
    {
        cantSpawnersDestruidosPorZona[zona]++;
    }
    public void CheckZona0()
    {
        if (Jugador.GetJugador() != null)
        {
            if (Jugador.GetJugador().jugadorWindows)
            {
                if (objetivoWindows != null && imagenCorredorWindows != null && imagenTiradorWindows != null)
                {
                    objetivoWindows.text = cantSpawnersDestruidosPorZona[0] + "/" + cantSpawnersPorZona[0] + " " + imagenCorredorWindows + " " + imagenTiradorWindows;
                    imagenCorredorWindows.SetActive(true);
                    imagenTiradorWindows.SetActive(true);
                }
            }

            if (Jugador.GetJugador().jugadorAndroid)
            {
                if (objetivoAndroid != null && imagenCorredorAndroid != null && imagenTiradorAndroid != null)
                {
                    objetivoAndroid.text = cantSpawnersDestruidosPorZona[0] + "/" + cantSpawnersPorZona[0] + " " + imagenCorredorAndroid + " " + imagenTiradorAndroid;
                    imagenCorredorAndroid.SetActive(true);
                    imagenTiradorAndroid.SetActive(true);
                }
            }
            if (cantSpawnersPorZona[0] <= cantSpawnersDestruidosPorZona[0])
            {
                ZonaActual++;
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
                if (Jugador.GetJugador().jugadorAndroid)
                {
                    objetivoAndroid.text = " ";
                    imagenCorredorAndroid.SetActive(false);
                    imagenTiradorAndroid.SetActive(false);
                }
            }
        }
    }
    public void CheckZona1()
    {
        if (Jugador.GetJugador() != null)
        {
            if (Jugador.GetJugador().jugadorWindows)
            {
                if (objetivoWindows != null && imagenCorredorWindows != null && imagenTiradorWindows != null)
                {
                    objetivoWindows.text = cantSpawnersDestruidosPorZona[1] + "/" + cantSpawnersPorZona[1] + " " + imagenCorredorWindows + " " + imagenTiradorWindows;
                    imagenCorredorWindows.SetActive(true);
                    imagenTiradorWindows.SetActive(true);
                }
                if (Jugador.GetJugador().jugadorAndroid)
                {
                    if (objetivoAndroid != null && imagenCorredorAndroid != null && imagenTiradorAndroid != null)
                    {
                        objetivoAndroid.text = cantSpawnersDestruidosPorZona[1] + "/" + cantSpawnersPorZona[1] + " " + imagenCorredorAndroid + " " + imagenTiradorAndroid;
                        imagenCorredorAndroid.SetActive(true);
                        imagenTiradorAndroid.SetActive(true);
                    }
                }
                if (cantSpawnersPorZona[1] <= cantSpawnersDestruidosPorZona[1])
                {
                    ZonaActual++;
                    if (puertas[1] != null)
                    {
                        puertas[1].SetActive(false);
                    }
                    if (Jugador.GetJugador().jugadorWindows)
                    {
                        objetivoWindows.text = " ";
                        imagenCorredorWindows.SetActive(false);
                        imagenTiradorWindows.SetActive(false);
                    }
                    if (Jugador.GetJugador().jugadorAndroid)
                    {
                        objetivoAndroid.text = " ";
                        imagenCorredorAndroid.SetActive(false);
                        imagenTiradorAndroid.SetActive(false);
                    }
                }
            }
        }
    }
    public void CheckZona2()
    {
        if (Jugador.GetJugador() != null)
        {
            if (Jugador.GetJugador().jugadorWindows)
            {
                if (objetivoWindows != null && imagenCorredorWindows != null && imagenTiradorWindows != null)
                {
                    objetivoWindows.text = cantSpawnersDestruidosPorZona[2] + "/" + cantSpawnersPorZona[2] + " " + imagenCorredorWindows + " " + imagenTiradorWindows;
                    imagenCorredorWindows.SetActive(true);
                    imagenTiradorWindows.SetActive(true);
                }
                if (Jugador.GetJugador().jugadorAndroid)
                {
                    if (objetivoAndroid != null && imagenCorredorAndroid != null && imagenTiradorAndroid != null)
                    {
                        objetivoAndroid.text = cantSpawnersDestruidosPorZona[2] + "/" + cantSpawnersPorZona[2] + " " + imagenCorredorAndroid + " " + imagenTiradorAndroid;
                        imagenCorredorAndroid.SetActive(true);
                        imagenTiradorAndroid.SetActive(true);
                    }
                }
                if (cantSpawnersPorZona[2] <= cantSpawnersDestruidosPorZona[2])
                {
                    ZonaActual++;
                    if (puertas[2] != null)
                    {
                        puertas[2].SetActive(false);
                    }
                    if (Jugador.GetJugador().jugadorWindows)
                    {
                        objetivoWindows.text = " ";
                        imagenCorredorWindows.SetActive(false);
                        imagenTiradorWindows.SetActive(false);
                    }
                    if (Jugador.GetJugador().jugadorAndroid)
                    {
                        objetivoAndroid.text = " ";
                        imagenCorredorAndroid.SetActive(false);
                        imagenTiradorAndroid.SetActive(false);
                    }
                }
            }
        }
    }
    public void CheckZona3()
    {
        if (Jugador.GetJugador() != null)
        {
            if (Jugador.GetJugador().jugadorWindows)
            {
                if (objetivoWindows != null && imagenCorredorWindows != null && imagenTiradorWindows != null)
                {
                    objetivoWindows.text = cantSpawnersDestruidosPorZona[3] + "/" + cantSpawnersPorZona[3] + " " + imagenCorredorWindows + " " + imagenTiradorWindows;
                    imagenCorredorWindows.SetActive(true);
                    imagenTiradorWindows.SetActive(true);
                }
                if (Jugador.GetJugador().jugadorAndroid)
                {
                    if (objetivoAndroid != null && imagenCorredorAndroid != null && imagenTiradorAndroid != null)
                    {
                        objetivoAndroid.text = cantSpawnersDestruidosPorZona[3] + "/" + cantSpawnersPorZona[3] + " " + imagenCorredorAndroid + " " + imagenTiradorAndroid;
                        imagenCorredorAndroid.SetActive(true);
                        imagenTiradorAndroid.SetActive(true);
                    }
                }
                if (cantSpawnersPorZona[3] <= cantSpawnersDestruidosPorZona[3])
                {
                    ZonaActual++;
                    if (puertas[3] != null)
                    {
                        puertas[3].SetActive(false);
                    }
                    if (Jugador.GetJugador().jugadorWindows)
                    {
                        objetivoWindows.text = " ";
                        imagenCorredorWindows.SetActive(false);
                        imagenTiradorWindows.SetActive(false);
                    }
                    if (Jugador.GetJugador().jugadorAndroid)
                    {
                        objetivoAndroid.text = " ";
                        imagenCorredorAndroid.SetActive(false);
                        imagenTiradorAndroid.SetActive(false);
                    }
                }
            }
        }
    }
    public void CheckZona4()
    {
        if (Jugador.GetJugador() != null)
        {
            if (Jugador.GetJugador().jugadorWindows)
            {
                if (objetivoWindows != null && imagenCorredorWindows != null && imagenTiradorWindows != null)
                {
                    objetivoWindows.text = cantSpawnersDestruidosPorZona[4] + "/" + cantSpawnersPorZona[4];
                    imagenCorredorWindows.SetActive(true);
                    imagenTiradorWindows.SetActive(true);
                }
                if (Jugador.GetJugador().jugadorAndroid)
                {
                    if (objetivoAndroid != null && imagenCorredorAndroid != null && imagenTiradorAndroid != null)
                    {
                        objetivoAndroid.text = cantSpawnersDestruidosPorZona[4] + "/" + cantSpawnersPorZona[4] + " " + imagenCorredorAndroid + " " + imagenTiradorAndroid;
                        imagenCorredorAndroid.SetActive(true);
                        imagenTiradorAndroid.SetActive(true);
                    }
                }
                if (cantSpawnersPorZona[4] <= cantSpawnersDestruidosPorZona[4])
                {
                    ZonaActual++;
                    if (puertas[4] != null)
                    {
                        puertas[4].SetActive(false);
                    }
                    if (Jugador.GetJugador().jugadorWindows)
                    {
                        objetivoWindows.text = " ";
                        imagenCorredorWindows.SetActive(false);
                        imagenTiradorWindows.SetActive(false);
                    }
                    if (Jugador.GetJugador().jugadorAndroid)
                    {
                        objetivoAndroid.text = " ";
                        imagenCorredorAndroid.SetActive(false);
                        imagenTiradorAndroid.SetActive(false);
                    }
                }
            }
        }
    }
    //MISMO CODIGO CAMBIO LOS SUB INDICES DE LOS ARRAYS(PARA COPIARLO Y GENERAR EL SIGUIENTE CHECKZONA COPIAR PEGAR Y CAMBIAR ESTOS SUB INDICES)
}
