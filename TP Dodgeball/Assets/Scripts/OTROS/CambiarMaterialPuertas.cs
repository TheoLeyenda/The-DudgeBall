using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarMaterialPuertas : MonoBehaviour {

    // Use this for initialization
    public Material nuevoMaterial;
    public GameObject objetaAcambiarMaterial;
    private float contar= 0;
    private bool EmpezarContar= false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (nuevoMaterial != null && objetaAcambiarMaterial != null)
            {
                objetaAcambiarMaterial.SetActive(true);
                objetaAcambiarMaterial.GetComponent<MeshRenderer>().sharedMaterial = nuevoMaterial;
                EmpezarContar = true;
            }
        }
    }
    private void Update()
    {
        if(EmpezarContar)
        {
            contar = contar + Time.deltaTime;
        }
        if(contar >= 1)
        {
            gameObject.SetActive(false);
        }
    }
}
