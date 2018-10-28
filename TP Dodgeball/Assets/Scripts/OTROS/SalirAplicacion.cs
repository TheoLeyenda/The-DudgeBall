using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalirAplicacion : MonoBehaviour {

    // Use this for initialization
    public bool DesactivarUpdate;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!DesactivarUpdate)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void salirApp()
    {
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
