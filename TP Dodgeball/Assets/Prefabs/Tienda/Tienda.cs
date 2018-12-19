using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
public class Tienda : MonoBehaviour
{
    public GameObject boton;
    public Text text;
    public Text textPriceIceAndroid;
    public Text textPriceIceWindows;
    public Text textPriceFireAndroid;
    public Text textPriceFireWindows;
    public Text textPriceDancerAndroid;
    public Text textPriceDancerWindows;
    public Text textPriceFragmentAndroid;
    public Text textPriceFragmentWindows;
    public Text textPriceExplociveAndroid;
    public Text textPriceExplociveWindows;
    public Text textPriceArmorAndroid;
    public Text textPriceArmorWindows;
    public Text textPriceLifeAndroid;
    public Text textPriceLifeWindows;
    public Text textPriceDobleDamageAndroid;
    public Text textPriceDobleDamageWindows;
    public int PriceChargerIce;
    public int PriceChargerFire;
    public int PriceChargerDancer;
    public int PriceChargerFragment;
    public int PriceChargerExplocive;
    public int PriceArmor;
    public int PriceDobleDamage;
    public int PriceLife;
    public GameObject camvasStoreWindows;
    public GameObject camvasStoreAndroid;
    private bool OpenStore;
    public GameObject menuWeaponAndroid;
    // Use this for initialization
    void Start()
    {
        OpenStore = false;
        if (camvasStoreAndroid != null && camvasStoreWindows != null)
        {
            camvasStoreWindows.SetActive(false);
            camvasStoreAndroid.SetActive(false);
        }
        if (textPriceLifeAndroid != null
            && textPriceIceAndroid != null
            && textPriceFireAndroid != null
            && textPriceFragmentAndroid != null
            && textPriceExplociveAndroid != null
            && textPriceDobleDamageAndroid != null
            && textPriceDancerAndroid != null
            && textPriceArmorAndroid != null)
        {
            textPriceLifeAndroid.text = PriceLife + " $";
            textPriceIceAndroid.text = PriceChargerIce + " $";
            textPriceFireAndroid.text = PriceChargerFire + " $";
            textPriceFragmentAndroid.text = PriceChargerFragment + " $";
            textPriceExplociveAndroid.text = PriceChargerExplocive + " $";
            textPriceDobleDamageAndroid.text = PriceDobleDamage + " $";
            textPriceDancerAndroid.text = PriceChargerDancer + " $";
            textPriceArmorAndroid.text = PriceArmor + " $";
        }
        if (textPriceIceWindows != null
            && textPriceFireWindows != null
            && textPriceDancerWindows != null
            && textPriceFragmentWindows != null
            && textPriceExplociveWindows != null
            && textPriceArmorWindows != null
            && textPriceLifeWindows != null
            && textPriceDobleDamageWindows != null)
        {
            textPriceLifeWindows.text = PriceLife + " $";
            textPriceIceWindows.text = PriceChargerIce + " $";
            textPriceFireWindows.text = PriceChargerFire + " $";
            textPriceFragmentWindows.text = PriceChargerFragment + " $";
            textPriceExplociveWindows.text = PriceChargerExplocive + " $";
            textPriceDobleDamageWindows.text = PriceDobleDamage + " $";
            textPriceDancerWindows.text = PriceChargerDancer + " $";
            textPriceArmorWindows.text = PriceArmor + " $";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(camvasStoreAndroid.activeSelf == true || camvasStoreWindows.activeSelf == true && GameManager.GetGameManager() != null)
        {
            GameManager.GetGameManager().pause = true;
        }
        else
        {
            //GameManager.GetGameManager().pausa = false;
        }
       
    }
    public void OpeningStore()
    {
        if (Player.GetPlayer() != null)
        {
            if (Player.GetPlayer().playerWindows && Player.GetPlayer().playerAndroid == false)
            {
                text.gameObject.SetActive(true);
                if (Input.GetKey(KeyCode.E))
                {
                    OpeningCamvasStore();
                    //prende el camvas de tienda
                }
            }
            if (Player.GetPlayer().playerAndroid && Player.GetPlayer().playerWindows == false)
            {
                boton.SetActive(true);
                //al apretar el boton en pantalla prende la tienda
            }
        }
    }
    public void OpeningCamvasStore()
    {
        if(Player.GetPlayer() != null)
        {
            Player.GetPlayer().inStore = true;
        }
        OpenStore = true;
        if (Player.GetPlayer().playerWindows && Player.GetPlayer().playerAndroid == false)
        {
            camvasStoreWindows.SetActive(true);
            text.gameObject.SetActive(false);
            Player.GetPlayer().inStore = true;
        }
        if (Player.GetPlayer().playerAndroid && Player.GetPlayer().playerWindows == false)
        {
            camvasStoreAndroid.SetActive(true);
            boton.SetActive(false);
            if (menuWeaponAndroid != null)
            {
                menuWeaponAndroid.SetActive(true);
            }

        }
        if (GameManager.GetGameManager() != null)
        {
            GameManager.GetGameManager().pause = true;
        }
    }
    public void ClosingStore()
    {
        OpenStore = false;
        if (Player.GetPlayer() != null)
        {
            Player.GetPlayer().inStore = false;
        }
        if (Player.GetPlayer().playerWindows && Player.GetPlayer().playerAndroid == false)
        {
            camvasStoreWindows.SetActive(false);
            Player.GetPlayer().inStore = false;
        }
        if (Player.GetPlayer().playerAndroid && Player.GetPlayer().playerWindows == false)
        {
            camvasStoreAndroid.SetActive(false);
            if (menuWeaponAndroid != null)
            {
                menuWeaponAndroid.SetActive(false);
            }
        }
        if (GameManager.GetGameManager() != null)
        {
            GameManager.GetGameManager().pause = false;
        }
        //que resiva el camvas de la tienda y la apague
    }
    public void Buy(int numBuy)
    {
        if (Player.GetPlayer() != null)
        {
            if (numBuy == 0)
            {
                ClosingStore();
            }
            if (PriceChargerIce <= Player.GetPlayer().GetScore() && numBuy == 1)
            {
                Player.GetPlayer().AddAmmoIceBall(12);
                Player.GetPlayer().SubtractScore(PriceChargerIce);
            }
            if (PriceChargerFire <= Player.GetPlayer().GetScore() && numBuy == 2)
            {
                Player.GetPlayer().AddAmmoFireBall(12);
                Player.GetPlayer().SubtractScore(PriceChargerFire);
            }
            if (PriceChargerFragment <= Player.GetPlayer().GetScore() && numBuy == 3)
            {
                Player.GetPlayer().AddAmmoFragmentBall(30);
                Player.GetPlayer().SubtractScore(PriceChargerFragment);
            }
            if (PriceChargerDancer <= Player.GetPlayer().GetScore() && numBuy == 4)
            {
                Player.GetPlayer().AddAmmoDanceBall(8);
                Player.GetPlayer().SubtractScore(PriceChargerDancer);
            }
            if (PriceChargerExplocive <= Player.GetPlayer().GetScore() && numBuy == 5)
            {
                Player.GetPlayer().AddAmmoExplocive(10);
                Player.GetPlayer().SubtractScore(PriceChargerExplocive);
            }
            if (PriceLife <= Player.GetPlayer().GetScore() && numBuy == 6)
            {
                Player.GetPlayer().AddedLife();
                Player.GetPlayer().SubtractScore(PriceLife);
            }
            if (PriceArmor <= Player.GetPlayer().GetScore() && numBuy == 7)
            {
                Player.GetPlayer().Armor();
                Player.GetPlayer().SubtractScore(PriceArmor);
            }
            if (PriceDobleDamage <= Player.GetPlayer().GetScore() && numBuy == 8)
            {
                Player.GetPlayer().DobleDamage();
                Player.GetPlayer().SubtractScore(PriceDobleDamage);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            OpeningStore();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (Player.GetPlayer().playerWindows && Player.GetPlayer().playerAndroid == false)
        {
            text.gameObject.SetActive(false);
        }
        if (Player.GetPlayer().playerAndroid && Player.GetPlayer().playerWindows == false)
        {
            boton.SetActive(false);
        }
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
