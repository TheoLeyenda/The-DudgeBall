using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaRejas : MonoBehaviour {

    // Use this for initialization
    private Animation animacion;
    public AnimationClip animationClip;
    void Start ()
    {
        animacion = GetComponent<Animation>();
        animacion.clip = animationClip;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void AbrirPuerta()
    {
        animacion.clip = animationClip;        
        animacion.Play();
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
