using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Defines the differnt powerups for this code in the level.
    public Healthpowerup healthPowerup;
    public Speedpowerup speedPowerup;
    public Fireratepowerup firerate;


    // Defines the differnt materals for the differnt powerups.
    public Material[] mats;
    public Material healthMat;
    public Material speedMat;
    public Material fireRateMat;

    //Defines the differnt powerups avaliable in the level.
    public enum PowerupType
    {
        powerupHealth,
        powerupSpeed,
        firerate,
    }
    public PowerupType currentPowerupType;
    public AudioClip sound;


    //When the game starts it spawns the powerup and changes the material based on the powerup loaded into level.
    public void Start()
    {
        
        currentPowerupType = (PowerupType)UnityEngine.Random.Range(0, System.Enum.GetNames(typeof(PowerupType)).Length);
        mats = GetComponent<MeshRenderer>().materials;
       
        switch (currentPowerupType)
        {
            case PowerupType.powerupHealth:
            {
                name = "Health Powerup";
                mats[0] = healthMat;
                break;
            }
            case PowerupType.powerupSpeed:
            {
                name = "Speed Powerup";
                mats[0] = speedMat;
                break;
            }
            case PowerupType.firerate:
            {
                name = "Fire Rate";
                mats[0] = fireRateMat;
                break;
            }
        }
        //gets the powerups differnt meshrenders and changes the material of it.
        GetComponent<MeshRenderer>().materials = mats;
    }

    //When a tank collides with the powerup it will activate there powerup.

    public void OnTriggerEnter(Collider other)
    {
        Controller_Powerup buffController = other.GetComponent<Controller_Powerup>();
        if (buffController != null)
        {
            switch (currentPowerupType)
            {
                //If the tank collides with Powerup Health then it will increase the health.
                case PowerupType.powerupHealth:
                {
                    buffController.add(healthPowerup);
                    break;
                }
                    //If the tank collides with Powerup Speed then it will increase the tanks speed.
                case PowerupType.powerupSpeed:
                {
                    buffController.add(speedPowerup);
                    break;
                }
                    //If the tank collides with the Powerup Firerate then it will increase the tanks firerate.
                case PowerupType.firerate:
                {
                    buffController.add(firerate);
                    break;
                }
            }
            //When the powerup is collected then a clip will play.
            if (sound != null)
            {
                AudioSource.PlayClipAtPoint(sound, transform.position, 1.0f);
            }
            //When the powerup is collected then it will destroy the object.
            Destroy(gameObject);

        }
        
    }

    //The game manager will remove the powerup that was spawned after a tank picks it up.
    public void OnDestroy()
    {
        GameManager.instance.spawnedItems.Remove(this.gameObject);
    }
}
