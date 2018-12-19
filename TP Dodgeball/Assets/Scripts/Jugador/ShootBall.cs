using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBall : MonoBehaviour {

    // Use this for initialization
    private Player instancePlayer;
    public AudioSource sound;
    public AudioClip soundCommonBall;
    public AudioClip soundIceBall;
    public AudioClip soundDanceBall;
    public AudioClip soundFireBall;
    public AudioClip soundExplociveBall;
    public AudioClip soundFragmentBall;
    public Pool CommonBall;
    public Pool IceBall;
    public Pool FragmentBall;
    public Pool DanceBall;
    public Pool FireBall;
    public Pool ExplociveBall;
    public GameObject generator;
    public GameObject generatorExplocive;
    public Transform player;
    public GameObject weaponsPanel;
    private float effectFire;
    public bool playerWindows;
    private bool is_Shooting;
    private int counter;

    private float endDelay;
    private float delay;
	void Start () {
        if(Player.InstancePlayer != null)
        {
            instancePlayer = Player.InstancePlayer;
        }
        counter = 0;
        delay = 0f;
        endDelay = 0.1f;
        if (weaponsPanel != null)
        {
            weaponsPanel.SetActive(false);
        }
        is_Shooting = false;
    }

    // Update is called once per frame
    void Update() {
        if (delay <= endDelay)
        {
            delay = delay + Time.deltaTime;
        }
//#if UNITY_EDITOR// modo de disparar en compu

        // ESTO ES PARA PC
        if (Input.GetButtonDown("Fire1") && playerWindows)
        { 
            Shoot();
        }
        //----------------------
//#elif UNITY_STANDALONE
        //if (Input.GetButtonDown("Fire1"))
        //{
             //Disparar();
             //estaDisparando = true;
       // }
//#endif
    }
    public void Shoot()
    {
        
        if (Time.timeScale > 0)
        {

            if (instancePlayer.ballType == 1 && generator != null && CommonBall != null)
            {
                is_Shooting = true;
                GameObject go = CommonBall.GetObject();
                Ball pelota = go.GetComponent<Ball>();
                go.transform.position = generator.transform.position + generator.transform.right;
                go.transform.rotation = generator.transform.rotation;
                pelota.Shoot();
                if(sound != null && soundCommonBall != null)
                {
                    sound.clip = soundCommonBall;
                    sound.PlayOneShot(soundCommonBall);
                }
            }
            if (instancePlayer.ballType == 2 && IceBall != null && generator != null && instancePlayer.GetAmmoIceBall() > 0)
            {
                is_Shooting = true;
                // cambiar esto por un codigo que haga que la pelota en cuestion se teletrasporte y luego la rotacion de la misma sea
                // igual a la del generador luego de esto que active el gameObject
                //Instantiate(pelotaDeHielo, generador.transform.position + generador.transform.forward, generador.transform.rotation);
                GameObject go = IceBall.GetObject();
                Ball pelota = go.GetComponent<Ball>();
                go.transform.position = generator.transform.position + generator.transform.right;
                go.transform.rotation = generator.transform.rotation;
                pelota.Shoot();
                instancePlayer.SubstractAmmoIceBall();
                if(sound != null && soundIceBall != null)
                {
                    sound.clip = soundIceBall;
                    sound.PlayOneShot(soundIceBall);
                }
            }
            if (instancePlayer.ballType == 3 && FragmentBall != null && generator != null && instancePlayer.GetAmmoFragmentBall() > 0)
            {
                is_Shooting = true;
                // cambiar esto por un codigo que haga que la pelota en cuestion se teletrasporte y luego la rotacion de la misma sea
                // igual a la del generador luego de esto que active el gameObject
                //Instantiate(pelotaFragmentadora, generador.transform.position + generador.transform.forward, generador.transform.rotation);
                GameObject go = FragmentBall.GetObject();
                FragmentBallController pelota = go.GetComponent<FragmentBallController>();
                go.transform.position = generator.transform.position + generator.transform.forward;
                go.transform.rotation = generator.transform.rotation;
                pelota.Shoot();
                instancePlayer.SubstractAmmoFragmentBall();
                if(sound != null && soundFragmentBall != null)
                {
                    sound.clip = soundFragmentBall;
                    sound.PlayOneShot(soundFragmentBall);
                }
            }
            if (instancePlayer.ballType == 4 && DanceBall != null && generator != null && instancePlayer.GetAmmoDanceBall() > 0)
            {
                is_Shooting = true;
                // cambiar esto por un codigo que haga que la pelota en cuestion se teletrasporte y luego la rotacion de la misma sea
                // igual a la del generador luego de esto que active el gameObject
                //Instantiate(pelotaDanzarina, generador.transform.position + generador.transform.forward, generador.transform.rotation);
                GameObject go = DanceBall.GetObject();
                Ball pelota = go.GetComponent<Ball>();
                go.transform.position = generator.transform.position + generator.transform.right;
                go.transform.rotation = generator.transform.rotation;
                pelota.Shoot();
                instancePlayer.SubstractAmmoDanceBall();
                if(sound != null && soundDanceBall != null)
                {
                    sound.clip = soundDanceBall;
                    sound.PlayOneShot(soundDanceBall);
                }
            }
            if (instancePlayer.ballType == 5 && FireBall != null && generator != null && instancePlayer.GetAmmoFireBall() > 0)
            {
                is_Shooting = true;
                // cambiar esto por un codigo que haga que la pelota en cuestion se teletrasporte y luego la rotacion de la misma sea
                // igual a la del generador luego de esto que active el gameObject
                //Instantiate(pelotaDeFuego, generador.transform.position + generador.transform.forward, generador.transform.rotation);
                GameObject go = FireBall.GetObject();
                Ball pelota = go.GetComponent<Ball>();
                go.transform.position = generator.transform.position + generator.transform.right;
                go.transform.rotation = generator.transform.rotation;
                pelota.Shoot();
                instancePlayer.SubstractAmmoFireBall();
                if(sound != null && soundFireBall != null)
                {
                    sound.clip = soundFireBall;
                    sound.PlayOneShot(soundFireBall);
                }
            }
            if (instancePlayer.ballType == 6 && ExplociveBall != null && generatorExplocive != null && instancePlayer.GetAmmoExplociveBall() > 0)
            {
                is_Shooting = true;
                // cambiar esto por un codigo que haga que la pelota en cuestion se teletrasporte y luego la rotacion de la misma sea
                // igual a la del generador luego de esto que active el gameObject
                //Instantiate(pelotaExplociva, generadorExplicivos.transform.position + generadorExplicivos.transform.forward, generador.transform.rotation);
                GameObject go = ExplociveBall.GetObject();
                ExplociveBall pelota = go.GetComponent<ExplociveBall>();
                go.transform.position = generatorExplocive.transform.position + generatorExplocive.transform.right;
                go.transform.rotation = generatorExplocive.transform.rotation;
                pelota.Shoot();
                instancePlayer.SubstractAmmoExplociveBall();
                if(sound != null && soundExplociveBall != null)
                {
                    sound.clip = soundExplociveBall;
                    sound.PlayOneShot(soundExplociveBall);
                }
            }
        }
        //estaDisparando = false;
    }
    public void SwinchWeaponAndroid(int numWeapon)
    {
        switch (numWeapon)
        {
            case 1:
                instancePlayer.ballType = 1;
                break;
            case 2:
                instancePlayer.ballType = 2;
                break;
            case 3:
                instancePlayer.ballType = 3;
                break;
            case 4:
                instancePlayer.ballType = 4;
                break;
            case 5:
                instancePlayer.ballType = 5;
                break;
            case 6:
                instancePlayer.ballType = 6;
                break;
            default:
                instancePlayer.ballType = 1;
                break;
        }
    }
    public void ActivatePanel()
    {
        if(counter == 2)
        {
            weaponsPanel.SetActive(false);
            counter = -1;
        }
        if(weaponsPanel != null && counter == 0)
        {
            weaponsPanel.SetActive(true);
            counter = counter + 1;
            if (GameManager.GetGameManager() != null)
            {
                GameManager.GetGameManager().pause = true;
            }
        }
        if(counter == 1)
        {
            counter = counter + 1;
        }
        if(counter == -1)
        {
            counter = 0;
            if (GameManager.GetGameManager() != null)
            {
                GameManager.GetGameManager().pause = false;
            }
            //Time.timeScale = 1;
        }
    }
    public bool GetIsShooting()
    {
        return is_Shooting;
    }
    public void SetIsShooting(bool isShooting)
    {
        is_Shooting = isShooting;
    }
}
