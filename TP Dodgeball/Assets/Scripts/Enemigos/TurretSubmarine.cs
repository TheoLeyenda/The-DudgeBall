using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSubmarine : MonoBehaviour {

    // Use this for initialization
    private Player player;
    public Pool poolBullets;
    private PoolObject poolObject;
    public GameObject generatorBullet;
    public float dilay;
    private float auxDilay;
    private bool shooting;
    void Start() {
        if (Player.InstancePlayer != null)
        {
            player = Player.InstancePlayer;
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
                EnemyBall bullet = go.GetComponent<EnemyBall>();
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
