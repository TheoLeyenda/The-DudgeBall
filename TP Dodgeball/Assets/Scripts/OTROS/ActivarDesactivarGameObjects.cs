using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
public class ActivarDesactivarGameObjects : MonoBehaviour {

    // Use this for initialization
    public GameObject[] objectsActived;
    public GameObject[] objectsDisable;
    public void Actived()
    {
        for (int i = 0; i < objectsActived.Length; i++)
        {
            if (objectsActived[i] != null)
            {
                objectsActived[i].SetActive(true);
            }
        }
        for(int i = 0; i< objectsDisable.Length; i++)
        {
            if (objectsDisable[i] != null)
            {
                objectsDisable[i].SetActive(false);
            }
        }
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Actived();
        }
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)