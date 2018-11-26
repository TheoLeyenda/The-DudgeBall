using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PantallaCarga : MonoBehaviour {

    // Use this for initialization
    public Text porsentaje;
    public GameObject marcoBarraDeCarga;
    public GameObject carga;
    public float porsentajeMaximoCarga;
    public float velocidadCarga;
    public string PantallaAcargar;
    private float porsentajeCarga;
    private bool pasarNivel;
    public bool usarString;
    private int nivelACargar;
    public bool noCargar;
    private EstructuraDatosAuxiliares estructuraDatos;
	void Start () {
        System.GC.Collect();
        porsentajeCarga = 0;
        if(EstructuraDatosAuxiliares.estructuraDatosAuxiliares != null)
        {
            estructuraDatos = EstructuraDatosAuxiliares.estructuraDatosAuxiliares;
        }
        pasarNivel = false;
	}

    // Update is called once per frame
    void Update() {

        if (!noCargar)
        {
            UpdateBarraCarga();
            if (usarString)
            {
                estructuraDatos.PasarNivel();
            }
            if (pasarNivel && !usarString)
            {
                estructuraDatos.PasarNivel();
            }
        }
	}
    public void UpdateBarraCarga()
    {
        porsentajeCarga = porsentajeCarga + Time.deltaTime * velocidadCarga;
        if (carga != null)
        {
            float z = (float)porsentajeCarga / (float)porsentajeMaximoCarga;
            Vector3 ScaleBar = new Vector3(1, 1, z);
            carga.transform.localScale = ScaleBar;
        }
        porsentaje.text = "" + (int)porsentajeCarga + "%";
        if (porsentajeCarga >= porsentajeMaximoCarga && !pasarNivel && !usarString)
        {
            estructuraDatos.SetNivel(estructuraDatos.datosNivel.I_nivelAcargar + 1);
            porsentajeCarga = 0;
            pasarNivel = true;
        }
        if(porsentajeCarga >= porsentajeMaximoCarga && !pasarNivel && usarString)
        {
            estructuraDatos.SetNivel(PantallaAcargar);
            porsentajeCarga = 0;
            pasarNivel = true;
        }
    }
    public void ResetNivel()
    {
        estructuraDatos.SetNivel(0);
        SceneManager.LoadScene("SplashScreen");
    }
}
