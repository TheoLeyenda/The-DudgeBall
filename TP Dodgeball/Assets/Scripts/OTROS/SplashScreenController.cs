using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{

    // Use this for initialization
    public GameObject logoEmpresa;
    public GameObject logoJuego;
    public bool menuWindows;
    public bool menuAndroid;
    private float TransparenciaLogoEmpresa;
    private float TransparenciaLogoJuego;
    private bool entrarLogoEmpresa;
    private bool entrarLogoJuego;
    private bool soloUnaVez;
    private bool sumar;
    private bool restar;
    private float tiempo;
    private float diley;
    void Start()
    {
        TransparenciaLogoEmpresa = 0;
        TransparenciaLogoJuego = 0;
        entrarLogoEmpresa = false;
        entrarLogoJuego = false;
        sumar = true;
        restar = false;
        soloUnaVez = true;
        tiempo = 0;
        diley = 0;

        Material tempMatEmpresa = logoEmpresa.GetComponent<MeshRenderer>().sharedMaterial;
        tempMatEmpresa.color = new Color(tempMatEmpresa.color.r, tempMatEmpresa.color.g, tempMatEmpresa.color.b, TransparenciaLogoEmpresa);
        //Material tempMatJuego = logoEmpresa.GetComponent<MeshRenderer>().sharedMaterial;
        //tempMatJuego.color = new Color(tempMatJuego.color.r, tempMatJuego.color.g, tempMatJuego.color.b, TransparenciaLogoJuego);
        logoEmpresa.GetComponent<MeshRenderer>().sharedMaterial.color = tempMatEmpresa.color;
        //logoJuego.GetComponent<MeshRenderer>().sharedMaterial.color = tempMatJuego.color;
    }

    // Update is called once per frame
    void Update()
    {

        diley = diley + Time.deltaTime;
        if (diley >= 2 && soloUnaVez)
        {
            entrarLogoEmpresa = true;
            soloUnaVez = false;
            //diley = 0;
        }

        if (entrarLogoEmpresa)
        {
            if (logoEmpresa != null)
            {
                logoEmpresa.GetComponent<MeshRenderer>().sharedMaterial.color = new Color(logoEmpresa.GetComponent<MeshRenderer>().sharedMaterial.color.r, logoEmpresa.GetComponent<MeshRenderer>().sharedMaterial.color.g, logoEmpresa.GetComponent<MeshRenderer>().sharedMaterial.color.b, TransparenciaLogoEmpresa);
            }
            tiempo = tiempo + Time.deltaTime;
            if (TransparenciaLogoEmpresa < 1 && sumar)
            {
                TransparenciaLogoEmpresa = TransparenciaLogoEmpresa + Time.deltaTime;
            }
            if (tiempo >= 4 && tiempo < 7f)
            {
                sumar = false;
                restar = true;
            }
            if (TransparenciaLogoEmpresa >= 0 && restar)
            {
                TransparenciaLogoEmpresa = TransparenciaLogoEmpresa - Time.deltaTime;
            }
            if (tiempo >= 5.5f)
            {
                Destroy(logoEmpresa);
                if (menuAndroid)
                {
                    SceneManager.LoadScene("MenuAndroid");
                }
                if(menuWindows)
                {
                    SceneManager.LoadScene("MenuWindows");
                }
            }
            //if (tiempo >= 5f)
            //{
            //    entrarLogoEmpresa = false;
            //    entrarLogoJuego = true;
            //    sumar = true;
            //    restar = false;
            //    tiempo = 0;
           // }
        }

        /*if (entrarLogoJuego)
        {
            if (logoJuego != null)
            {
                logoJuego.GetComponent<MeshRenderer>().material.color = new Color(logoJuego.GetComponent<MeshRenderer>().material.color.r, logoJuego.GetComponent<MeshRenderer>().material.color.g, logoJuego.GetComponent<MeshRenderer>().material.color.b, TransparenciaLogoJuego);
            }
            tiempo = tiempo + Time.deltaTime;
            if (TransparenciaLogoJuego < 1 && sumar)
            {
                TransparenciaLogoJuego = TransparenciaLogoJuego + Time.deltaTime;
            }
            if (tiempo >= 2 && tiempo < 5f)
            {
                sumar = false;
                restar = true;
            }
            if(TransparenciaLogoJuego >= 0 && restar)
            {
                TransparenciaLogoJuego = TransparenciaLogoJuego - Time.deltaTime;
            }

            if (tiempo >= 3.5f)
            {
               
                entrarLogoEmpresa = false;
                entrarLogoJuego = false;
                sumar = true;
                restar = false;
                SceneManager.LoadScene("Menu");

            }
        }*/
    }
}
