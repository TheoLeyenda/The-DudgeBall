using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class Bomb : MonoBehaviour {

    // Use this for initialization
    public AudioSource sound;
    public AudioClip explotionSound;
    public GameObject bomb;
    public GameObject explotionRange;
    public GameObject explotionEffect;
    public float explotionTime;
    private bool ActivedBomb;
    private bool ActivedDiley;
    private float disappearanceDiley = 1;
    private bool unaVez = true;
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (ActivedBomb)
        {
            explotionTime = explotionTime - Time.deltaTime;
            if (explotionTime <= 0)
            {
                explotionRange.SetActive(true);
                explotionEffect.SetActive(true);
                ActivedDiley = true;
                ActivedBomb = false;
                if (sound != null && explotionSound != null && unaVez)
                {
                    sound.PlayOneShot(explotionSound);
                    unaVez = false;
                }
            }
        }
        if(ActivedDiley)
        {
            disappearanceDiley = disappearanceDiley - Time.deltaTime;
            if(disappearanceDiley <= 0)
            {
                bomb.SetActive(false);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(Player.GetPlayer() != null)
            {
                ActivedBomb = true;
            }
        }
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
