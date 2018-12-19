using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDamageBomb : MonoBehaviour {

    // Use this for initialization
    public float damage;
    private Player player;
    private void Start()
    {
        if(Player.InstancePlayer != null)
        {
            player = Player.InstancePlayer;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (player != null)
            {
                if (player.armor > 0)
                {
                    player.armor = 0;
                }
                else
                {
                    player.life = player.life - damage;
                }
            }
        }
    }
}
