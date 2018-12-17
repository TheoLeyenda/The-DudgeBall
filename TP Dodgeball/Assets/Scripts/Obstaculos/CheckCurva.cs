using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class CheckCurva : MonoBehaviour {

    // Use this for initialization
    public bool down;//abajo
    public bool up;//arriba
    public bool left;//izquierda
    public bool right;//derecha

    public bool down_up;
    public bool down_left;
    public bool down_right;
    public bool everythingButUp;

    public bool up_right;
    public bool up_left;
    public bool everythingButDown;

    public bool allDirections;

    public bool everythingButRight;
    public bool everythingButLeft;

    public bool left_Right;
    public bool up_down;

    private float centerX;
    private float centerZ;
    private Vector3 pos;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Corredor" || other.tag == "Tirador")
        {
            if(up_down)
            {
                float opcion = Random.Range(0, 2);
                if(opcion >= 1)
                {
                    //no pasa nada porque sigue el mismo rumbo
                }
                if (opcion >= 0 && opcion < 1)
                {
                    //Abajo
                    other.transform.Rotate(0, -180, 0);
                }
            }
            if(left_Right)
            {
                float opcion = Random.Range(0, 2);
                if (opcion >= 1)
                {
                    //Izquierda
                    other.transform.Rotate(0, 270, 0);
                }
                if (opcion >= 0 && opcion < 1)
                {
                    //Derecha
                    other.transform.Rotate(0, 90, 0);
                }
            }
            //other.transform.position = new Vector3(transform.position.x, other.transform.position.y, transform.position.z);
            if (down)
            {
                //Abajo
                other.transform.Rotate(0, -180, 0);
            }
            if(up)
            {
                //no pasa nada porque sigue el mismo rumbo
            }
            if(left)
            {
                //Izquierda
                other.transform.Rotate(0, 270, 0);
            }
            if(right)
            {
                //Derecha
                other.transform.Rotate(0, 90, 0);
            }
            if(down_up)
            {
                float opcion = Random.Range(0,2);
                if(opcion >=1 )
                {
                    //Arriba
                    //no pasa nada porque sigue el mismo rumbo;
                }
                if(opcion >= 0 && opcion < 1)
                {
                    //Abajo
                    other.transform.Rotate(0, -180, 0);
                }
            }
            if(down_left)
            {
                float opcion = Random.Range(0, 2);
                if (opcion >= 1)
                {
                    //Izquierda
                    other.transform.Rotate(0, 270, 0);
                }
                if (opcion >= 0 && opcion < 1)
                {
                    //Abajo
                    other.transform.Rotate(0, -180, 0);
                }
            }
            if(down_right)
            {
                float opcion = Random.Range(0, 2);
                if (opcion >= 1)
                {
                    //Derecha
                    other.transform.Rotate(0, 90, 0);
                }
                if (opcion >= 0 && opcion < 1)
                {
                    //Abajo
                    other.transform.Rotate(0, -180, 0);
                }
            }
            if(everythingButUp)
            {
                float opcion = Random.Range(0, 3);
                if(opcion >= 0 && opcion <1)
                {
                    //Abajo
                    other.transform.Rotate(0, -180, 0);
                }
                if(opcion >= 1 && opcion < 2)
                {
                    //Izquierda
                    other.transform.Rotate(0, 270, 0);
                }
                if(opcion >= 2)
                {
                    //Derecha
                    other.transform.Rotate(0, 90, 0);
                }
            }
            if(up_right)
            {
                float opcion = Random.Range(0, 2);
                if(opcion >= 0 && opcion < 1)
                {
                    //Arriba
                    //no pasa nada porque sigue el mismo rumbo;
                }
                if(opcion >= 1)
                {
                    //Derecha
                    other.transform.Rotate(0, 90, 0);
                }
            }
            if(up_left)
            {
                float opcion = Random.Range(0, 2);
                Debug.Log(opcion);
                if (opcion >= 0 && opcion < 1)
                {
                    //Arriba
                    //no pasa nada porque sigue el mismo rumbo;
                }
                if (opcion >= 1)
                {
                    //Izquierda
                    other.transform.Rotate(0, 270, 0);
                }
            }
            if(everythingButDown)
            {
                float opcion = Random.Range(0, 3);
                if (opcion >= 0 && opcion < 1)
                {
                    //Arriba
                    //no pasa nada porque sigue el mismo rumbo;
                }
                if (opcion >= 1 && opcion < 2)
                {
                    //Izquierda
                    other.transform.Rotate(0, 270, 0);
                }
                if (opcion >= 2)
                {
                    //Derecha
                    other.transform.Rotate(0, 90, 0);
                }
            }
            if(everythingButRight)
            {
                float opcion = Random.Range(0, 3);
                if (opcion >= 0 && opcion < 1)
                {
                    //Arriba
                    //no pasa nada porque sigue el mismo rumbo;
                }
                if (opcion >= 1 && opcion < 2)
                {
                    //Izquierda
                    other.transform.Rotate(0, 270, 0);
                }
                if (opcion >= 2)
                {
                    //abajo
                    other.transform.Rotate(0, 180, 0);
                }
            }
            if(everythingButLeft)
            {
                float opcion = Random.Range(0, 3);
                if (opcion >= 0 && opcion < 1)
                {
                    //Arriba
                    //no pasa nada porque sigue el mismo rumbo;
                }
                if (opcion >= 1 && opcion < 2)
                {
                    //abajo
                    other.transform.Rotate(0, 180, 0);
                }
                if (opcion >= 2)
                {
                    //Derecha
                    other.transform.Rotate(0, 90, 0);
                }
            }
            if(allDirections)
            {
                float opcion = Random.Range(0, 4);
                if (opcion >= 0 && opcion < 1)
                {
                    //Arriba
                    //no pasa nada porque sigue el mismo rumbo;
                }
                if (opcion >= 1 && opcion < 2)
                {
                    //Izquierda
                    other.transform.Rotate(0, 270, 0);
                }
                if (opcion >= 2 && opcion < 3)
                {
                    //Derecha
                    other.transform.Rotate(0, 90, 0);
                }
                if(opcion >= 3)
                {
                    //Abajo
                    other.transform.Rotate(0, -180, 0);
                }
            }
        }
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
