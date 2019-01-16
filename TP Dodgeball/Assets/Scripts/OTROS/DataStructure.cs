using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class DataStructure : MonoBehaviour {

    // Use this for initialization
    [HideInInspector]
    public static DataStructure auxiliaryDataStructure;
    [HideInInspector]
    public bool save;
    [HideInInspector]
    public bool once= true;
    [HideInInspector]
    public int dificulty;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (auxiliaryDataStructure == null)
        {
            auxiliaryDataStructure = this;
        }
        else if (auxiliaryDataStructure != null)
        {
            this.gameObject.SetActive(false);
        }
    }
    public static DataStructure GetAuxiliaryDataStructure()
    {
        return auxiliaryDataStructure;
    }
    public struct DataLoadingLevel
    {
        public int I_levelLoad;
        public string S_levelLoad;
    }
    
    public struct PlayerData
    {
        public bool playerWindows;
        public bool playerAndroid;
        public int TOP_AMMO;// = 500
        public float life;
        public float maxLife;
        public float armor;
        public int ballType;
        public int score;
        public int ammoIceBall;
        public int ammoFireBall;
        public int ammoFragmentBall;
        public int ammoDanceBall;
        public int ammoExplociveBall;
        public bool powerUpAddLife;
        public bool powerUpChalecoAntiGolpes;
        public bool powerUpDobleDamage;
        public bool Immune;
        public bool doblePoints;
        public bool InstaKill;
        public bool activeInstaKill;
        public float countImmune;
        public float countDoblePoints;
        public float countInstaKill;
        public float dileyActive;
        public int opportunities;
        public int downcastEnemies;
    }

    [HideInInspector]
    public PlayerData playerData;
    [HideInInspector]
    public DataLoadingLevel levelData;

    public void SetLevel(int level)
    {
        levelData.I_levelLoad = level;
    }
    public void SetLevel(string level)
    {
        levelData.S_levelLoad = level;
    }
    public int I_GetLevel()
    {
        return levelData.I_levelLoad;
    }
    public string S_GetLevel()
    {
        return levelData.S_levelLoad;
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(levelData.I_levelLoad);
    }
    public void SetPlayerData(Player player)
    {
        playerData.TOP_AMMO = player.GetTOPAMMO();
        playerData.life = player.life;
        playerData.maxLife = player.maxLife;
        playerData.armor = player.armor;
        playerData.ballType = player.ballType;
        playerData.score = player.GetScore();
        playerData.ammoIceBall = player.GetAmmoIceBall();
        playerData.ammoFireBall = player.GetAmmoFireBall();
        playerData.ammoFragmentBall = player.GetAmmoFragmentBall();
        playerData.ammoDanceBall = player.GetAmmoDanceBall();
        playerData.ammoExplociveBall = player.GetAmmoExplociveBall();
        playerData.powerUpAddLife = player.GetPowerUpAddLife();
        if (Player.GetPlayer() != null)
        {
            if(Player.GetPlayer().armor> 0)
            {
                playerData.powerUpChalecoAntiGolpes = true;
            }
            else
            {
                playerData.powerUpChalecoAntiGolpes = false;
            }
        }
        playerData.powerUpDobleDamage = player.GetpowerUpDobleDamage();
        playerData.Immune = player.GetImmune();
        playerData.doblePoints = player.GetDoblePoints();
        playerData.InstaKill = player.GetInstaKill();
        playerData.activeInstaKill = player.GetActiveInstaKill();
        playerData.countImmune = player.GetCountImmune();
        playerData.countDoblePoints = player.GetCountDoblePoints();
        playerData.countInstaKill = player.GetCountInstaKill();
        playerData.dileyActive = player.GetDileyActive();
        playerData.opportunities = player.opportunities;
        playerData.playerWindows = player.playerWindows;
        playerData.playerAndroid = player.playerAndroid;
        //DatosJugador.cantEnemigosAbatidos = jugador.cantAbatidos;
    }

    public void SetPlayerValues(Player player)
    {
        player.SetTOPAMMO(playerData.TOP_AMMO);
        player.life = playerData.life;
        player.maxLife = playerData.maxLife;
        player.armor = playerData.armor;
        player.ballType = playerData.ballType;
        player.SetScore(playerData.score);
        player.SetAmmoIceBall(playerData.ammoIceBall);
        player.SetAmmoFireBall(playerData.ammoFireBall);
        player.SetAmmoExplociveBall(playerData.ammoFragmentBall);
        player.SetAmmoDanceBall(playerData.ammoDanceBall);
        player.SetAmmoExplociveBall(playerData.ammoExplociveBall);
        player.SetPowerUpArmor(playerData.powerUpChalecoAntiGolpes);
        player.SetPowerUpDobleDamage(playerData.powerUpDobleDamage);
        player.SetImmune(playerData.Immune);
        player.SetDoblePoints(playerData.doblePoints);
        player.SetInstaKill(playerData.InstaKill);
        player.SetActiveInstaKill(playerData.activeInstaKill);
        player.SetCountImmune(playerData.countImmune);
        player.SetCountDoblePoints(playerData.countDoblePoints);
        player.SetCountInstaKill(playerData.countInstaKill);
        player.SetDileyActive(playerData.dileyActive);
        player.opportunities = playerData.opportunities;
        player.countKilled = playerData.downcastEnemies;
        player.playerAndroid = playerData.playerAndroid;
        player.playerWindows = playerData.playerWindows;
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
