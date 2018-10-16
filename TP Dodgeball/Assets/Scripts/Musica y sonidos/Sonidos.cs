using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonidos : MonoBehaviour {

    // Use this for initialization
    public AudioSource sonido;
    public AudioClip efectoSonido;
    private float dileyDesactivacion;
    private void OnEnable()
    {
        sonido.clip = efectoSonido;
        sonido.PlayOneShot(efectoSonido);
    }
    private void Update()
    {
        if(!sonido.isPlaying)
        {
            dileyDesactivacion = 0.1f;
        }
        dileyDesactivacion = dileyDesactivacion - Time.deltaTime;
        if(dileyDesactivacion<= 0)
        {
            gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
}
