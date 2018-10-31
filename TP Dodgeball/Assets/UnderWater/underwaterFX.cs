using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class underwaterFX : MonoBehaviour {

    // Use this for initialization
    public int underwaterLevel;
    private Color color;
    public Camera camara;
    //private bool defaultFog = RenderSettings.fog;
    //private Color defaultFogColor = RenderSettings.fogColor;
    //private float defaultFogDensity = RenderSettings.fogDensity;
    
    void Start () {
        if (camara != null)
        {
            camara.backgroundColor = new Color(color.r, color.g, color.b, color.a);
        }
    }
	
	// Update is called once per frame
	void Update () {
        /*if (transform.position.y < underwaterLevel)
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0, 0.4f, 0.7f, 0.6f);
            RenderSettings.fogDensity = 0.04f;
            //RenderSettings.skybox = noSkybox;
        }

        else
        {
            RenderSettings.fog = defaultFog;
            RenderSettings.fogColor = defaultFogColor;
            RenderSettings.fogDensity = defaultFogDensity;
            RenderSettings.skybox = defaultSkybox;
        }*/
        if (camara != null)
        {
            
            if (transform.position.y > underwaterLevel)
            {
                color.r = 0;
                color.g = 0.4f;
                color.b = 0.7f;
                color.a = 1;
                camara.backgroundColor = new Color(color.r, color.g, color.b, color.a);
                
            }
            else
            {
                color.r = 0;
                color.g = 0.4f;
                color.b = 0.7f;
                color.a = 0.6f;
                camara.backgroundColor = new Color(color.r, color.g, color.b, color.a);
            }
        }
    }
}
