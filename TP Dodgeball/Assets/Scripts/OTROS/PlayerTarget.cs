using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTarget : MonoBehaviour {
    public float elevateSight;
    private Player player;
    public bool targetModel;
    public Enemy enemy;
    private void Start()
    {

        if(Player.InstancePlayer != null)
        {
            player = Player.InstancePlayer;
        }
    }
    void Update () {
        if (targetModel == false)
        {
            TargetPlayer();
        }
        if (enemy.GetDead() == false) { 
            if (targetModel) {
                    transform.LookAt(new Vector3(Player.GetPlayer().transform.position.x, transform.position.y, Player.GetPlayer().transform.position.z));
                }
        }
	}
    public void TargetPlayer()
    {
        if(Player.GetPlayer() != null)
        {
            transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y+ elevateSight, player.transform.position.z));
        }
    }
}
