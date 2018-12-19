using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour {

    // Use this for initialization
    public AudioSource sound;
    public AudioClip soundEffect;
    private float dileyDisable;
    private void OnEnable()
    {
        sound.clip = soundEffect;
        sound.PlayOneShot(soundEffect);
    }
    private void Update()
    {
        if(!sound.isPlaying)
        {
            dileyDisable = 0.1f;
        }
        dileyDisable = dileyDisable - Time.deltaTime;
        if(dileyDisable<= 0)
        {
            gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
}
