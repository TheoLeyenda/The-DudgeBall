using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCurva : MonoBehaviour {

    // Use this for initialization
    public bool abajo;
    public bool arriba;
    public bool izquierda;
    public bool derecha;

    public bool abajoArriba;
    public bool abajoIzquierda;
    public bool abajoDerecha;
    public bool todoMenosArriba;

    public bool arribaDerecha;
    public bool arribaIzquierda;
    public bool todoMenosAbajo;

    public bool todasDirecciones;

    public bool todoMenosDerecha;
    public bool todoMenosIzquierda;

    public bool DerechaIzquierda;
    public bool ArribaAbajo;

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
            if(ArribaAbajo)
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
            if(DerechaIzquierda)
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
            if (abajo)
            {
                //Abajo
                other.transform.Rotate(0, -180, 0);
            }
            if(arriba)
            {
                //no pasa nada porque sigue el mismo rumbo
            }
            if(izquierda)
            {
                //Izquierda
                other.transform.Rotate(0, 270, 0);
            }
            if(derecha)
            {
                //Derecha
                other.transform.Rotate(0, 90, 0);
            }
            if(abajoArriba)
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
            if(abajoIzquierda)
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
            if(abajoDerecha)
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
            if(todoMenosArriba)
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
            if(arribaDerecha)
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
            if(arribaIzquierda)
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
            if(todoMenosAbajo)
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
            if(todoMenosDerecha)
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
            if(todoMenosIzquierda)
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
            if(todasDirecciones)
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
