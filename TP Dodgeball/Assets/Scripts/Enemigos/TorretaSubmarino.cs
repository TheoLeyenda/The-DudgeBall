using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
public class TorretaSubmarino : MonoBehaviour {

    // Use this for initialization
    private Jugador player;
    public PoolPelota poolBullets;
    private PoolObject poolObject;
    public GameObject generatorBullet;
    public float dilay;
    private float auxDilay;
    private bool shooting;
    void Start() {
        if (Jugador.InstancePlayer != null)
        {
            player = Jugador.InstancePlayer;
        }
        auxDilay = dilay;
        shooting = false;
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
        CheckShooting();
    }
    public void Shoot()
    {
        if (shooting)
        {
            if (poolBullets.GetId() < poolBullets.count)
            {
                GameObject go = poolBullets.GetObject();
                PelotaEnemigo bullet = go.GetComponent<PelotaEnemigo>();
                go.transform.position = generatorBullet.transform.position;
                go.transform.rotation = generatorBullet.transform.rotation;
                bullet.Shoot();
            }
        }
    }
    public void CheckShooting()
    {
        if (dilay > 0)
        {
            dilay = dilay - Time.deltaTime;
        }
        if (dilay <= 0)
        {
            Shoot();
            dilay = auxDilay;
        }
    }
    public void Movement()
    {
        if (player != null)
        {
            if (shooting)
            {
                transform.LookAt(new Vector3(player.transform.position.x, player.transform.transform.position.y, player.transform.position.z));
            }
        }
    }
    public void SetShooting(bool _shooting)
    {
        shooting = _shooting;
    }
    public bool GetShooting()
    {
        return shooting;
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
