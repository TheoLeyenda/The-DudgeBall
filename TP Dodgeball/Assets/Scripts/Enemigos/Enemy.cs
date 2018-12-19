using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
public enum EstadoEnemigo
{
    frozen,
    Burned,
    normal,
    dance,
}
public class Enemy : MonoBehaviour {

    // Use this for initialization
    private Player player;
    public float life;
    public float maxLife;
    public GameObject lifeBar;
    public GameObject framework;
    private bool dead;
    private EstadoEnemigo enemyState;
    private float damageBallCommon = 30;
    private float damageBallIce = 5;
    private float damageMiniBalls = 18;
    private float damageBallDance = 0;
    private float damageFireBall = 10;
    private float damageExplociveBall = 50;
    private float rotateX = 0;
    private float rotateY = 0;
    private float rotateZ = 0;
    public GameObject effectBurned;
    public GameObject effectFrozen;
    public GameObject effectMusic;
    private bool touchFloor;
    private bool dodge;
    public bool i_AmInPool;
    private bool once;
    void Start() {
        once = true;
        if(Player.InstancePlayer != null)
        {
            player = Player.InstancePlayer;
        }
        dead = false;
        enemyState = EstadoEnemigo.normal;
        
    }

    // Update is called once per frame
    void Update() {
        if (player != null)
        {
            if (player.GetpowerUpDobleDamage())
            {
                player.SetAdditionalDamageCommonBall(damageBallCommon);
                player.SetAditionalDamageMiniBalls(damageMiniBalls);
                player.SetAdditionalDamageIceBall(damageBallIce);
                player.SetAdditionalDamageFireBall(damageFireBall);
                player.SetAdditionalDamageExplociveBall(damageExplociveBall);
                player.SetPowerUpDobleDamage(false);
            }
           
        }
        
    }
    public void Dodge(int lado, float velocidad, float velAgregado)//tiempo = 1 y tiempoFinal = 2
    {
        
        if (lado == 1)
        {

            transform.position = transform.position + transform.right * Time.deltaTime * (velocidad+ velAgregado);
            transform.position += transform.forward * Time.deltaTime *velocidad;
        }
        if(lado == 2)
        {
           
            transform.position = transform.position - transform.right * Time.deltaTime * (velocidad + velAgregado);
            transform.position += transform.forward * Time.deltaTime * velocidad;
        }
    }
    public void UpdateHP()
    {
        if (lifeBar != null)
        {
            float z = (float)life / (float)maxLife;
            Vector3 ScaleBar = new Vector3(1, 1, z);
            lifeBar.transform.localScale = ScaleBar;
        }
    }
    public void IsDead()
    {
        if (life <= 0)
        {
            dead = true;
            DataStructure.auxiliaryDataStructure.playerData.downcastEnemies++;
        }
    }
    public void SetDead(bool _dead)
    {
        dead = _dead;
    }
    public bool GetDead()
    {
        return dead;
    }
    public void SetEnemyState(EstadoEnemigo state)
    {
        enemyState = state;
    }
    public EstadoEnemigo GetEnemyState()
    {
        return enemyState;
    }
    public void SetRotateX(float rotX)
    {
        rotateX = rotX;
    }
    public void SetRotateY(float rotY)
    {
        rotateY = rotY;
    }
    public void SetRotateZ(float rotZ)
    {
        rotateZ = rotZ;
    }
    public float GetRotateX()
    {
        return rotateX;
    }
    public float GetRotateY()
    {
        return rotateY;
    }
    public float GetRotateZ()
    {
        return rotateZ;
    }
    public void Rotate()
    {
        transform.Rotate(rotateX, rotateY, rotateZ);
    }
    public void SetTouchFloor(bool touch)
    {
        touchFloor = touch;
    }
    public bool GetTouchFloor()
    {
        return touchFloor;
    }
    public void SetDamageCommonBall(float newDamage)
    {
        damageBallCommon = newDamage;
    }
    public void SetDamageIceBall(float newDamage)
    {
        damageBallIce = newDamage;
    }
    public void SetDamageMiniBall(float newDamage)
    {
        damageMiniBalls = newDamage;
    }
    public void SetDamageDanceBall(float newDamage)
    {
        damageBallDance = newDamage;
    }
    public void SetDamageFireBall(float newDamage)
    {
        damageFireBall = newDamage;
    }
    public void SetDamageExplociveBall(float newDamage)
    {
        damageExplociveBall = newDamage;
    }

    public float GetDamageCommonBall()
    {
        return damageBallCommon;
    }
    public float GetDamageIceBall()
    {
        return damageBallIce;
    }
    public float GetDamageMiniBall()
    {
        return damageMiniBalls;
    }
    public float GetDamageDanceBall()
    {
        return damageBallDance;
    }
    public float GetDamageFireBall()
    {
        return damageFireBall;
    }
    public float GetDamageExplociveBall()
    {
        return damageExplociveBall;
    }
    public void SetDodge(bool _esquivar)
    {
        dodge = _esquivar;
    }
    public bool GetDodge()
    {
        return dodge;
    }

}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
