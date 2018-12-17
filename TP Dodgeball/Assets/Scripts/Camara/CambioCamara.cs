using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
public class CambioCamara : MonoBehaviour {

    // Use this for initialization
    public GameObject cameraPrincipal;
    public GameObject cameraSecond;
    public GameObject[] ObjectsOnScreen;
    public GameObject hat;
	void Start () {
        if (hat != null)
        {
            hat.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnMainCamera()
    {
        if (hat != null)
        {
            hat.SetActive(false);
        }
        if(cameraPrincipal != null)
        {
            cameraPrincipal.SetActive(true);
            cameraSecond.SetActive(false);
            if (ObjectsOnScreen != null)
            {
                for (int i = 0; i < ObjectsOnScreen.Length; i++)
                {
                    if (ObjectsOnScreen[i] != null)
                    {
                        ObjectsOnScreen[i].SetActive(true);
                    }
                }
            }
        }
    }
    public void OnSecundaryCamera()
    {
        if (hat != null)
        {
            hat.SetActive(true);
        }
        if(cameraSecond != null)
        {
            cameraSecond.SetActive(true);
            cameraPrincipal.SetActive(false);
            if (ObjectsOnScreen != null)
            {
                for (int i = 0; i < ObjectsOnScreen.Length; i++)
                {
                    if (ObjectsOnScreen[i] != null && ObjectsOnScreen[i].activeSelf == true)
                    {
                        ObjectsOnScreen[i].SetActive(false);
                    }
                }
            }
        }
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
