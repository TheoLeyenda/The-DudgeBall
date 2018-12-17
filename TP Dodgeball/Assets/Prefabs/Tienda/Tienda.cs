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
        if (Jugador.GetPlayer() != null)
        {
            if (Jugador.GetPlayer().playerWindows && Jugador.GetPlayer().playerAndroid == false)
            {
                text.gameObject.SetActive(true);
                if (Input.GetKey(KeyCode.E))
                {
                    OpeningCamvasStore();
                    //prende el camvas de tienda
                }
            }
            if (Jugador.GetPlayer().playerAndroid && Jugador.GetPlayer().playerWindows == false)
            {
                boton.SetActive(true);
                //al apretar el boton en pantalla prende la tienda
            }
        }
    }
    public void OpeningCamvasStore()
    {
        if(Jugador.GetPlayer() != null)
        {
            Jugador.GetPlayer().inStore = true;
        }
        OpenStore = true;
        if (Jugador.GetPlayer().playerWindows && Jugador.GetPlayer().playerAndroid == false)
        {
            camvasStoreWindows.SetActive(true);
            text.gameObject.SetActive(false);
            Jugador.GetPlayer().inStore = true;
        }
        if (Jugador.GetPlayer().playerAndroid && Jugador.GetPlayer().playerWindows == false)
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
        if (Jugador.GetPlayer() != null)
        {
            Jugador.GetPlayer().inStore = false;
        }
        if (Jugador.GetPlayer().playerWindows && Jugador.GetPlayer().playerAndroid == false)
        {
            camvasStoreWindows.SetActive(false);
            Jugador.GetPlayer().inStore = false;
        }
        if (Jugador.GetPlayer().playerAndroid && Jugador.GetPlayer().playerWindows == false)
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
        if (Jugador.GetPlayer() != null)
        {
            if (numBuy == 0)
            {
                ClosingStore();
            }
            if (PriceChargerIce <= Jugador.GetPlayer().GetScore() && numBuy == 1)
            {
                Jugador.GetPlayer().AddAmmoIceBall(12);
                Jugador.GetPlayer().SubtractScore(PriceChargerIce);
            }
            if (PriceChargerFire <= Jugador.GetPlayer().GetScore() && numBuy == 2)
            {
                Jugador.GetPlayer().AddAmmoFireBall(12);
                Jugador.GetPlayer().SubtractScore(PriceChargerFire);
            }
            if (PriceChargerFragment <= Jugador.GetPlayer().GetScore() && numBuy == 3)
            {
                Jugador.GetPlayer().AddAmmoFragmentBall(30);
                Jugador.GetPlayer().SubtractScore(PriceChargerFragment);
            }
            if (PriceChargerDancer <= Jugador.GetPlayer().GetScore() && numBuy == 4)
            {
                Jugador.GetPlayer().AddAmmoDanceBall(8);
                Jugador.GetPlayer().SubtractScore(PriceChargerDancer);
            }
            if (PriceChargerExplocive <= Jugador.GetPlayer().GetScore() && numBuy == 5)
            {
                Jugador.GetPlayer().AddAmmoExplocive(10);
                Jugador.GetPlayer().SubtractScore(PriceChargerExplocive);
            }
            if (PriceLife <= Jugador.GetPlayer().GetScore() && numBuy == 6)
            {
                Jugador.GetPlayer().AddedLife();
                Jugador.GetPlayer().SubtractScore(PriceLife);
            }
            if (PriceArmor <= Jugador.GetPlayer().GetScore() && numBuy == 7)
            {
                Jugador.GetPlayer().Armor();
                Jugador.GetPlayer().SubtractScore(PriceArmor);
            }
            if (PriceDobleDamage <= Jugador.GetPlayer().GetScore() && numBuy == 8)
            {
                Jugador.GetPlayer().DobleDamage();
                Jugador.GetPlayer().SubtractScore(PriceDobleDamage);
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
        if (Jugador.GetPlayer().playerWindows && Jugador.GetPlayer().playerAndroid == false)
        {
            text.gameObject.SetActive(false);
        }
        if (Jugador.GetPlayer().playerAndroid && Jugador.GetPlayer().playerWindows == false)
        {
            boton.SetActive(false);
        }
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
