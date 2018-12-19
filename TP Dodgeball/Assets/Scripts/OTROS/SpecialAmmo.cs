using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class SpecialAmmo : MonoBehaviour {

    // Use this for initialization
    public GameObject pickUp;
    private Player player;
	void Start () {
		if(Player.InstancePlayer != null)
        {
            player = Player.InstancePlayer;
        }
	}
	
	// Update is called once per frame
	void Update () {
        checkAmmoPlayer();
	}
    public void checkAmmoPlayer()
    {
        if(player.GetAmmoFragmentBall() <= 0)
        {
            pickUp.gameObject.SetActive(true);
        }
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
