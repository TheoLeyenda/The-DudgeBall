using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class MovimientoJugadorAndroid : MonoBehaviour {

    // Use this for initialization
    public GameObject player;
    private Vector3 dir;
    private float x;
    private float z;
	void Start () {
        x = player.transform.position.x;
        z = player.transform.position.z;
        dir = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void MoveForward()
    {
        //z++;
        x++;
        dir = new Vector3(x, player.transform.position.y, z);
        player.transform.position = dir;
    }
    public void MoveBack()
    {
        //z--;
        dir = new Vector3(x, player.transform.position.y, z);
        player.transform.position = dir;
        player.transform.SetPositionAndRotation(transform.position, new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w));
    }
    public void MoveLeft()
    {
       //x--;
        dir = new Vector3(x, player.transform.position.y, z);
        player.transform.position = dir;
        player.transform.Rotate(0, -90, 0);
    }
    public void MoveRight()
    {
        //x++;
        dir = new Vector3(x, player.transform.position.y, z);
        player.transform.position = dir;
        player.transform.Rotate(0, 90, 0);
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)