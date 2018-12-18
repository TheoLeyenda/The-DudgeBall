using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class MunicionEspecial : MonoBehaviour {

    // Use this for initialization
    public GameObject pickUp;
    private Jugador player;
	void Start () {
		if(Jugador.InstancePlayer != null)
        {
            player = Jugador.InstancePlayer;
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
