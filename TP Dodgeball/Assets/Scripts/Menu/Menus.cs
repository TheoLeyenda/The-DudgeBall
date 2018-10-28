using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour {

    // Use this for initialization
    public GameObject menuPausa;
    public GameObject menuOpciones;
    public GameObject menuArmas;
    public GameObject menuControles;
    public GameObject menuObjetivo;
    void Start() {
        if (menuPausa != null)
        {
            menuPausa.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ActivarMenuPausa()
    {
        if (menuArmas != null)
        {
            if (menuPausa != null && menuOpciones != null && menuArmas.activeSelf == false)
            {
                menuPausa.SetActive(true);
                menuOpciones.SetActive(false);
                menuControles.SetActive(false);
                menuObjetivo.SetActive(false);
                if (GameManager.GetGameManager() != null)
                {
                    GameManager.GetGameManager().pausa = true;
                }
            }
        }
    }
    public void ActivarMenuControles()
    {
        if(menuControles != null)
        {
            menuControles.SetActive(true);
            menuPausa.SetActive(false);
            menuOpciones.SetActive(false);
            menuObjetivo.SetActive(false);
            if (GameManager.GetGameManager() != null)
            {
                GameManager.GetGameManager().pausa = true;
            }
        }
    }
    public void DesactivarMenuPausa()
    {
        if (menuPausa != null && menuOpciones != null)
        {
            menuPausa.SetActive(false);
            if (GameManager.GetGameManager() != null)
            {
                GameManager.GetGameManager().pausa = false;
            }
        }
    }
    public void ActivarMenuOpciones()
    {
        if(menuOpciones != null && menuPausa != null)
        {
            menuPausa.SetActive(false);
            menuOpciones.SetActive(true);
            menuControles.SetActive(false);
            menuObjetivo.SetActive(false);
            if (GameManager.GetGameManager() != null)
            {
                GameManager.GetGameManager().pausa = true;
            }
        }
    }
    public void VolverMenuPausa()
    {
        if (menuOpciones != null && menuPausa != null)
        {
            menuPausa.SetActive(true);
            menuOpciones.SetActive(false);
            menuControles.SetActive(false);
            menuObjetivo.SetActive(false);
            if (GameManager.GetGameManager() != null)
            {
                GameManager.GetGameManager().pausa = true;
            }
        }
    }
    public void ActivarMenuObjetivo()
    {
        if(menuObjetivo != null)
        {
            menuObjetivo.SetActive(true);
            menuPausa.SetActive(false);
            menuOpciones.SetActive(false);
            menuControles.SetActive(false);
            if (GameManager.GetGameManager() != null)
            {
                GameManager.GetGameManager().pausa = true;
            }
        }
    }

}