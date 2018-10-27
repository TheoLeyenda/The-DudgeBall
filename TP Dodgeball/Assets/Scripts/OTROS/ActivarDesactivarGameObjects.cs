using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivarDesactivarGameObjects : MonoBehaviour {

    // Use this for initialization
    public GameObject[] objectsActivar;
    public GameObject[] objectsDesactivar;
    public void Activar()
    {
        for (int i = 0; i < objectsActivar.Length; i++)
        {
            if (objectsActivar[i] != null)
            {
                objectsActivar[i].SetActive(true);
            }
        }
        for(int i = 0; i< objectsDesactivar.Length; i++)
        {
            if (objectsDesactivar[i] != null)
            {
                objectsDesactivar[i].SetActive(false);
            }
        }
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Activar();
        }
    }
}
