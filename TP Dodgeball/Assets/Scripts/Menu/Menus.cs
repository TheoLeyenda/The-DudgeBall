﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour {

    // Use this for initialization
    private Jugador jugador;
    public GameObject menuGeneral;
    public GameObject menuPausa;
    public GameObject menuOpciones;
    public GameObject menuArmas;
    public GameObject menuControles;
    public GameObject menuObjetivo;
    public GameObject menuMapa;
    private bool activar_Desactivar;
    void Start() {
        if(Jugador.instanciaJugador != null)
        {
            jugador = Jugador.instanciaJugador;
        }
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
        if(Jugador.GetJugador().jugadorAndroid)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        ControlCursor(activar_Desactivar);
    }
    public void DesbloquearCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void ControlCursor(bool Activar_o_Desactivar)
    {
        if (Activar_o_Desactivar)
        {
            if (jugador != null)
            {
                if (jugador.jugadorWindows)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    Jugador.GetJugador().enPausa = true;
                   
                }
            }
        }
        else
        {
            if (jugador != null && jugador.jugadorAndroid == false)
            {
                jugador.enPausa = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
    public void ActivarMenuGeneral()
    {
        menuGeneral.SetActive(true);
        menuPausa.SetActive(true);
        menuOpciones.SetActive(false);
        menuControles.SetActive(false);
        menuObjetivo.SetActive(false);
        menuMapa.SetActive(false);
        if (GameManager.GetGameManager() != null)
        {
            GameManager.GetGameManager().pausa = true;
        }
    }
    public void DesactivarMenuGeneral()
    {
        menuGeneral.SetActive(false);
        menuPausa.SetActive(false);
        menuOpciones.SetActive(false);
        menuControles.SetActive(false);
        menuObjetivo.SetActive(false);
        menuMapa.SetActive(false);
        if (GameManager.GetGameManager() != null)
        {
            GameManager.GetGameManager().pausa = false;
        }
    }
    public void ActivarMenuPausa()
    {
        activar_Desactivar = true;
        if (menuArmas != null && jugador != null)
        {
            if (jugador.jugadorAndroid)
            {
                if (menuPausa != null && menuOpciones != null && menuArmas.activeSelf == false)
                {
                    menuPausa.SetActive(true);
                    menuOpciones.SetActive(false);
                    menuControles.SetActive(false);
                    menuObjetivo.SetActive(false);
                    menuMapa.SetActive(false);
                    if (GameManager.GetGameManager() != null)
                    {
                        GameManager.GetGameManager().pausa = true;
                    }
                }
            }
        }
        if(jugador != null)
        {
            if(jugador.jugadorWindows)
            {
                if (menuPausa != null && menuOpciones != null)
                {
                    menuPausa.SetActive(true);
                    menuOpciones.SetActive(false);
                    menuControles.SetActive(false);
                    menuObjetivo.SetActive(false);
                    menuMapa.SetActive(false);
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
            menuMapa.SetActive(false);
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
            menuMapa.SetActive(false);
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
            menuMapa.SetActive(false);
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
            menuMapa.SetActive(false);
            if (GameManager.GetGameManager() != null)
            {
                GameManager.GetGameManager().pausa = true;
            }
        }
    }
    public void ActivarMenuMapa()
    {
        if(menuPausa != null)
        {
            menuObjetivo.SetActive(false);
            menuPausa.SetActive(false);
            menuOpciones.SetActive(false);
            menuControles.SetActive(false);
            menuMapa.SetActive(true);
        }
        if (GameManager.GetGameManager() != null)
        {
            GameManager.GetGameManager().pausa = true;
        }
    }
}