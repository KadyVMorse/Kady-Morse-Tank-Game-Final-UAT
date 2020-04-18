using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    // Defines the Audio Mixer in this code.
    public AudioMixer Mixer;

    // On start it will set the mixer to max on Music Volume and SFX Volume.
    private void Start()
    {
        Mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("musicVolume", 0));
        Mixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("sfxVolume", 0));
    }

    //This sets the exposed Music Volume from the Mixer.
    public void setMusicVolume(float volume)
    {
        Mixer.SetFloat("MusicVolume", volume);
    }

    //This sets the exposed SFX Volume from the mixer.
    public void setSFXVolume(float volume)
    {
        Mixer.SetFloat("SFXVolume", volume);
    }
}
