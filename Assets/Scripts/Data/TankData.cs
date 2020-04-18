using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    //Defines the intger of the lives and score.
    public int score;
    public int lives;

    // Defines the name of the players.
    public string myName = "No Name";

    // Defines the speed,health,and noise of the tank.
    public float movementSpeed;
    public float backwardMovementSpeed;
    public float rotationSpeed;
    public float healthCurrent;
    public float healthMax;

    public float noiseLevelReducPerSec;
    public float rotateNoiseLevel;
    public float moveNoiseLevel;

    // Defines the bullets noise and cooldown for them.
    public float bulletCooldownMax;
    public float bulletCooldownCurrent;
    public float bulletNoiseLevel;


    // Defines the missile noise ans the cooldown for them.
    public float missileCooldownMax;
    public float missileCooldownCurrent;
    public float missileNoiseLevel;

    // Defines the variables for sight and hearing.
    public float viewDistance;
    public float fieldOfView;
    public float hearingDistance;
    public float wallDetectDistance;
    public float noiseLevel;

    //Defines the varaibles for the differnt sounds.
    public AudioSource soundSource;
    public AudioClip shellFire;
    public AudioClip cannonFire;
    public AudioClip died;
}
