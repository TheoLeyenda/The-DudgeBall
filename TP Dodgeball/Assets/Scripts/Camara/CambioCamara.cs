using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioCamara : MonoBehaviour {

    // Use this for initialization
    public GameObject camaraPrincipal;
    public GameObject camaraSecundaria;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PrenderCamaraPrincipal()
    {
        if(camaraPrincipal != null)
        {
            camaraPrincipal.SetActive(true);
            camaraSecundaria.SetActive(false);
        }
    }
    public void PrenderCamaraSecundaria()
    {
        if(camaraSecundaria != null)
        {
            camaraSecundaria.SetActive(true);
            camaraPrincipal.SetActive(false);
        }
    }
}
