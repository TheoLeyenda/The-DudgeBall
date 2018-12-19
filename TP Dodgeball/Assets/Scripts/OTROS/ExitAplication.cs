using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitAplication : MonoBehaviour {

    // Use this for initialization
    public bool DisableUpdate;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!DisableUpdate)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ExitApp()
    {
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
