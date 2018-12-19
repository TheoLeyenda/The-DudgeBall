using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class GameManager : MonoBehaviour {

    // Use this for initialization
    public Text TextRoundWindows;
    public Text TextRoundAndroid;
    [HideInInspector]
    public int enemyAmountOnScreen;
    public SwitchMaterialDoor changeMaterial;
    public SpawnerEnemy[] spawnersEnemy;
    public StaticShooter[] turrets;
    public SpawnerTrap[] traps;
    private bool start;
    private static GameManager instanceGameManager;
    private int Round;
    public bool survival;
    public bool history;
    public bool checkRound;
    public bool checkVictory;
    public int RoundVictory;
    private int deaths;
    public int deathLimit;
    public string currentMap;
    public string nextMap;
    private bool enterRound;
    private bool victory;
    private int turretsAmountOnScreen;
    private bool oncePerRound;
    [HideInInspector]
    public bool nextLevel = false;
    [HideInInspector]
    public bool pause;
    public GameObject door;
    public GameObject heart;
    public GameObject armor;
    public GameObject checkPoint;

    public static GameManager GetGameManager()
    {
        return instanceGameManager;
    }
    private void Awake()
    {
        //entrarRonda = true;
        Round = 1;
        deaths = 0;
        oncePerRound = false;
        if (instanceGameManager == null)
        {
            instanceGameManager = this;
        }
        else if (instanceGameManager != null)
        {
            this.gameObject.SetActive(false);            
        }
    }
    private void Start()
    {
        enemyAmountOnScreen = 0;
        if (!history && survival)
        {
            if (currentMap == "Arena(Supervivencia)")
            {
                if (spawnersEnemy[4] != null && spawnersEnemy[5] != null && spawnersEnemy[6] != null && spawnersEnemy[7] != null)
                {
                    spawnersEnemy[4].gameObject.SetActive(false);
                    spawnersEnemy[5].gameObject.SetActive(false);
                    spawnersEnemy[6].gameObject.SetActive(false);
                    spawnersEnemy[7].gameObject.SetActive(false);
                }
            }
        }
        if (turrets != null)
        {
            for(int i = 0; i<turrets.Length; i++)
            {
                if(turrets[i] != null)
                {
                    turrets[i].gameObject.SetActive(false);
                }
            }
        }
    }


    // Update is called once per frame
    void Update () {
        CheckTurrets();
        //VerificarVictoria();
        if (pause)
        {
            Time.timeScale = 0;
        }
        if(!pause)
        {
            Time.timeScale = 1;
        }
        if (survival)
        {

            for (int i = 0; i < spawnersEnemy.Length; i++)
            {
                
                if (enemyAmountOnScreen <= 0 && turretsAmountOnScreen <= 0)
                {
                    
                    spawnersEnemy[i].SetInWorking(true);
                    if (enterRound)
                    {
                        oncePerRound = true;
                        AddRound();
                        enterRound = false;
                    }
                }
                if (currentMap == "Arena(Historia)" && oncePerRound)
                {
                    if (heart != null && armor != null)
                    {
                        heart.SetActive(false);
                        armor.SetActive(false);
                    }
                    CheckVictory();
                    if (!victory)
                    {
                        ActiveTurrets();
                    }
                    oncePerRound = false;
                    if(Round == 2)
                    {
                        for (int j = 0; j < traps.Length; j++)
                        {
                            if (traps[j] != null)
                            {
                                traps[j].gameObject.SetActive(true);
                            }
                        }
                    }
                    if(Round == 4)
                    {
                        spawnersEnemy[4].gameObject.SetActive(true);
                        spawnersEnemy[5].gameObject.SetActive(true);
                        spawnersEnemy[6].gameObject.SetActive(false);
                        spawnersEnemy[7].gameObject.SetActive(false);
                    }
                    if(Round == 5)
                    {
                        spawnersEnemy[4].gameObject.SetActive(false);
                        spawnersEnemy[5].gameObject.SetActive(false);
                        spawnersEnemy[6].gameObject.SetActive(true);
                        spawnersEnemy[7].gameObject.SetActive(true);
                    }
                    if(Round == 6)
                    {
                        spawnersEnemy[4].gameObject.SetActive(true);
                        spawnersEnemy[5].gameObject.SetActive(true);
                        spawnersEnemy[6].gameObject.SetActive(true);
                        spawnersEnemy[7].gameObject.SetActive(true);
                    }
                }
            }
        }

        if(currentMap == "Laberinto")
        {
            if (TextRoundWindows != null)
            {
                TextRoundWindows.text = "";
            }

        }
        
        if(history && !survival && nextLevel == true)
        {
            ShowRound();
            /*if(muertes >= limiteMuertes)
            {
                SceneManager.LoadScene(mapaSiguiente);
            }*/
        }
        if(!history && survival)
        {
            ShowRound();
            if (currentMap == "Arena(Supervivencia)")
            {
                if (spawnersEnemy[4] != null && spawnersEnemy[5] != null && spawnersEnemy[6] != null && spawnersEnemy[7] != null)
                {
                    if (Round % 5 == 0)
                    {
                        spawnersEnemy[4].gameObject.SetActive(true);
                        spawnersEnemy[5].gameObject.SetActive(true);
                        spawnersEnemy[6].gameObject.SetActive(true);
                        spawnersEnemy[7].gameObject.SetActive(true);
                    }
                    if(Round % 5 != 0)
                    {
                        spawnersEnemy[4].gameObject.SetActive(false);
                        spawnersEnemy[5].gameObject.SetActive(false);
                        spawnersEnemy[6].gameObject.SetActive(false);
                        spawnersEnemy[7].gameObject.SetActive(false);
                    }
                }
            }
        }
	}
    public void ActiveTurrets()
    {
        if (Round == 3)
        {
            turrets[0].gameObject.SetActive(true);
            turrets[1].gameObject.SetActive(true);
        }
        if (Round == 4)
        {
            turrets[0].gameObject.SetActive(true);
            turrets[1].gameObject.SetActive(true);
            turrets[2].gameObject.SetActive(true);
            turrets[3].gameObject.SetActive(true);
        }
        if (Round == 5)
        {
            turrets[4].gameObject.SetActive(true);
            turrets[5].gameObject.SetActive(true);
            turrets[6].gameObject.SetActive(true);
            turrets[7].gameObject.SetActive(true);
            turrets[8].gameObject.SetActive(true);
            turrets[9].gameObject.SetActive(true);
            turrets[10].gameObject.SetActive(true);
            turrets[11].gameObject.SetActive(true);
        }
        if (Round >= 6)
        {
            turrets[0].gameObject.SetActive(true);
            turrets[1].gameObject.SetActive(true);
            turrets[2].gameObject.SetActive(true);
            turrets[3].gameObject.SetActive(true);
            turrets[4].gameObject.SetActive(true);
            turrets[5].gameObject.SetActive(true);
            turrets[6].gameObject.SetActive(true);
            turrets[7].gameObject.SetActive(true);
            turrets[8].gameObject.SetActive(true);
            turrets[9].gameObject.SetActive(true);
            turrets[10].gameObject.SetActive(true);
            turrets[11].gameObject.SetActive(true);
        }
    }
    public bool GetVictory()
    {
        return victory;
    }
    public void CheckVictory()
    {
        if(Round >= RoundVictory)
        {
            victory = true;
            heart.SetActive(true);
            armor.SetActive(true);
            for (int i = 0; i < spawnersEnemy.Length; i++)
            {
                if(spawnersEnemy[i] != null)
                {
                    spawnersEnemy[i].SetInWorking(false);
                    spawnersEnemy[i].gameObject.SetActive(false);
                }
            }
            changeMaterial.SwitchMaterial();
            door.GetComponent<BoxCollider>().enabled = true;
            checkPoint.SetActive(true);
        }
    }
    public void CheckTurrets()
    {
        if (turrets != null)
        {
            turretsAmountOnScreen = turrets.Length;
            for (int i = 0; i < turrets.Length; i++)
            {
                if (turrets[i] != null)
                {
                    if (turrets[i].gameObject.activeSelf == false && turretsAmountOnScreen > 0)
                    {
                        turretsAmountOnScreen--;
                    }
                }
            }
        }
    }
    public void ShowRound()
    {
        if (TextRoundWindows != null)
        {
            TextRoundWindows.text = "RONDA: " + Round;
        }
        if (TextRoundAndroid != null)
        {
            TextRoundAndroid.text = "RONDA: " + Round;
        }
    }
    public void SetEnemyAmoutOnScreen(int _enemyAmountOnScreen)
    {
        enemyAmountOnScreen = _enemyAmountOnScreen;
    }
    public void AddEnemyAmoutOnScreen()
    {
        enemyAmountOnScreen++;
    }
    public void SubstractEnemyAmountOnScreen()
    {
        enemyAmountOnScreen--;
    }
    public int GetEnemyAmountOnScreen()
    {
        return enemyAmountOnScreen;
    }
    public void AddRound()
    {
        Round = Round + 1;
    }
    public int GetRound()
    {
        return Round;
    }
    public void SetRound(int _Round)
    {
        Round = _Round;
    }
    public void AddDeath()
    {
        deaths = deaths + 1;
    }
    public void SetEntrarRonda(bool _enterRound)
    {
        enterRound = _enterRound;
    }
}

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
