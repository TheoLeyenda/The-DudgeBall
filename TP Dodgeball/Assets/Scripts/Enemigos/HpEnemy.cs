using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpEnemy : MonoBehaviour {

    // Use this for initialization
    public Shooter shooter;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PelotaComun" && gameObject.tag == "Tirador")
        {
            //Debug.Log("ENTRE");
            if (shooter.player != null)
            {
                shooter.life = shooter.life - (shooter.GetDamageCommonBall() + shooter.player.GetAdditionalDamageCommonBall());
                //Debug.Log("Hize Daño");
                shooter.IsDead();
                if (shooter.player.GetDoblePoints())
                {
                    shooter.player.AddScore(10 * 2);
                }
                else
                {
                    shooter.player.AddScore(10);
                }
            }
        }
        if (other.gameObject.tag == "PelotaDeHielo" && gameObject.tag == "Tirador")
        {
            if (shooter.player != null)
            {
                if (shooter.player.GetDoblePoints())
                {
                    shooter.player.AddScore(10 * 2);
                }
                else
                {
                    shooter.player.AddScore(10);
                }
                shooter.life = shooter.life - (shooter.GetDamageIceBall() + shooter.player.GetAdditionalDamageIceBall());
            }
            shooter.IsDead();
            if (shooter.speed > 0)
            {
                //velocidad = velocidad - 0.2f;
                shooter.speed = 0;
            }
            if (shooter.speed <= 0)
            {

                if (shooter.GetEnemyState() != EstadoEnemigo.frozen)
                {
                    shooter.timeState = 5;//tiempo por el cual el enemigo "Corredor" estara congelado
                }
                shooter.SetEnemyState(EstadoEnemigo.frozen);
                shooter.effectFrozen.SetActive(true);
            }
        }
        if (other.gameObject.tag == "MiniPelota" && gameObject.tag == "Tirador")
        {
            if (shooter.player != null)
            {
                if (shooter.player.GetDoblePoints())
                {
                    shooter.player.AddScore(10 * 2);
                }
                else
                {
                    shooter.player.AddScore(10);
                }
                shooter.life = shooter.life - (shooter.GetDamageMiniBall() + shooter.player.GetAditionalDamageMiniBalls());
                shooter.IsDead();
            }
        }
        if (other.gameObject.tag == "PelotaDanzarina" && gameObject.tag == "Tirador")
        {

            if (shooter.GetEnemyState() != EstadoEnemigo.dance)
            {
                shooter.timeState = 7;//tiempo por el cual el enemigo estara bailando
            }
            shooter.SetEnemyState(EstadoEnemigo.dance);
            shooter.effectMusic.SetActive(true);
            shooter.life = shooter.life - shooter.GetDamageDanceBall();
            shooter.IsDead();
            if (shooter.player.GetDoblePoints())
            {
                shooter.player.AddScore(5 * 2);
            }
            else
            {
                shooter.player.AddScore(5);
            }
        }
        if (other.gameObject.tag == "PelotaDeFuego" && gameObject.tag == "Tirador")
        {

            if (shooter.GetEnemyState() != EstadoEnemigo.Burned)
            {
                shooter.timeState = 7;
            }
            if (shooter.GetEnemyState() != EstadoEnemigo.dance)
            {
                shooter.SetEnemyState(EstadoEnemigo.Burned);
            }
            shooter.effectBurned.SetActive(true);
            shooter.speed = shooter.auxSpeed;
            shooter.dilay = shooter.auxDilay;
        }
        if (other.gameObject.tag == "PelotaExplociva" && gameObject.tag == "Tirador")
        {

            if (shooter.player != null)
            {
                if (shooter.player.GetDoblePoints())
                {
                    shooter.player.AddScore(20 * 2);
                }
                else
                {
                    shooter.player.AddScore(10);
                }
                shooter.life = shooter.life - (shooter.GetDamageExplociveBall() + shooter.player.GetAdditionalDamageExplociveBall());
                shooter.IsDead();
            }

        }
    }

}
