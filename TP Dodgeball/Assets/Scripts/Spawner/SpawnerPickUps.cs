using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class SpawnerPickUps : MonoBehaviour {

    // Use this for initialization
    public PoolPelota poolpickUp;
    private PoolObject poolObject;
    private void Start()
    {
    }
    private void OnEnable()
    {
        GameObject go = poolpickUp.GetObject();
        pickUp pickUp = go.GetComponent<pickUp>();
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;
        pickUp.On();
        gameObject.SetActive(false);
    }
}

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
