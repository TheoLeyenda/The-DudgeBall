using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class SplashScreenController : MonoBehaviour
{

    // Use this for initialization
    public GameObject companyLogo;
    public GameObject gameLogo;
    public bool menuWindows;
    public bool menuAndroid;
    private float transparencyCompanyLogo;
    private float transparencyGameLogo;
    private bool getInCompanyLogo;
    private bool getInGameLogo;
    private bool once;
    private bool add;
    private bool substract;
    private float time;
    private float diley;
    void Start()
    {
        transparencyCompanyLogo = 0;
        transparencyGameLogo = 0;
        getInCompanyLogo = false;
        getInGameLogo = false;
        add = true;
        substract = false;
        once = true;
        time = 0;
        diley = 0;

        Material tempMatEmpresa = companyLogo.GetComponent<MeshRenderer>().sharedMaterial;
        tempMatEmpresa.color = new Color(tempMatEmpresa.color.r, tempMatEmpresa.color.g, tempMatEmpresa.color.b, transparencyCompanyLogo);
        //Material tempMatJuego = logoEmpresa.GetComponent<MeshRenderer>().sharedMaterial;
        //tempMatJuego.color = new Color(tempMatJuego.color.r, tempMatJuego.color.g, tempMatJuego.color.b, TransparenciaLogoJuego);
        companyLogo.GetComponent<MeshRenderer>().sharedMaterial.color = tempMatEmpresa.color;
        //logoJuego.GetComponent<MeshRenderer>().sharedMaterial.color = tempMatJuego.color;
    }

    // Update is called once per frame
    void Update()
    {

        diley = diley + Time.deltaTime;
        if (diley >= 2 && once)
        {
            getInCompanyLogo = true;
            once = false;
            //diley = 0;
        }

        if (getInCompanyLogo)
        {
            if (companyLogo != null)
            {
                companyLogo.GetComponent<MeshRenderer>().sharedMaterial.color = new Color(companyLogo.GetComponent<MeshRenderer>().sharedMaterial.color.r, companyLogo.GetComponent<MeshRenderer>().sharedMaterial.color.g, companyLogo.GetComponent<MeshRenderer>().sharedMaterial.color.b, transparencyCompanyLogo);
            }
            time = time + Time.deltaTime;
            if (transparencyCompanyLogo < 1 && add)
            {
                transparencyCompanyLogo = transparencyCompanyLogo + Time.deltaTime;
            }
            if (time >= 4 && time < 7f)
            {
                add = false;
                substract = true;
            }
            if (transparencyCompanyLogo >= 0 && substract)
            {
                transparencyCompanyLogo = transparencyCompanyLogo - Time.deltaTime;
            }
            if (time >= 5.5f)
            {
                Destroy(companyLogo);
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
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
