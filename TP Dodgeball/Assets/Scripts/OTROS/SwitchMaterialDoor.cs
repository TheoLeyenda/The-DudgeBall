using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMaterialDoor : MonoBehaviour {

    // Use this for initialization
    public Material newMaterial;
    public GameObject objectToChangeMaterial;
    private float count= 0;
    private bool startCount= false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (newMaterial != null && objectToChangeMaterial != null)
            {
                objectToChangeMaterial.SetActive(true);
                objectToChangeMaterial.GetComponent<MeshRenderer>().sharedMaterial = newMaterial;
                startCount = true;
            }
        }
    }
    private void Update()
    {
        if(startCount)
        {
            count = count + Time.deltaTime;
        }
        if(count >= 1)
        {
            gameObject.SetActive(false);
        }
    }
    public void SwitchMaterial()
    {
        if (newMaterial != null && objectToChangeMaterial != null)
        {
            objectToChangeMaterial.SetActive(true);
            objectToChangeMaterial.GetComponent<MeshRenderer>().sharedMaterial = newMaterial;
            startCount = true;
        }
    }
}
