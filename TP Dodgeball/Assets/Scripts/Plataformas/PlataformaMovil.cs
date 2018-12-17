using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class PlataformaMovil : MonoBehaviour {

    // Use this for initialization
    public bool upDown;
    public bool leftRight;
    public float distance;
    public float vel;
    private Vector3 pos1;
    private Vector3 pos2;
    private Vector3 pos1Final;
    private Vector3 pos2Final;
    private int route;
    private Rigidbody rig;
	void Start () {
        rig = GetComponent<Rigidbody>();
        pos1 = transform.position;
        route = 2;
        if(upDown)
        {
            pos2Final = transform.position + new Vector3(0, transform.position.y + distance, 0);
            pos1Final = transform.position;
        }
        if(leftRight)
        {
            pos1Final = transform.position;
            pos2Final = transform.position + new Vector3(0, 0, transform.position.z + distance);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    private void FixedUpdate()
    {
        if (upDown)
        {
            MovUpDown();
        }
        if(leftRight)
        {
            MovLeftRight();
        }
    }
    public void MovUpDown()
    {
        if (route == 2)
        {
            pos2 = transform.position + new Vector3(0,0.1f,0) * Time.deltaTime * vel;
            rig.MovePosition(pos2);
            if(transform.position.y > pos2Final.y)
            {
                route = 1;
            }
        }
        if(route == 1)
        {
            pos1 = transform.position + new Vector3(0, -0.1f, 0) * Time.deltaTime * vel;
            rig.MovePosition(pos1);
            if (transform.position.y <= pos1Final.y)
            {
                route = 2;
            }
        }
    }
    public void MovLeftRight()
    {
        if (route == 2)
        {
            pos2 = transform.position + new Vector3(0,0, 0.1f) * Time.deltaTime * vel;
            rig.MovePosition(pos2);
            if (transform.position.z > pos2Final.z)
            {
                route = 1;
            }
        }
        if (route == 1)
        {
            pos1 = transform.position + new Vector3(0, 0, -0.1f) * Time.deltaTime * vel;
            rig.MovePosition(pos1);
            if (transform.position.z <= pos1Final.z)
            {
                route = 2;
            }
        }
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)