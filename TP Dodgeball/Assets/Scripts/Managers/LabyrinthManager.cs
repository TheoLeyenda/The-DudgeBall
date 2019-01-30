using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabyrinthManager : MonoBehaviour
{
    private int dificulty;
    public int[] SpawnersQuantityByArea;
    public int[] SpawnersDestroyedByArea;
    public GameObject[] doors;
    public Text objetiveWindows;
    public GameObject imageShooterWindows;
    public GameObject imageRunnerWindows;
    public Text objetiveAndroid;
    public GameObject imageShooterAndroid;
    public GameObject imageRunnerAndroid;
    public SpawnerEnemy[] spawners;
    public SpawnerEnemy[] spawnerRunner;
    public SpawnerEnemy[] spawnerShooter;
    public TimeOnPlay timeGameAndroid;
    public TimeOnPlay timeGameWindows;
    public Pool[] poolSpawners;
    public int currentZone;

    public static LabyrinthManager instanceLabyrinthManager;
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
    public static LabyrinthManager GetLaberintoManager()
    {
        return instanceLabyrinthManager;
    }
    void Start()
    {
        SpawnersDestroyedByArea = new int[SpawnersQuantityByArea.Length];

        dificulty = DataStructure.auxiliaryDataStructure.dificulty;
        if (timeGameAndroid != null && Player.InstancePlayer.playerAndroid)
        {
            for (int i = 0; i < spawnerRunner.Length; i++)
            {
                spawnerRunner[i].enemySpeed = 3;
            }
            for (int i = 0; i < spawnerShooter.Length; i++)
            {
                spawnerShooter[i].enemySpeed = 2;
            }
            switch (dificulty)
            {
                case 1:
                    for (int i = 0; i < spawners.Length; i++)
                    {
                        spawners[i].dileyCreation = 50;
                    }
                    for (int i = 0; i < poolSpawners.Length; i++)
                    {
                        poolSpawners[i].count = 10;
                    }
                    timeGameAndroid.minutes = 30f;
                    break;
                case 2:
                    for (int i = 0; i < spawners.Length; i++)
                    {
                        spawners[i].dileyCreation = 40;
                    }
                    for (int i = 0; i < poolSpawners.Length; i++)
                    {
                        poolSpawners[i].count = 15;
                    }
                    timeGameAndroid.minutes = 20f;
                    break;
                case 3:
                    for (int i = 0; i < spawners.Length; i++)
                    {
                        spawners[i].dileyCreation = 25;
                    }
                    for (int i = 0; i < poolSpawners.Length; i++)
                    {
                        poolSpawners[i].count = 20;
                    }
                    timeGameAndroid.minutes = 10f;
                    break;
            }
        }
        if (timeGameWindows != null && Player.InstancePlayer.playerWindows)
        {
            for (int i = 0; i < spawnerRunner.Length; i++)
            {
                spawnerRunner[i].enemySpeed = 4;
            }
            for (int i = 0; i < spawnerShooter.Length; i++)
            {
                spawnerShooter[i].enemySpeed = 2.5f;
            }
            switch (dificulty)
            {
                case 1:
                    for (int i = 0; i < spawners.Length; i++)
                    {
                        spawners[i].dileyCreation = 50;
                    }
                    timeGameWindows.minutes = 30f;
                    break;
                case 2:
                    for (int i = 0; i < spawners.Length; i++)
                    {
                        spawners[i].dileyCreation = 40;
                    }
                    timeGameWindows.minutes = 20f;
                    break;
                case 3:
                    for (int i = 0; i < spawners.Length; i++)
                    {
                        spawners[i].dileyCreation = 30;
                    }
                    timeGameWindows.minutes = 10f;
                    break;
            }
        }
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
        if (Player.GetPlayer() != null)
        {
            if (Player.GetPlayer().playerWindows)
            {
                if (objetiveWindows != null && imageRunnerWindows != null && imageShooterWindows != null)
                {
                    objetiveWindows.text = SpawnersDestroyedByArea[0] + "/" + SpawnersQuantityByArea[0] + " " + imageRunnerWindows + " " + imageShooterWindows;
                    imageRunnerWindows.SetActive(true);
                    imageShooterWindows.SetActive(true);
                }
            }

            if (Player.GetPlayer().playerAndroid)
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
                if (Player.GetPlayer().playerWindows)
                {
                    objetiveWindows.text = " ";
                    imageRunnerWindows.SetActive(false);
                    imageShooterWindows.SetActive(false);
                }
                if (Player.GetPlayer().playerAndroid)
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
        if (Player.GetPlayer() != null)
        {
            if (Player.GetPlayer().playerWindows)
            {
                if (objetiveWindows != null && imageRunnerWindows != null && imageShooterWindows != null)
                {
                    objetiveWindows.text = SpawnersDestroyedByArea[1] + "/" + SpawnersQuantityByArea[1] + " " + imageRunnerWindows + " " + imageShooterWindows;
                    imageRunnerWindows.SetActive(true);
                    imageShooterWindows.SetActive(true);
                }
            }
            if (Player.GetPlayer().playerAndroid)
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
                if (Player.GetPlayer().playerWindows)
                {
                    objetiveWindows.text = " ";
                    imageRunnerWindows.SetActive(false);
                    imageShooterWindows.SetActive(false);
                }
                if (Player.GetPlayer().playerAndroid)
                {
                    objetiveAndroid.text = " ";
                    imageRunnerAndroid.SetActive(false);
                    imageShooterAndroid.SetActive(false);
                }
            }
        }
    }
    public void CheckZone2()
    {
        if (Player.GetPlayer() != null)
        {
            if (Player.GetPlayer().playerWindows)
            {
                if (objetiveWindows != null && imageRunnerWindows != null && imageShooterWindows != null)
                {
                    objetiveWindows.text = SpawnersDestroyedByArea[2] + "/" + SpawnersQuantityByArea[2] + " " + imageRunnerWindows + " " + imageShooterWindows;
                    imageRunnerWindows.SetActive(true);
                    imageShooterWindows.SetActive(true);
                }
            }
            if (Player.GetPlayer().playerAndroid)
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
                if (Player.GetPlayer().playerWindows)
                {
                    objetiveWindows.text = " ";
                    imageRunnerWindows.SetActive(false);
                    imageShooterWindows.SetActive(false);
                }
                if (Player.GetPlayer().playerAndroid)
                {
                    objetiveAndroid.text = " ";
                    imageRunnerAndroid.SetActive(false);
                    imageShooterAndroid.SetActive(false);
                }
            }
        }
    }
    public void CheckZone3()
    {
        if (Player.GetPlayer() != null)
        {
            if (Player.GetPlayer().playerAndroid)
            {
                if (objetiveAndroid != null && imageRunnerAndroid != null && imageShooterAndroid != null)
                {
                    objetiveAndroid.text = SpawnersDestroyedByArea[3] + "/" + SpawnersQuantityByArea[3] + " " + imageRunnerAndroid + " " + imageShooterAndroid;
                    imageRunnerAndroid.SetActive(true);
                    imageShooterAndroid.SetActive(true);
                }
            }
            if (Player.GetPlayer().playerWindows)
            {
                if (objetiveWindows != null && imageRunnerWindows != null && imageShooterWindows != null)
                {
                    objetiveWindows.text = SpawnersDestroyedByArea[3] + "/" + SpawnersQuantityByArea[3] + " " + imageRunnerWindows + " " + imageShooterWindows;
                    imageRunnerWindows.SetActive(true);
                    imageShooterWindows.SetActive(true);
                }
            }
            if (SpawnersQuantityByArea[3] <= SpawnersDestroyedByArea[3])
            {
                currentZone++;
                if (doors[3] != null)
                {
                    doors[3].SetActive(false);
                }
                if (Player.GetPlayer().playerWindows)
                {
                    objetiveWindows.text = " ";
                    imageRunnerWindows.SetActive(false);
                    imageShooterWindows.SetActive(false);
                }
                if (Player.GetPlayer().playerAndroid)
                {
                    objetiveAndroid.text = " ";
                    imageRunnerAndroid.SetActive(false);
                    imageShooterAndroid.SetActive(false);
                }
            }
        }
    }
    public void CheckZone4()
    {
        if (Player.GetPlayer() != null)
        {
            if (Player.GetPlayer().playerAndroid)
            {
                if (objetiveAndroid != null && imageRunnerAndroid != null && imageShooterAndroid != null)
                {
                    objetiveAndroid.text = SpawnersDestroyedByArea[4] + "/" + SpawnersQuantityByArea[4] + " " + imageRunnerAndroid + " " + imageShooterAndroid;
                    imageRunnerAndroid.SetActive(true);
                    imageShooterAndroid.SetActive(true);
                }
            }
            if (Player.GetPlayer().playerWindows)
            {
                if (objetiveWindows != null && imageRunnerWindows != null && imageShooterWindows != null)
                {
                    objetiveWindows.text = SpawnersDestroyedByArea[4] + "/" + SpawnersQuantityByArea[4];
                    imageRunnerWindows.SetActive(true);
                    imageShooterWindows.SetActive(true);
                }
            }
            if (SpawnersQuantityByArea[4] <= SpawnersDestroyedByArea[4])
            {
                currentZone++;
                if (doors[4] != null)
                {
                    doors[4].SetActive(false);
                }
                if (Player.GetPlayer().playerWindows)
                {
                    objetiveWindows.text = " ";
                    imageRunnerWindows.SetActive(false);
                    imageShooterWindows.SetActive(false);
                }
                if (Player.GetPlayer().playerAndroid)
                {
                    objetiveAndroid.text = " ";
                    imageRunnerAndroid.SetActive(false);
                    imageShooterAndroid.SetActive(false);
                }
            }
        }
    }
}
