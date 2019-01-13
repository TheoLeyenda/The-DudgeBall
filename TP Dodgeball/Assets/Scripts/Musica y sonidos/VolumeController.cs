using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour {

    // Use this for initialization
    
    public AudioSource musicAudio;
    public AudioSource[] effectsAudio;
    public Text percentageMusic;
    public Text percentageEffects;
    [HideInInspector]
    public float volume;

    private float integerPercentageEffects;
    private float integerPercentageMusic;
    private float effectsAudioVolume = 1f;
    private float musicAudioVolume = 1f;

    void Start () {
        volume = 1;
        integerPercentageMusic = 100;
        integerPercentageEffects = 100;
        if (percentageEffects != null)
        {
            percentageEffects.text = (int)integerPercentageEffects + "%";
        }
        if (percentageMusic != null)
        {
            percentageMusic.text = (int)integerPercentageMusic + "%";
        }
        
    }
	
	// Update is called once per frame
        

    public void SetMusicAudioVolume(float vol)
    {
        musicAudio.volume = vol;
        integerPercentageMusic = musicAudioVolume + vol * 100;
        if ((int)integerPercentageMusic < 101)
        {
            percentageMusic.text = (int)integerPercentageMusic + "%";
        }
        if((int)integerPercentageMusic == 1)
        {
            percentageMusic.text = "0%";
        }
        volume = vol;
    }
    public void SetEffectAudioVolume(float vol)
    {
        for(int i = 0; i< effectsAudio.Length; i++)
        {
            if (effectsAudio[i] != null)
            {
                effectsAudio[i].volume = vol;
            }
        }
        integerPercentageEffects = effectsAudioVolume + vol * 100;
        if((int)integerPercentageEffects < 101)
        {
            percentageEffects.text = (int)integerPercentageEffects + "%";
        }
        if((int)integerPercentageEffects == 1)
        {
            percentageEffects.text = "0%";
        }
        volume = vol;

    }
    public float GetVolume()
    {
        return volume;
    }
}
