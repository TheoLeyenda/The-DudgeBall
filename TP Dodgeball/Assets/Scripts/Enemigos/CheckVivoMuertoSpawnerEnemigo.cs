using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckVivoMuertoSpawnerEnemigo : MonoBehaviour {

    // Use this for initialization
    private bool unaVez = true;
    public int zona;
    private LaberintoManager laberintoManager;
    private void Start()
    {
        if(LaberintoManager.instanciaLaberintoManager != null)
        {
            laberintoManager = LaberintoManager.instanciaLaberintoManager;
        }
    }
    private void OnDisable()
    {
        if(unaVez)
        {
            unaVez = false;
            if (laberintoManager != null)
            {
                laberintoManager.SumarSpawnerDestruidos(zona);
            }
        }
    }
}
