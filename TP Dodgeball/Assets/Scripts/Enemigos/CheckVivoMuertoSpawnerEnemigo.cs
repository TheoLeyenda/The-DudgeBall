using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckVivoMuertoSpawnerEnemigo : MonoBehaviour {

    // Use this for initialization
    private bool unaVez = true;
    public int zona;
    private void OnDisable()
    {
        if(unaVez)
        {
            unaVez = false;
            if (LaberintoManager.GetLaberintoManager() != null)
            {
                LaberintoManager.GetLaberintoManager().SumarSpawnerDestruidos(zona);
            }
        }
    }
}
