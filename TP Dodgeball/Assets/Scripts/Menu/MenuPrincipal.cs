using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class MenuPrincipal : MonoBehaviour {

    // Use this for initialization
    public GameObject mainMenu;
    public GameObject informationMenu;
    public GameObject controlsMenu;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Play()
    {
        SceneManager.LoadScene("Pantalla de carga");
    }
    public void MainMenu()
    {
        mainMenu.SetActive(true);
        informationMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }
    public void InformationMenu()
    {
        mainMenu.SetActive(false);
        informationMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }
    public void ControlsMenu()
    {
        mainMenu.SetActive(false);
        informationMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
