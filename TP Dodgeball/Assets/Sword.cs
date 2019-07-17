using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

    // Use this for initialization
    private Player player;
    public float damage;
    private bool firstPuch;
    public Runner runner;
    public Wizard wizard;
    void Start () {
        
        firstPuch = false;
        if (Player.InstancePlayer != null)
        {
            player = Player.InstancePlayer;
        }
            

    }
	
	// Update is called once per frame
	void Update () {
        if (runner != null)
        {
            
            if (runner.life <= 0)
            {
                GetComponent<BoxCollider>().enabled = false;
            }
            else if (runner.life > 0)
            {
                GetComponent<BoxCollider>().enabled = true;
            }
        }
        else if (wizard != null) {
            if (wizard.life <= 0)
            {
                GetComponent<BoxCollider>().enabled = false;
            }
            else if (wizard.life > 0)
            {
                GetComponent<BoxCollider>().enabled = true;
            }
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            if (!player.GetImmune())
            {
                if (wizard != null)
                {
                    if (wizard.GetDead() == false)
                    {
                        if (firstPuch)
                        {
                            player.audioSource2.PlayOneShot(player.soundDamageMe);
                            if (player.armor > 0)
                            {
                                player.armor = player.armor - damage;
                            }
                            else
                            {
                                player.life = player.life - damage;
                            }
                        }
                        else
                        {
                            firstPuch = true;
                        }
                    }
                }
                else if (runner != null)
                {
                    if (runner.GetDead() == false)
                    {
                        if (firstPuch)
                        {
                            player.audioSource2.PlayOneShot(player.soundDamageMe);
                            if (player.armor > 0)
                            {
                                player.armor = player.armor - damage;
                            }
                            else
                            {
                                player.life = player.life - damage;
                            }
                        }
                        else
                        {
                            firstPuch = true;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            firstPuch = true;
            if (wizard != null) {
                wizard.aviableShoot = true;
            }
        }
       
    }
}
