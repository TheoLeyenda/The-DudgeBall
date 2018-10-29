using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCamara : MonoBehaviour {

    // Use this for initialization
    public GameObject camara;
    private float x;
    private float y;
    private float z;
    private Vector3 dir;
	void Start () {
        x = camara.transform.position.x;
        y = camara.transform.position.y;
        z = camara.transform.position.z;
        dir = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
        Movimiento();
	}
    public void Movimiento()
    {
        if(Input.GetKey(KeyCode.S))
        {
            y++;
        }
        if(Input.GetKey(KeyCode.W))
        {
            y--;
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            x--;
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            x++;
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            z++;
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            z--;
        }
        dir = new Vector3(x, y, z);
        camara.transform.position = dir;
    }
    public void Subir()
    {
        y++;
        dir = new Vector3(x, y, z);
        camara.transform.position = dir;
    }
    public void Bajar()
    {
        y--;
        dir = new Vector3(x, y, z);
        camara.transform.position = dir;
    }
    public void MoverAdelante()
    {
        z++;
        dir = new Vector3(x, y, z);
        camara.transform.position = dir;
    }
    public void MoverAtras()
    {
        z--;
        dir = new Vector3(x, y, z);
        camara.transform.position = dir;
    }
    public void MoverIzquierda()
    {
        x--;
        Debug.Log(x);
        dir = new Vector3(x, y, z);
        camara.transform.position = dir;
    }
    public void MoverDerecha()
    {
        x++;
        dir = new Vector3(x, y, z);
        camara.transform.position = dir;
    }
}

