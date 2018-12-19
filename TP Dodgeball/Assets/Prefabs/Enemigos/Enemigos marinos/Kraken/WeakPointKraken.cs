using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPointKraken : Enemy {

    // Use this for initialization
    private float timeState;
    private float fireEffect;
    public Kraken kraken;
    private Player instancePlayer;
	void Start () {
        if (Player.InstancePlayer != null)
        {
            instancePlayer = Player.InstancePlayer;
        }
	}
	
	// Update is called once per frame
	void Update () {
        CheckStates();
	}
    public void CheckStates()
    {
        if (timeState > 0)
        {
            
            if (GetEnemyState() == EstadoEnemigo.Burned || effectBurned.activeSelf)
            {
                fireEffect = fireEffect + Time.deltaTime;
                if (fireEffect >= 1)
                {
                    if (instancePlayer != null)
                    {
                        if (instancePlayer.GetDoblePoints())
                        {
                            instancePlayer.AddScore(5 * 2);
                        }
                        else
                        {
                            instancePlayer.AddScore(5);
                        }
                        kraken.life = kraken.life - (GetDamageFireBall() + instancePlayer.GetAdditionalDamageFireBall() + kraken.danioIncreasedFireBall);
                        kraken.IsDead();
                    }
                    fireEffect = 0;
                }
                
            }
            timeState = timeState - Time.deltaTime;
        }
        if (timeState <= 0)
        {

            if (GetEnemyState() == EstadoEnemigo.Burned)
            {
                effectBurned.SetActive(false);
                SetEnemyState(EstadoEnemigo.normal);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PelotaComun")
        {
            if (instancePlayer != null)
            {
                kraken.life = kraken.life - (GetDamageCommonBall() + instancePlayer.GetAdditionalDamageCommonBall() + kraken.danioIncreasedCommonBall);
                kraken.IsDead();
                if (instancePlayer.GetDoblePoints())
                {
                    instancePlayer.AddScore(10 * 2);
                }
                else
                {
                    instancePlayer.AddScore(10);
                }
            }
        }

        if (other.gameObject.tag == "MiniPelota")
        {
            if (instancePlayer != null)
            {
                if (instancePlayer.GetDoblePoints())
                {
                    instancePlayer.AddScore(10 * 2);
                }
                else
                {
                    instancePlayer.AddScore(10);
                }
                kraken.life = kraken.life - (GetDamageMiniBall() + instancePlayer.GetAditionalDamageMiniBalls());
                kraken.IsDead();
            }
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
        }
        if (other.gameObject.tag == "PelotaExplociva")
        {
            if (instancePlayer != null)
            {
                if (instancePlayer.GetDoblePoints())
                {
                    instancePlayer.AddScore(20 * 2);
                }
                else
                {
                    instancePlayer.AddScore(20);
                }
                kraken.life = kraken.life - (GetDamageExplociveBall() + instancePlayer.GetAdditionalDamageExplociveBall());
            }
            kraken.IsDead();

        }
    }
}