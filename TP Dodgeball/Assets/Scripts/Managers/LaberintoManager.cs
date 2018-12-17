using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class LaberintoManager : MonoBehaviour
{

    public int[] SpawnersQuantityByArea;
    public int[] SpawnersDestroyedByArea;
    public GameObject[] doors;
    public Text objetiveWindows;
    public GameObject imageShooterWindows;
    public GameObject imageRunnerWindows;
    public Text objetiveAndroid;
    public GameObject imageShooterAndroid;
    public GameObject imageRunnerAndroid;
    public int currentZone;

    public static LaberintoManager instanceLabyrinthManager;
    // Use this for initialization
    private void Awake()
    {
        //entrarRonda = true;

        if (instanceLabyrinthManager == null)
        {
            instanceLabyrinthManager = this;
        }
        else if (instanceLabyrinthManager != null)
        {
            this.gameObject.SetActive(false);
        }
    }
    public static LaberintoManager GetLaberintoManager()
    {
        return instanceLabyrinthManager;
    }


    void Start()
    {
        SpawnersDestroyedByArea = new int[SpawnersQuantityByArea.Length];
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentZone)
        {
            case 0:
                CheckZone0();
                break;
            case 1:
                CheckZone1();
                break;
            case 2:
                CheckZone2();
                break;
            case 3:
                CheckZone3();
                break;
            case 4:
                CheckZone4();
                break;
        }

    }
    public void AddSpawnersDestroyedByArea(int zone)
    {
        SpawnersDestroyedByArea[zone]++;
    }
    public void CheckZone0()
    {
        if (Jugador.GetPlayer() != null)
        {
            if (Jugador.GetPlayer().playerWindows)
            {
                if (objetiveWindows != null && imageRunnerWindows != null && imageShooterWindows != null)
                {
                    objetiveWindows.text = SpawnersDestroyedByArea[0] + "/" + SpawnersQuantityByArea[0] + " " + imageRunnerWindows + " " + imageShooterWindows;
                    imageRunnerWindows.SetActive(true);
                    imageShooterWindows.SetActive(true);
                }
            }

            if (Jugador.GetPlayer().playerAndroid)
            {
                if (objetiveAndroid != null && imageRunnerAndroid != null && imageShooterAndroid != null)
                {
                    objetiveAndroid.text = SpawnersDestroyedByArea[0] + "/" + SpawnersQuantityByArea[0] + " " + imageRunnerAndroid + " " + imageShooterAndroid;
                    imageRunnerAndroid.SetActive(true);
                    imageShooterAndroid.SetActive(true);
                }
            }
            if (SpawnersQuantityByArea[0] <= SpawnersDestroyedByArea[0])
            {
                currentZone++;
                if (doors[0] != null)
                {
                    doors[0].SetActive(false);
                }
                if (Jugador.GetPlayer().playerWindows)
                {
                    objetiveWindows.text = " ";
                    imageRunnerWindows.SetActive(false);
                    imageShooterWindows.SetActive(false);
                }
                if (Jugador.GetPlayer().playerAndroid)
                {
                    objetiveAndroid.text = " ";
                    imageRunnerAndroid.SetActive(false);
                    imageShooterAndroid.SetActive(false);
                }
            }
        }
    }
    public void CheckZone1()
    {
        if (Jugador.GetPlayer() != null)
        {
            if (Jugador.GetPlayer().playerWindows)
            {
                if (objetiveWindows != null && imageRunnerWindows != null && imageShooterWindows != null)
                {
                    objetiveWindows.text = SpawnersDestroyedByArea[1] + "/" + SpawnersQuantityByArea[1] + " " + imageRunnerWindows + " " + imageShooterWindows;
                    imageRunnerWindows.SetActive(true);
                    imageShooterWindows.SetActive(true);
                }
                if (Jugador.GetPlayer().playerAndroid)
                {
                    if (objetiveAndroid != null && imageRunnerAndroid != null && imageShooterAndroid != null)
                    {
                        objetiveAndroid.text = SpawnersDestroyedByArea[1] + "/" + SpawnersQuantityByArea[1] + " " + imageRunnerAndroid + " " + imageShooterAndroid;
                        imageRunnerAndroid.SetActive(true);
                        imageShooterAndroid.SetActive(true);
                    }
                }
                if (SpawnersQuantityByArea[1] <= SpawnersDestroyedByArea[1])
                {
                    currentZone++;
                    if (doors[1] != null)
                    {
                        doors[1].SetActive(false);
                    }
                    if (Jugador.GetPlayer().playerWindows)
                    {
                        objetiveWindows.text = " ";
                        imageRunnerWindows.SetActive(false);
                        imageShooterWindows.SetActive(false);
                    }
                    if (Jugador.GetPlayer().playerAndroid)
                    {
                        objetiveAndroid.text = " ";
                        imageRunnerAndroid.SetActive(false);
                        imageShooterAndroid.SetActive(false);
                    }
                }
            }
        }
    }
    public void CheckZone2()
    {
        if (Jugador.GetPlayer() != null)
        {
            if (Jugador.GetPlayer().playerWindows)
            {
                if (objetiveWindows != null && imageRunnerWindows != null && imageShooterWindows != null)
                {
                    objetiveWindows.text = SpawnersDestroyedByArea[2] + "/" + SpawnersQuantityByArea[2] + " " + imageRunnerWindows + " " + imageShooterWindows;
                    imageRunnerWindows.SetActive(true);
                    imageShooterWindows.SetActive(true);
                }
                if (Jugador.GetPlayer().playerAndroid)
                {
                    if (objetiveAndroid != null && imageRunnerAndroid != null && imageShooterAndroid != null)
                    {
                        objetiveAndroid.text = SpawnersDestroyedByArea[2] + "/" + SpawnersQuantityByArea[2] + " " + imageRunnerAndroid + " " + imageShooterAndroid;
                        imageRunnerAndroid.SetActive(true);
                        imageShooterAndroid.SetActive(true);
                    }
                }
                if (SpawnersQuantityByArea[2] <= SpawnersDestroyedByArea[2])
                {
                    currentZone++;
                    if (doors[2] != null)
                    {
                        doors[2].SetActive(false);
                    }
                    if (Jugador.GetPlayer().playerWindows)
                    {
                        objetiveWindows.text = " ";
                        imageRunnerWindows.SetActive(false);
                        imageShooterWindows.SetActive(false);
                    }
                    if (Jugador.GetPlayer().playerAndroid)
                    {
                        objetiveAndroid.text = " ";
                        imageRunnerAndroid.SetActive(false);
                        imageShooterAndroid.SetActive(false);
                    }
                }
            }
        }
    }
    public void CheckZone3()
    {
        if (Jugador.GetPlayer() != null)
        {
            if (Jugador.GetPlayer().playerWindows)
            {
                if (objetiveWindows != null && imageRunnerWindows != null && imageShooterWindows != null)
                {
                    objetiveWindows.text = SpawnersDestroyedByArea[3] + "/" + SpawnersQuantityByArea[3] + " " + imageRunnerWindows + " " + imageShooterWindows;
                    imageRunnerWindows.SetActive(true);
                    imageShooterWindows.SetActive(true);
                }
                if (Jugador.GetPlayer().playerAndroid)
                {
                    if (objetiveAndroid != null && imageRunnerAndroid != null && imageShooterAndroid != null)
                    {
                        objetiveAndroid.text = SpawnersDestroyedByArea[3] + "/" + SpawnersQuantityByArea[3] + " " + imageRunnerAndroid + " " + imageShooterAndroid;
                        imageRunnerAndroid.SetActive(true);
                        imageShooterAndroid.SetActive(true);
                    }
                }
                if (SpawnersQuantityByArea[3] <= SpawnersDestroyedByArea[3])
                {
                    currentZone++;
                    if (doors[3] != null)
                    {
                        doors[3].SetActive(false);
                    }
                    if (Jugador.GetPlayer().playerWindows)
                    {
                        objetiveWindows.text = " ";
                        imageRunnerWindows.SetActive(false);
                        imageShooterWindows.SetActive(false);
                    }
                    if (Jugador.GetPlayer().playerAndroid)
                    {
                        objetiveAndroid.text = " ";
                        imageRunnerAndroid.SetActive(false);
                        imageShooterAndroid.SetActive(false);
                    }
                }
            }
        }
    }
    public void CheckZone4()
    {
        if (Jugador.GetPlayer() != null)
        {
            if (Jugador.GetPlayer().playerWindows)
            {
                if (objetiveWindows != null && imageRunnerWindows != null && imageShooterWindows != null)
                {
                    objetiveWindows.text = SpawnersDestroyedByArea[4] + "/" + SpawnersQuantityByArea[4];
                    imageRunnerWindows.SetActive(true);
                    imageShooterWindows.SetActive(true);
                }
                if (Jugador.GetPlayer().playerAndroid)
                {
                    if (objetiveAndroid != null && imageRunnerAndroid != null && imageShooterAndroid != null)
                    {
                        objetiveAndroid.text = SpawnersDestroyedByArea[4] + "/" + SpawnersQuantityByArea[4] + " " + imageRunnerAndroid + " " + imageShooterAndroid;
                        imageRunnerAndroid.SetActive(true);
                        imageShooterAndroid.SetActive(true);
                    }
                }
                if (SpawnersQuantityByArea[4] <= SpawnersDestroyedByArea[4])
                {
                    currentZone++;
                    if (doors[4] != null)
                    {
                        doors[4].SetActive(false);
                    }
                    if (Jugador.GetPlayer().playerWindows)
                    {
                        objetiveWindows.text = " ";
                        imageRunnerWindows.SetActive(false);
                        imageShooterWindows.SetActive(false);
                    }
                    if (Jugador.GetPlayer().playerAndroid)
                    {
                        objetiveAndroid.text = " ";
                        imageRunnerAndroid.SetActive(false);
                        imageShooterAndroid.SetActive(false);
                    }
                }
            }
        }
    }
    //MISMO CODIGO CAMBIO LOS SUB INDICES DE LOS ARRAYS(PARA COPIARLO Y GENERAR EL SIGUIENTE CHECKZONA COPIAR PEGAR Y CAMBIAR ESTOS SUB INDICES)
}

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
