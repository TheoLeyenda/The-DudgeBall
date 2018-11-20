using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour {

    // Use this for initialization
    public bool arribaAbajo;
    public bool izquierdaDerecha;
    public float Distancia;
    public float vel;
    private Vector3 pos1;
    private Vector3 pos2;
    private Vector3 pos1Final;
    private Vector3 pos2Final;
    private int ruta;
    private Rigidbody rig;
	void Start () {
        rig = GetComponent<Rigidbody>();
        pos1 = transform.position;
        ruta = 2;
        if(arribaAbajo)
        {
            pos2Final = transform.position + new Vector3(0, transform.position.y + Distancia, 0);
            pos1Final = transform.position;
        }
        if(izquierdaDerecha)
        {
            pos1Final = transform.position;
            pos2Final = transform.position + new Vector3(0, 0, transform.position.z + Distancia);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    private void FixedUpdate()
    {
        if (arribaAbajo)
        {
            MovArribaAbajo();
        }
        if(izquierdaDerecha)
        {
            MovIzquierdaDerecha();
        }
    }
    public void MovArribaAbajo()
    {
        if (ruta == 2)
        {
            pos2 = transform.position + new Vector3(0,0.1f,0) * Time.deltaTime * vel;
            rig.MovePosition(pos2);
            if(transform.position.y > pos2Final.y)
            {
                ruta = 1;
            }
        }
        if(ruta == 1)
        {
            pos1 = transform.position + new Vector3(0, -0.1f, 0) * Time.deltaTime * vel;
            rig.MovePosition(pos1);
            if (transform.position.y <= pos1Final.y)
            {
                ruta = 2;
            }
        }
    }
    public void MovIzquierdaDerecha()
    {
        if (ruta == 2)
        {
            pos2 = transform.position + new Vector3(0,0, 0.1f) * Time.deltaTime * vel;
            rig.MovePosition(pos2);
            if (transform.position.z > pos2Final.z)
            {
                ruta = 1;
            }
        }
        if (ruta == 1)
        {
            pos1 = transform.position + new Vector3(0, 0, -0.1f) * Time.deltaTime * vel;
            rig.MovePosition(pos1);
            if (transform.position.z <= pos1Final.z)
            {
                ruta = 2;
            }
        }
    }
}
