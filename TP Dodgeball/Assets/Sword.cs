using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

    // Use this for initialization
    private Player player;
    public float damage;
    private bool firstPuch;
    void Start () {
        firstPuch = false;
        if (Player.InstancePlayer != null)
        {
            player = Player.InstancePlayer;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            if (firstPuch)
            {
                player.life = player.life - damage;
            }
            else {
                firstPuch = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            firstPuch = true;
        }
    }
}
