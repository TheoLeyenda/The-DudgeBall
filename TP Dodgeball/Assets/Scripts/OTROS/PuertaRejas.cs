using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaRejas : MonoBehaviour {

    // Use this for initialization
    private Animation animacion;
    public AnimationClip animationClip;
    private bool checkDestruirme;
    private bool activarUnaVez;
    public GameObject[] objetosActivar;
    void Start ()
    {
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
		if(checkDestruirme)
        {
            if(!animacion.isPlaying)
            {
                gameObject.SetActive(false);
            }
        }
	}
    public void AbrirPuerta()
    {
        animacion.clip = animationClip;        
        animacion.Play();
        checkDestruirme = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "PararAnimacion")
        {
            Debug.Log("TREMENDA COLICION");
            animacion.Stop();
        }
    }
}
