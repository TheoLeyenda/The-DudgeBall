using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class ActivarMultiplesPuertas : MonoBehaviour {

    // Use this for initialization
    private DataStructure dataStructure;
    private Jugador player;
    public PuertaRejas[] doors;
    public Enemigo enemy;
    public SphereCollider sphereCollider;
	void Start () { 
        if(DataStructure.auxiliaryDataStructure != null)
        {
            dataStructure = DataStructure.auxiliaryDataStructure;
        }
        if(Jugador.InstancePlayer != null)
        {
            player = Jugador.InstancePlayer;
        }
	}
	
	// Update is called once per frame
	void Update () {
        CheckOpenDoor();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            enemy.gameObject.SetActive(true);
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].SetCloseDoor(true);
            }
            if (sphereCollider != null)
            {
                sphereCollider.enabled = false;
            }
        }
    }
    public void CheckOpenDoor()
    {
        if(enemy != null)
        {
            if (enemy.life <= 0)
            {
                dataStructure.SetPlayerData(player);
                dataStructure.SetPlayerValues(player);
                for (int i = 0; i < doors.Length; i++)
                {
                    doors[i].SetOpenDoor(true);
                }
                if (sphereCollider != null)
                {
                    sphereCollider.enabled = true;
                }
            }
        }
    }

}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)