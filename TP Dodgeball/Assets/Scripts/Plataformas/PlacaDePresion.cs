using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacaDePresion : MonoBehaviour {

	// Use this for initialization
    private Animation animacion;
    public AnimationClip AnimationClip;
    public GameObject[] objectosApagar;
    public Enemigo Enemigo;
    public PuertaRejas puerta;
    public bool checkAbrirPuerta;
    public bool activarPorCubo;
    public bool activarPorJugador;
    private bool unaVez;
	void Start () {
        animacion = GetComponent<Animation>();
        if (Enemigo != null)
        {
            Enemigo.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(checkAbrirPuerta)
        {
            if (Enemigo != null)
            {
                if (Enemigo.vida <= 0)
                {
                    if(puerta != null && !unaVez)
                    {
                        puerta.AbrirPuerta();
                        unaVez = true;
                    }
                }
            }
        }
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (activarPorCubo)
        {
            if (collision.gameObject.tag == "CuboActivador")
            {
                animacion.clip = AnimationClip;
                animacion.Play();
                for (int i = 0; i < objectosApagar.Length; i++)
                {
                    objectosApagar[i].SetActive(false);
                }
                if (Enemigo != null)
                {
                    Enemigo.gameObject.SetActive(true);
                }
            }
        }
        if(activarPorJugador)
        {
            if (collision.gameObject.tag == "Player")
            {
                animacion.clip = AnimationClip;
                animacion.Play();
                for (int i = 0; i < objectosApagar.Length; i++)
                {
                    objectosApagar[i].SetActive(false);
                }
                if (Enemigo != null)
                {
                    Enemigo.gameObject.SetActive(true);
                }
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        
        animacion.transform.localScale = new Vector3(transform.localScale.x, 0.1f, transform.localScale.z);

    }

}
