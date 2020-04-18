using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lifeTracker : MonoBehaviour
{
    //This states the Tank Data that is used in this script and the image that reprsents the lives of players.
    public Image[] lives;
    public TankData data;

    // When the game starts it loads the tank data and the lives.
    void Start()
    {
        lives = GetComponentsInChildren<Image>();
        data = GetComponentInParent<TankData>();
    }

    
    // Update is called once per frame
    //On update the game will didplay the amount of health the player has.
    void Update()
    {
        //If the player has three lives it wil show 3 circles.
        if (data.lives == 3)
        {
            lives[0].enabled = true;
            lives[1].enabled = true;
            lives[2].enabled = true;
        }
        //If the player has two lives it will show 2 circles.
        if (data.lives == 2)
        {
            lives[0].enabled = false;
            lives[1].enabled = true;
            lives[2].enabled = true;
        }
        //If the player has one life then it will show 1 circle.
        if (data.lives == 1)
        {
            lives[0].enabled = false;
            lives[1].enabled = false;
            lives[2].enabled = true;
        }
        //If the player has no more lives left then it will show no circles.
        if (data.lives <= 0)
        {
            lives[0].enabled = false;
            lives[1].enabled = false;
            lives[2].enabled = false;
        }
    }
}
