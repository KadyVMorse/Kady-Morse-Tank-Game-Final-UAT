using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Speedpowerup : Powerup
{
    //Defines the float for the increase of speed and rotation.
    public float speedIncrease;
    public float rotationIncrease;
    
    //When the player picks up Speed Powerup it increases the movement speed and rotation speed of the tank.
    public override void OnActivated(TankData data)
    {
        data.movementSpeed += speedIncrease;
        data.rotationSpeed += rotationIncrease;
        base.OnActivated(data);
    }
    //When the Speed Powerup is deactivated it will return the speed of the tank back to normal.
    public override void OnDeactivated(TankData data)
    {
        data.movementSpeed -= speedIncrease;
        data.rotationSpeed -= rotationIncrease;
        base.OnDeactivated(data);
    }
}