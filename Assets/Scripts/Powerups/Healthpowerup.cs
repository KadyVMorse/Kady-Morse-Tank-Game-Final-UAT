using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Healthpowerup : Powerup
{
    //Defines the health to increase from the health powerup.
    public float maxHealthIncrease;
    public float currentHealthIncrease;

    //When a tank picks up the Health Powerup then it will increase its health till makx health over time.
    public override void OnActivated(TankData data)
    {
        data.healthMax += maxHealthIncrease;
        data.healthCurrent += currentHealthIncrease;
        base.OnActivated(data);
    }
    //When the Health Powerup is deactivated it will return the health to the amount it healed.
    public override void OnDeactivated(TankData data)
    {
        data.healthMax -= maxHealthIncrease;
        data.healthCurrent -= currentHealthIncrease;
        base.OnDeactivated(data);
    }
}
