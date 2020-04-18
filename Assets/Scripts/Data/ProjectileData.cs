using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileData : MonoBehaviour
{
    // Defines the Projectile spped,damage,and how long it exsits.Also defines the noise when it hits a tank.And uses Tank Data to figuire out who shot the projectile.
    public float projectileSpeed;
    public float projectileDamage;
    public float projectileLifespan;
    public AudioClip Hit;

    public TankData shooterName;
}
