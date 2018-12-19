using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTarget : MonoBehaviour {
    public float elevateSight;
    private Player player;
    private void Start()
    {
        if(Player.InstancePlayer != null)
        {
            player = Player.InstancePlayer;
        }
    }
    void Update () {
        TargetPlayer();
	}
    public void TargetPlayer()
    {
        if(Player.GetPlayer() != null)
        {
            transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y+ elevateSight, player.transform.position.z));
        }
    }
}
