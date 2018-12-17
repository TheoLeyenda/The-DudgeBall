using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
public class Jugador : MonoBehaviour {

    // Use this for initialization
    [HideInInspector]
    public bool pause;
    public static Jugador InstancePlayer;
    private int TOP_AMMO = 500;
    public float life;
    public float maxLife;
    public float armor;
    [HideInInspector]
    public int ballType;
    private int score;
    [HideInInspector]
    public bool count;
    public Enemigo runner;
    public Enemigo shooter;
    public Enemigo staticShooter;
    //public Enemigo Danio;
    public Pinchos skewers;
    public bool playerWindows;
    public bool playerAndroid;
    public Rigidbody rigJugador;
    public int opportunities;
    public Transform posRespawn;

    [HideInInspector]
    public int countKilled = 0;

    private float AdditionalDamageCommonBall;
    private float AdditionalDamageIceBall;
    private float AdditionalDamageFireBall;
    private float AdditionalDamageExplociveBall;
    private float AditionalDamageMiniBalls;

    public Text textLife;
    public Text textScore;
    public Text textArmor;
    public Text TextOpportunities;

    private int AmmoIceBall = 0;
    private int AmmoFireBall = 0;
    private int AmmoFragmentBall = 0;
    private int AmmoDanceBall = 0;
    private int AmmoExplociveBall = 0;

    public Text textAmmoIceBall;
    public Text textAmmoFireBall;
    public Text textAmmoFragmentBall;
    public Text textAmmoDanceBall;
    public Text textAmmoExplociveBall;

    public GameObject lockedIce;
    public GameObject lockedFire;
    public GameObject lockedDance;
    public GameObject lockedFragment;
    public GameObject lockedExplosive;

    public GameObject unlockedIce;
    public GameObject unlockedFire;
    public GameObject unlockedDance;
    public GameObject unlockedFragment;
    public GameObject unlockedExplocive;

    private bool powerUpAddLife;
    public bool powerUpArmor;
    private bool powerUpDobleDamage;

    public GameObject logoArmor;
    public GameObject logoDobleDamage;

    public bool inStore;

    public GameObject logoImulnerability;
    public GameObject logoDoblePoints;
    public GameObject logoInstaKill;
    //private bool powerUpDoblePelota;

    private bool Immune;
    private bool DoblePoints;
    private bool InstaKill;
    private bool activeInstaKill;
    //private float auxContInmune;
    private float countImmune;
    private float countDoblePoints;
    private float countInstaKill;
    private float dileyActive;
    private void Awake()
    {
        InstancePlayer = this;
    }
    public void AddedLife()
    {
        powerUpAddLife = true;
    }
    public void Armor()
    {
        powerUpArmor = true;
        if(logoArmor != null)
        {
            logoArmor.SetActive(true);
        }
    }
    public void DobleDamage()
    {
        powerUpDobleDamage = true;
        if(logoDobleDamage != null)
        {
            logoDobleDamage.SetActive(true);
        }
    }
    public void SubtractScore(int _score)
    {
        score = score - _score;
    }
    void Start() {
        
        inStore = false;
        countImmune = 0;
        AdditionalDamageCommonBall = 0;
        AditionalDamageMiniBalls = 0;
        AdditionalDamageExplociveBall = 0;
        AdditionalDamageFireBall = 0;
        AdditionalDamageIceBall = 0;
        //blindaje = 0;
        if (textArmor != null)
        {
            textArmor.gameObject.SetActive(false);
        }
        Immune = false;
        count = true;
        score = 0;
        textLife.text = ""+((int)life);
        InstancePlayer = this;
        ballType = 1;
        AmmoIceBall = 0;
        if (DataStructure.GetEstructuraDatosAuxiliares() != null)
        {
            if (DataStructure.GetEstructuraDatosAuxiliares().once)
            {
                DataStructure.GetEstructuraDatosAuxiliares().once = false;
            }
            else
            {
                DataStructure.GetEstructuraDatosAuxiliares().SetPlayerValues(Jugador.GetPlayer());
           }
        }
    }

    // Update is called once per frame
    public void ControlCursor()
    {
        if (inStore || playerAndroid || pause)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (!inStore && !playerAndroid && !pause)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if(playerAndroid && Input.GetKey(KeyCode.Mouse0))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    void Update() {
      
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
            //LeanRodrigez98
        }
        ControlCursor();
        if(DoblePoints)
        {
            logoDoblePoints.SetActive(true);
        }
        if(DoblePoints == false)
        {
            logoDoblePoints.SetActive(false);
        }
        if (Immune == true)
        {
            logoImulnerability.SetActive(true);
            countImmune = countImmune - Time.deltaTime;
            if(countImmune <= 0)
            {
                Immune = false;
            }
        }
        if(Immune == false)
        {
            logoImulnerability.SetActive(false);
        }
        if(InstaKill == false)
        {
            logoInstaKill.SetActive(false);
        }
        if(DoblePoints == true)
        {
            countDoblePoints = countDoblePoints - Time.deltaTime;
            if (countDoblePoints <= 0)
            {
                DoblePoints = false;
            }
        }
        if(InstaKill == true)
        {
            logoInstaKill.SetActive(true);
            countInstaKill = countInstaKill - Time.deltaTime;
            if(countInstaKill <= 0)
            {
                InstaKill = false;
            }
        }
        if (AmmoDanceBall > 0)
        {
            lockedDance.SetActive(false);
            unlockedDance.SetActive(true);
        }
        if(AmmoFireBall > 0)
        {
            lockedFire.SetActive(false);
            unlockedFire.SetActive(true);
        }
        if(AmmoIceBall > 0)
        {
            lockedIce.SetActive(false);
            unlockedIce.SetActive(true);
        }
        if(AmmoExplociveBall > 0)
        {
            lockedExplosive.SetActive(false);
            unlockedExplocive.SetActive(true);
        }
        if(AmmoFragmentBall > 0)
        {
            lockedFragment.SetActive(false);
            unlockedFragment.SetActive(true);
        }
        if(armor <= 0)
        {
            armor = 0;
            if(logoArmor != null)
            {
                logoArmor.SetActive(false);
            }
        }
        if(powerUpAddLife)
        {
            life = maxLife;
            powerUpAddLife = false;

        }
        if(powerUpArmor)
        {
            logoArmor.SetActive(true);
            textArmor.gameObject.SetActive(true);
            armor = 100;
            powerUpArmor = false;
        }
        if(powerUpDobleDamage)
        {
            //definido en enemigo
        }
        textLife.text = "VIDA: " + (int)life;
        textScore.text = "PUNTOS: " + score;
        if (opportunities > -1 && TextOpportunities != null)
        {
            TextOpportunities.text = "OPORTUNIDADES: " + opportunities;
        }
        if(textArmor != null)
        {
            textArmor.text = "BLINDAJE:" + (int)armor;
        }
        if (life <= 0)
        {
            opportunities = opportunities - 1;
            if (life <= 0 && opportunities < 0)
            {
                SceneManager.LoadScene("GameOver");
                gameObject.SetActive(false);
            }
            else
            {
                if (posRespawn != null)
                {
                    transform.position = posRespawn.position;
                    life = 100;
                }
                else
                {
                    SceneManager.LoadScene("GameOver");
                    gameObject.SetActive(false);
                }
            }
        }
        if (textAmmoIceBall != null)
        {
            textAmmoIceBall.text = AmmoIceBall + "";
        }
        if (textAmmoFireBall != null)
        {
            textAmmoFireBall.text = AmmoFireBall + "";
        }
        if (textAmmoFragmentBall != null)
        {
            textAmmoFragmentBall.text = AmmoFragmentBall + "";
        }
        if (textAmmoDanceBall != null)
        {
            textAmmoDanceBall.text = AmmoDanceBall + "";
        }
        if (textAmmoExplociveBall != null)
        {
            textAmmoExplociveBall.text = AmmoExplociveBall + "";
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TESORO")
        {
            SceneManager.LoadScene("JUEGO COMPLETADO");
        }
        if (other.tag == "PickUpChalecoAntiGolpes")
        {
            powerUpArmor = true;
            other.gameObject.SetActive(false);
        }
        if(other.tag == "RompeObjetos")
        {
            //SceneManager.LoadScene("GameOver");
            armor = 0;
            life = 0; 
        }
        if(other.tag == "ZonaRespawn")
        {
            if (DataStructure.GetEstructuraDatosAuxiliares() != null)
            {
                
                DataStructure.GetEstructuraDatosAuxiliares().SetPlayerData(Jugador.GetPlayer());
                if (armor > 0)
                {
                    DataStructure.GetEstructuraDatosAuxiliares().playerData.armor = armor;
                    logoArmor.SetActive(true);
                    textArmor.gameObject.SetActive(true);
                }
            }
            posRespawn = other.gameObject.transform;
        }
        if(other.tag == "PoderInmune")
        {
            other.gameObject.SetActive(false);
            Immune = true;
            countImmune = 15;
        }
        if(other.tag == "DoblePuntuacion")
        {
            other.gameObject.SetActive(false);
            DoblePoints = true;
            countDoblePoints = 20;

        }
        if (other.tag == "InstaKill")
        {
            other.gameObject.SetActive(false);
            InstaKill = true;
            activeInstaKill = true;
            countInstaKill = 12;
        }
        if (other.tag == "PickUpHielo")
        {
            AmmoIceBall = AmmoIceBall + 12;
            count = true;
            if (lockedIce != null)
            {
                lockedIce.SetActive(false);
            }
            if (unlockedIce != null)
            {
                unlockedIce.SetActive(true);
            }
            other.gameObject.SetActive(false);
        }
        if (other.tag == "PickUpFuego")
        {
            AmmoFireBall = AmmoFireBall + 12;
            count = true;
            if (lockedFire != null)
            {
                lockedFire.SetActive(false);
            }
            if (unlockedFire != null)
            {
                unlockedFire.SetActive(true);
            }
            other.gameObject.SetActive(false);
        }
        if (other.tag == "PickUpDanzarina")
        {
            AmmoDanceBall = AmmoDanceBall + 8;
            count = true;
            if (lockedDance != null)
            {
                lockedDance.SetActive(false);
            }
            if (unlockedDance != null)
            {
                unlockedDance.SetActive(true);
            }
            other.gameObject.SetActive(false);
        }
        if (other.tag == "PickUpFragmentadora")
        {
            AmmoFragmentBall = AmmoFragmentBall + 30;
            count = true;
            if (lockedFragment != null)
            {
                lockedFragment.SetActive(false);
            }
            if (unlockedFragment != null)
            {
                unlockedFragment.SetActive(true);
            }
            other.gameObject.SetActive(false);
        }
        if (other.tag == "PickUpExplosivo")
        {
            AmmoExplociveBall = AmmoExplociveBall + 10;
            count = true;
            if (lockedExplosive != null)
            {
                lockedExplosive.SetActive(false);
            }
            if (unlockedExplocive != null)
            {
                unlockedExplocive.SetActive(true);
            }
            other.gameObject.SetActive(false);
        }
        if(other.tag == "PickUpVida")
        {
            life = life + 100;
            if(life>maxLife)
            {
                life = maxLife;
            }
            other.gameObject.SetActive(false);
        }
        //AQUI RESIVO DAÑO
        
    }
    //COMENTARLO
    public static Jugador GetPlayer()
    {
        return InstancePlayer;
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Roca")
        {
            if (armor > 0)
            {
                armor = 0;
            }
            else
            {
                life = 0;
            }
        }
        
    }

    private void OnCollisionStay(Collision collision)
    {
        
        if (!Immune)
        {
            if (collision.gameObject.tag == "Corredor")
            {
                if (armor > 0)
                {
                    armor = armor - 1;
                }
                else
                {
                    life = life - 1;
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
        if (!Immune)
        {
            if (other.tag == "Pinchos")
            {
                if (armor > 0)
                {
                    armor = armor - skewers.damage;
                }
                else
                {
                    life = life - skewers.damage;
                }
            }
            if (other.gameObject.tag == "Corredor")
            {
                if (armor > 0)
                {
                    armor = armor - 1;
                }
                else
                {
                    life = life - 1;
                }
            }
        }
    }
    public void AddAmmoFireBall(int ammo)
    {
        if (AmmoFireBall <= TOP_AMMO)
        {
            AmmoFireBall = AmmoFireBall + ammo;
        }
    }
    public void AddAmmoIceBall(int ammo)
    {
        if (AmmoIceBall <= TOP_AMMO)
        {
            AmmoIceBall = AmmoIceBall + ammo;
        }
    }
    public void AddAmmoFragmentBall(int ammo)
    {
        if (AmmoFragmentBall <= TOP_AMMO)
        {
            AmmoFragmentBall = AmmoFragmentBall + ammo;
        }
    }
    public void AddAmmoDanceBall(int ammo)
    {
        if (AmmoDanceBall <= TOP_AMMO)
        {
            AmmoDanceBall = AmmoDanceBall + ammo;
        }
    }
    public void AddAmmoExplocive(int ammo)
    {
        if (AmmoExplociveBall <= TOP_AMMO)
        {
            AmmoExplociveBall = AmmoExplociveBall + ammo;
        }
    }
    public int GetTOPAMMO()
    {
        return TOP_AMMO;
    }
    public int GetAmmoFireBall()
    {
        return AmmoFireBall;
    }
    public int GetAmmoIceBall()
    {
        return AmmoIceBall;
    }
    public int GetAmmoFragmentBall()
    {
        return AmmoFragmentBall;
    }
    public int GetAmmoDanceBall()
    {
        return AmmoDanceBall;
    }
    public int GetAmmoExplociveBall()
    {
        return AmmoExplociveBall;
    }
    public void SubstractAmmoFireBall()
    {
        AmmoFireBall = AmmoFireBall - 1;
    }
    public void SubstractAmmoDanceBall()
    {
        AmmoDanceBall = AmmoDanceBall - 1;
    }
    public void SubstractAmmoIceBall()
    {
        AmmoIceBall = AmmoIceBall - 1;
    }
    public void SetTOPAMMO(int topAmmo)
    {
        TOP_AMMO = topAmmo;
    }
    public void SetAmmoIceBall(int ammo)
    {
        AmmoIceBall = ammo;
    }
    public void SetAmmoFireBall(int ammo)
    {
        AmmoFireBall = ammo;
    }
    public void SetAmmoDanceBall(int ammo)
    {
        AmmoDanceBall = ammo;
    }
    public void SetAmmoExplociveBall(int ammo)
    {
        AmmoExplociveBall = ammo;
    }
    public void SetPowerUpArmor(bool _powerUpArmor)
    {
        powerUpArmor = _powerUpArmor;
    }
    public void SetPowerUpDobleDamage(bool _dobleDamage)
    {
        powerUpDobleDamage = _dobleDamage;
    }
    public void SetImmune(bool _immune)
    {
        Immune = _immune;
    }
    public void SetDoblePoints(bool _doblepoints)
    {
        DoblePoints = _doblepoints;
    }
    public void SetInstaKill(bool _instaKill)
    {
        InstaKill = _instaKill;
    }
    public void SetCountImmune(float _contImmune)
    {
        countImmune = _contImmune;
    }
    public void SetCountDoblePoints(float _countDoblePoints)
    {
        countDoblePoints = _countDoblePoints;
    }
    public void SetDileyActive(float diley)
    {
        dileyActive = diley;
    }
    public void SetCountInstaKill(float _countInstaKill)
    {
        countInstaKill = _countInstaKill;
    }
    public void SetScore(int _score)
    {
        score = _score;
    }
    public void SubstractAmmoFragmentBall()
    {
        AmmoFragmentBall = AmmoFragmentBall - 1;
    }
    public void SubstractAmmoExplociveBall()
    {
        AmmoExplociveBall = AmmoExplociveBall - 1;
    }
    public int GetScore()
    {
        return score;
    }
    public void AddScore(int _score)
    {
        score = score + _score;
    }
    public void SetAdditionalDamageCommonBall(float _additionalDamageCommonBall)
    {
        AdditionalDamageCommonBall = _additionalDamageCommonBall;
    }
    public void SetAditionalDamageMiniBalls(float _aditionalDamageMiniBalls)
    {
        AditionalDamageMiniBalls = _aditionalDamageMiniBalls;
    }
    public void SetAdditionalDamageExplociveBall(float _aditionalDamageExplociveBall)
    {
        AdditionalDamageExplociveBall = _aditionalDamageExplociveBall;
    }
    public void SetAdditionalDamageFireBall(float _additionalDamageFireBall)
    {
        AdditionalDamageFireBall = _additionalDamageFireBall;
    }
    public void SetAdditionalDamageIceBall(float _additionalDamageIceBall)
    {
        AdditionalDamageIceBall = _additionalDamageIceBall;
    }
    public float GetAdditionalDamageCommonBall()
    {
        return AdditionalDamageCommonBall;
    }
    public float GetAditionalDamageMiniBalls()
    {
        return AditionalDamageMiniBalls;
    }
    public float GetAdditionalDamageExplociveBall()
    {
        return AdditionalDamageExplociveBall;
    }
    public float GetAdditionalDamageFireBall()
    {
        return AdditionalDamageFireBall;
    }
    public float GetAdditionalDamageIceBall()
    {
        return AdditionalDamageIceBall;
    }
    public bool GetpowerUpDobleDamage()
    {
        return powerUpDobleDamage;
    }
    public void SetPowerUpAddLife(bool _life)
    {
        powerUpAddLife = _life;
    }
    public void SetpowerUpArmor(bool _armor)
    {
        powerUpArmor = _armor;
    }
    public bool GetDoblePoints()
    {
        return DoblePoints;
    }
    public bool GetInstaKill()
    {
        return InstaKill;
    }
    public bool GetActiveInstaKill()
    {
        return activeInstaKill;
    }
    public void SetActiveInstaKill(bool _InstaKill)
    {
        activeInstaKill = _InstaKill;
    }
    public bool GetPowerUpAddLife()
    {
        return powerUpAddLife;
    }
    public bool GetPowerUpArmor()
    {
        return powerUpArmor;
    }
    public float GetCountImmune()
    {
        return countImmune;
    }
    public bool GetImmune()
    {
        return Immune;
    }
    public float GetCountDoblePoints()
    {
        return countDoblePoints;
    }
    public float GetCountInstaKill()
    {
        return countInstaKill;
    }
    public float GetDileyActive()
    {
        return dileyActive;
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
