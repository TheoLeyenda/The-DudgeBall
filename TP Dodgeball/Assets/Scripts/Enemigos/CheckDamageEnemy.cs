using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDamageEnemy : Enemy {

    public Shooter shooter;
    private Vector3 dir;
	// Use this for initialization
	void Start () {
        life = shooter.life;
        maxLife = shooter.maxLife;
	}
    private void Update()
    {
        if (GetEnemyState() == EstadoEnemigo.Burned)
        {
            life = shooter.life;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PelotaComun" && gameObject.tag == "Tirador" && GetComponent<SphereCollider>() == null)
        {
            Debug.Log("ENTRE");
            if (shooter.player != null)
            {
                shooter.life = shooter.life - (GetDamageCommonBall() + shooter.player.GetAdditionalDamageCommonBall());
                Debug.Log("Hize Daño");
                IsDead();
                if (shooter.player.GetDoblePoints())
                {
                    shooter.player.AddScore(10 * 2);
                }
                else
                {
                    shooter.player.AddScore(10);
                }
                life = shooter.life;
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
                shooter.life = shooter.life - (GetDamageIceBall() + shooter.player.GetAdditionalDamageIceBall());
                life = shooter.life;
            }
            IsDead();
            if (shooter.speed > 0)
            {
                //velocidad = velocidad - 0.2f;
                shooter.speed = 0;
            }
            if (shooter.speed <= 0)
            {

                if (GetEnemyState() != EstadoEnemigo.frozen)
                {
                    shooter.timeState = 5;//tiempo por el cual el enemigo "Corredor" estara congelado
                }
                SetEnemyState(EstadoEnemigo.frozen);
                effectFrozen.SetActive(true);
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
                shooter.life = shooter.life - (GetDamageMiniBall() + shooter.player.GetAditionalDamageMiniBalls());
                life = shooter.life;
                IsDead();
            }
        }
        if (other.gameObject.tag == "PelotaDanzarina" && gameObject.tag == "Tirador")
        {

            if (GetEnemyState() != EstadoEnemigo.dance)
            {
                shooter.timeState = 7;//tiempo por el cual el enemigo estara bailando
            }
            SetEnemyState(EstadoEnemigo.dance);
            effectMusic.SetActive(true);
            shooter.life = shooter.life - GetDamageDanceBall();
            life = shooter.life;
            IsDead();
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

            if (GetEnemyState() != EstadoEnemigo.Burned)
            {
                shooter.timeState = 7;
            }
            if (GetEnemyState() != EstadoEnemigo.dance)
            {
                SetEnemyState(EstadoEnemigo.Burned);
            }
            effectBurned.SetActive(true);
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
                shooter.life = shooter.life - (GetDamageExplociveBall() + shooter.player.GetAdditionalDamageExplociveBall());
                life = shooter.life;
                IsDead();
            }

        }
    }
}
