using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour {

    // Use this for initialization
    public GameObject menuPrincipal;
    public GameObject menuInformacion;
    public GameObject menuControles;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Jugar()
    {
        SceneManager.LoadScene("Pantalla de carga");
    }
    public void MenuPrin()
    {
        menuPrincipal.SetActive(true);
        menuInformacion.SetActive(false);
        menuControles.SetActive(false);
    }
    public void MenuInformacion()
    {
        menuPrincipal.SetActive(false);
        menuInformacion.SetActive(true);
        menuControles.SetActive(false);
    }
    public void MenuControles()
    {
        menuPrincipal.SetActive(false);
        menuInformacion.SetActive(false);
        menuControles.SetActive(true);
    }
}
