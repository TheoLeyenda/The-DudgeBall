using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioCamara : MonoBehaviour {

    // Use this for initialization
    public GameObject camaraPrincipal;
    public GameObject camaraSecundaria;
    public GameObject[] cosasEnPantalla;
    public GameObject sombrero;
	void Start () {
        if (sombrero != null)
        {
            sombrero.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PrenderCamaraPrincipal()
    {
        if (sombrero != null)
        {
            sombrero.SetActive(false);
        }
        if(camaraPrincipal != null)
        {
            camaraPrincipal.SetActive(true);
            camaraSecundaria.SetActive(false);
            if (cosasEnPantalla != null)
            {
                for (int i = 0; i < cosasEnPantalla.Length; i++)
                {
                    if (cosasEnPantalla[i] != null)
                    {
                        cosasEnPantalla[i].SetActive(true);
                    }
                }
            }
        }
    }
    public void PrenderCamaraSecundaria()
    {
        if (sombrero != null)
        {
            sombrero.SetActive(true);
        }
        if(camaraSecundaria != null)
        {
            camaraSecundaria.SetActive(true);
            camaraPrincipal.SetActive(false);
            if (cosasEnPantalla != null)
            {
                for (int i = 0; i < cosasEnPantalla.Length; i++)
                {
                    if (cosasEnPantalla[i] != null && cosasEnPantalla[i].activeSelf == true)
                    {
                        cosasEnPantalla[i].SetActive(false);
                    }
                }
            }
        }
    }
}
