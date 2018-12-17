using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
public class TargetJugador : MonoBehaviour {
    public float elevateSight;
    private Jugador player;
    private void Start()
    {
        if(Jugador.InstancePlayer != null)
        {
            player = Jugador.InstancePlayer;
        }
    }
    void Update () {
        TargetPlayer();
	}
    public void TargetPlayer()
    {
        if(Jugador.GetPlayer() != null)
        {
            transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y+ elevateSight, player.transform.position.z));
        }
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
