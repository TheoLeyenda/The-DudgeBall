using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class Menus : MonoBehaviour {

    // Use this for initialization
    private Player player;
    public GameObject generalMenu;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject weaponsMenu;
    public GameObject controlsMenu;
    public GameObject objetiveMenu;
    public GameObject mapMenu;
    private bool activate_disable;
    void Start() {
        if(Player.InstancePlayer != null)
        {
            player = Player.InstancePlayer;
        }
        activate_disable = false;
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P))
        {
            ActivedPauseMenu();
        }
        if(Player.GetPlayer().playerAndroid)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        ControlCursor(activate_disable);
    }
    public void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void ControlCursor(bool Activar_o_Desactivar)
    {
        if (Activar_o_Desactivar)
        {
            if (player != null)
            {
                if (player.playerWindows)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    Player.GetPlayer().pause = true;
                   
                }
            }
        }
        else
        {
            if (player != null && player.playerAndroid == false)
            {
                player.pause = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
    public void ActivedGeneralMenu()
    {
        generalMenu.SetActive(true);
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
        controlsMenu.SetActive(false);
        objetiveMenu.SetActive(false);
        mapMenu.SetActive(false);
        if (GameManager.GetGameManager() != null)
        {
            GameManager.GetGameManager().pause = true;
        }
    }
    public void DisableGeneralMenu()
    {
        generalMenu.SetActive(false);
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        controlsMenu.SetActive(false);
        objetiveMenu.SetActive(false);
        mapMenu.SetActive(false);
        if (GameManager.GetGameManager() != null)
        {
            GameManager.GetGameManager().pause = false;
        }
    }
    public void ActivedPauseMenu()
    {
        activate_disable = true;
        if (weaponsMenu != null && player != null)
        {
            if (player.playerAndroid)
            {
                if (pauseMenu != null && optionsMenu != null && weaponsMenu.activeSelf == false)
                {
                    pauseMenu.SetActive(true);
                    optionsMenu.SetActive(false);
                    controlsMenu.SetActive(false);
                    objetiveMenu.SetActive(false);
                    mapMenu.SetActive(false);
                    if (GameManager.GetGameManager() != null)
                    {
                        GameManager.GetGameManager().pause = true;
                    }
                }
            }
        }
        if(player != null)
        {
            if(player.playerWindows)
            {
                if (pauseMenu != null && optionsMenu != null)
                {
                    pauseMenu.SetActive(true);
                    optionsMenu.SetActive(false);
                    controlsMenu.SetActive(false);
                    objetiveMenu.SetActive(false);
                    mapMenu.SetActive(false);
                    if (GameManager.GetGameManager() != null)
                    {
                        GameManager.GetGameManager().pause = true;
                    }
                }
            }
        }
    }
   
    public void ActivedControlsMenu()
    {
        if(controlsMenu != null)
        {
            controlsMenu.SetActive(true);
            pauseMenu.SetActive(false);
            optionsMenu.SetActive(false);
            objetiveMenu.SetActive(false);
            mapMenu.SetActive(false);
            if (GameManager.GetGameManager() != null)
            {
                GameManager.GetGameManager().pause = true;
            }
        }
    }
    public void DisablePauseMenu()
    {
        activate_disable = false;
        if (pauseMenu != null && optionsMenu != null)
        {
            pauseMenu.SetActive(false);
            if (GameManager.GetGameManager() != null)
            {
                GameManager.GetGameManager().pause = false;
            }
        }
    }
    public void ActivedOptionsMenu()
    {
        if(optionsMenu != null && pauseMenu != null)
        {
            pauseMenu.SetActive(false);
            optionsMenu.SetActive(true);
            controlsMenu.SetActive(false);
            objetiveMenu.SetActive(false);
            mapMenu.SetActive(false);
            if (GameManager.GetGameManager() != null)
            {
                GameManager.GetGameManager().pause = true;
            }
        }
    }
    public void ReturnMenuPause()
    {
        if (optionsMenu != null && pauseMenu != null)
        {
            pauseMenu.SetActive(true);
            optionsMenu.SetActive(false);
            controlsMenu.SetActive(false);
            objetiveMenu.SetActive(false);
            mapMenu.SetActive(false);
            if (GameManager.GetGameManager() != null)
            {
                GameManager.GetGameManager().pause = true;
            }
        }
    }
    public void ActivedObjetiveMenu()
    {
        if(objetiveMenu != null)
        {
            objetiveMenu.SetActive(true);
            pauseMenu.SetActive(false);
            optionsMenu.SetActive(false);
            controlsMenu.SetActive(false);
            mapMenu.SetActive(false);
            if (GameManager.GetGameManager() != null)
            {
                GameManager.GetGameManager().pause = true;
            }
        }
    }
    public void ActivedMapMenu()
    {
        if(pauseMenu != null)
        {
            objetiveMenu.SetActive(false);
            pauseMenu.SetActive(false);
            optionsMenu.SetActive(false);
            controlsMenu.SetActive(false);
            mapMenu.SetActive(true);
        }
        if (GameManager.GetGameManager() != null)
        {
            GameManager.GetGameManager().pause = true;
        }
    }
}

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)