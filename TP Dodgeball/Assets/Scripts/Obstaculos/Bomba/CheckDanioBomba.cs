using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class CheckDanioBomba : MonoBehaviour {

    // Use this for initialization
    public float damage;
    private Jugador player;
    private void Start()
    {
        if(Jugador.InstancePlayer != null)
        {
            player = Jugador.InstancePlayer;
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
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
