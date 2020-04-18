using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
   //Defines the Audio Source in this code.
    public AudioSource soundSource;
    public AudioClip pressed;

    // When the game starts it will load the Audio Source.
    private void Start()
    {
        soundSource = GetComponentInParent<AudioSource>();
    }
   
    // When the sound is triggerd then it will play that clip of music.
    public void playPressed()
    {
        soundSource.clip = pressed;
        if (soundSource.isPlaying == true)
        {
            return;
        }
        else
        {
            soundSource.Play();
        }
    }
}
