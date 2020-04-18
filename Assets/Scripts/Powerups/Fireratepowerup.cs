using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Fireratepowerup : Powerup
{
    //Defines the float as being firerate.
    public float Firerate;
    //When the player picks up this power up the tank will have an increase in its firerates by making the cooldown shorter.
    public override void OnActivated(TankData data)
    {
        data.missileCooldownMax -= Firerate;
        base.OnActivated(data);
    }
    //When the firatepowerup is deactivated then the tank will return to having a normal firerate and cool down.
    public override void OnDeactivated(TankData data)
    {
        data.missileCooldownMax += Firerate;
        base.OnDeactivated(data);
    }
}
