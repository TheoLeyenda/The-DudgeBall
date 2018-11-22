using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaRejas : MonoBehaviour {

    // Use this for initialization
    private Animation animacion;
    public AnimationClip animationClip;
    public float velocidad;
    private bool checkDestruirme;
    private bool activarUnaVez;
    public float tiempoMov;
    private bool cerrarPuerta;
    private bool abrirPuerta;
    private float y;
    private float auxTiempoMov;
    public GameObject[] objetosActivar;
    void Start ()
    {
        y = transform.position.y;
        auxTiempoMov = tiempoMov;
        animacion = GetComponent<Animation>();
        animacion.clip = animationClip;
    }

    // Update is called once per frame
    private void OnDisable()
    {
        if (!activarUnaVez)
        {
            for (int i = 0; i < objetosActivar.Length; i++)
            {
                if (objetosActivar[i] != null)
                {
                    objetosActivar[i].SetActive(true);
                }
            }
            activarUnaVez = true;
        }
    }
    void Update ()
    {
        if(cerrarPuerta)
        {
            CerrarPuerta();
        }
        if(abrirPuerta)
        {
            AbrirPuertaSinAnimacion();
        }
		if(checkDestruirme)
        {
            if(!animacion.isPlaying)
            {
                gameObject.SetActive(false);
            }
        }
	}
    public void SetCerrarPuerta(bool _cerrar)
    {
        cerrarPuerta = _cerrar;
    }
    public void SetAbrirPuerta(bool _abrir)
    {
        abrirPuerta = _abrir;
    }
    public void AbrirPuerta()
    {
        animacion.clip = animationClip;        
        animacion.Play();
        checkDestruirme = true;
    }
    public void AbrirPuertaSinAnimacion()
    {
        if (tiempoMov > 0)
        {
            tiempoMov = tiempoMov - Time.deltaTime;
            y = y - Time.deltaTime * velocidad;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        if (tiempoMov <= 0)
        {
            tiempoMov = auxTiempoMov;
            abrirPuerta = false;
        }
    }
    public void CerrarPuerta()
    {
        if(tiempoMov > 0)
        {
            tiempoMov = tiempoMov - Time.deltaTime;
            y = y + Time.deltaTime * velocidad;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        if(tiempoMov <= 0)
        {
            tiempoMov = auxTiempoMov;
            cerrarPuerta = false;
        }
    }
}
