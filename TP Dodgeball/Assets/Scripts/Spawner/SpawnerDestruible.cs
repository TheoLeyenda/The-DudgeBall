using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class SpawnerDestruible : Enemigo {

    // Use this for initialization
    private float timeState;
    private float effectFire;
    private Jugador player;
    void Start () {
        if(Jugador.InstancePlayer != null)
        {
            player = Jugador.InstancePlayer;
        }
        effectFire = 0;
        effectFrozen.SetActive(false);
        effectBurned.SetActive(false);
        effectMusic.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if(life <= 0)
        {
            player.AddScore(150);
            gameObject.SetActive(false);
        }
        if (timeState > 0)
        {
            if (GetEnemyState() == EstadoEnemigo.Burned || effectBurned.activeSelf)
            {
                effectFire = effectFire + Time.deltaTime;
                if (effectFire >= 1)
                {
                    if (player != null)
                    {
                        if (player.GetDoblePoints())
                        {
                            player.AddScore(5 * 2);
                        }
                        else
                        {
                            player.AddScore(5);
                        }
                        life = life - (GetDamageFireBall() + player.GetAdditionalDamageFireBall());
                    }
                    effectFire = 0;
                }
            }
            timeState = timeState - Time.deltaTime;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PelotaComun")
        {
            if (player != null)
            {
                life = life - (GetDamageCommonBall() + player.GetAdditionalDamageCommonBall());
                if (player.GetDoblePoints())
                {
                    player.AddScore(10 * 2);
                }
                else
                {
                    player.AddScore(10);
                }
            }
            UpdateHP();
        }
        if (other.gameObject.tag == "PelotaDeHielo")
        {
            if (player != null)
            {
                if (player.GetDoblePoints())
                {
                    player.AddScore(10 * 2);
                }
                else
                {
                    player.AddScore(10);
                }
                life = life - (GetDamageIceBall() + player.GetAdditionalDamageIceBall());
            }
            UpdateHP();
        }
        if (other.gameObject.tag == "MiniPelota")
        {
            if (player != null)
            {
                if (player.GetDoblePoints())
                {
                    player.AddScore(10 * 2);
                }
                else
                {
                    player.AddScore(10);
                }
                life = life - (GetDamageMiniBall() + player.GetAditionalDamageMiniBalls());
            }
            UpdateHP();
        }
        if (other.gameObject.tag == "PelotaDanzarina")
        {
            if (player != null)
            {
                if (player.GetDoblePoints())
                {
                    player.AddScore(5 * 2);
                }
                else
                {
                    player.AddScore(5);
                }
            }
            if (GetEnemyState() != EstadoEnemigo.dance)
            {
                timeState = 7;//tiempo por el cual el enemigo estara bailando
            }
            SetEnemyState(EstadoEnemigo.dance);
            life = life - GetDamageDanceBall();
            UpdateHP();
        }
        if (other.gameObject.tag == "PelotaDeFuego")
        {
            if (GetEnemyState() != EstadoEnemigo.Burned)
            {
                timeState = 7;
            }
            if (GetEnemyState() != EstadoEnemigo.dance)
            {
                SetEnemyState(EstadoEnemigo.Burned);
            }
            effectBurned.SetActive(true);
            UpdateHP();
        }
        if (other.gameObject.tag == "PelotaExplociva")
        {
            if (player != null)
            {
                if (player.GetDoblePoints())
                {
                    player.AddScore(20 * 2);
                }
                else
                {
                    player.AddScore(20);
                }
                life = life - (GetDamageExplociveBall() + player.GetAdditionalDamageExplociveBall());
            }
            UpdateHP();
        }
    }
}

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)