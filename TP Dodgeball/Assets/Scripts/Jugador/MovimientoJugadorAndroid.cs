using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugadorAndroid : MonoBehaviour {

    // Use this for initialization
    public GameObject jugador;
    private Vector3 dir;
    private float x;
    private float z;
	void Start () {
        x = jugador.transform.position.x;
        z = jugador.transform.position.z;
        dir = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void MoverAdelante()
    {
        //z++;
        x++;
        dir = new Vector3(x, jugador.transform.position.y, z);
        jugador.transform.position = dir;
    }
    public void MoverAtras()
    {
        //z--;
        dir = new Vector3(x, jugador.transform.position.y, z);
        jugador.transform.position = dir;
        jugador.transform.SetPositionAndRotation(transform.position, new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w));
    }
    public void MoverIzquierda()
    {
       //x--;
        dir = new Vector3(x, jugador.transform.position.y, z);
        jugador.transform.position = dir;
        jugador.transform.Rotate(0, -90, 0);
    }
    public void MoverDerecha()
    {
        //x++;
        dir = new Vector3(x, jugador.transform.position.y, z);
        jugador.transform.position = dir;
        jugador.transform.Rotate(0, 90, 0);
    }
}