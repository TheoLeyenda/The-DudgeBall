using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateDisableGameObject : MonoBehaviour {

    // Use this for initialization
    public GameObject[] objectsActived;
    public GameObject[] objectsDisable;
    public bool dontDisableMe;
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
        if (!dontDisableMe)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Actived();
        }
    }
}