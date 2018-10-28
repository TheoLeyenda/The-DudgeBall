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
    private bool activar_Desactivar;
    void Start() {
        activar_Desactivar = false;
        if (menuPausa != null)
        {
            menuPausa.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P))
        {
            ActivarMenuPausa();
        }
        ControlCursor(activar_Desactivar);
    }
    public void ControlCursor(bool Activar_o_Desactivar)
    {
        if (Activar_o_Desactivar)
        {
            if (Jugador.GetJugador() != null)
            {
                if (Jugador.GetJugador().jugadorWindows)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    Jugador.GetJugador().enPausa = true;
                   
                }
            }
        }
        else
        {
            if (Jugador.GetJugador() != null)
            {
                Jugador.GetJugador().enPausa = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
    public void ActivarMenuPausa()
    {
        activar_Desactivar = true;
        if (menuArmas != null && Jugador.GetJugador() != null)
        {
            if (Jugador.GetJugador().jugadorAndroid)
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
        if(Jugador.GetJugador() != null)
        {
            if(Jugador.GetJugador().jugadorWindows)
            {
                if (menuPausa != null && menuOpciones != null)
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
        activar_Desactivar = false;
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