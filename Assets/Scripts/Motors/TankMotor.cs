using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMotor : MonoBehaviour
{
    //Defines the Transform and the bullet and missile prefab.It also gets the Tank Data and uses it in this code while hiding it inn inspector.
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject missilePrefab;

    [HideInInspector] public TankData data;

    //When the level start then it will load in the tank data.
    private void Start()
    {
        data = GetComponent<TankData>();
    }
    //When update begins it will reduce the cooldowns of the projectiles over time and increae the noise.
    private void Update()
    {
        if (data.missileCooldownCurrent >= 0)
        {
            data.missileCooldownCurrent -= Time.deltaTime;
        }
        if (data.bulletCooldownCurrent >= 0)
        {
            data.bulletCooldownCurrent -= Time.deltaTime;
        }
        if (data.noiseLevel >= 0)
        {
            data.noiseLevel -= data.noiseLevelReducPerSec;
        }
    }

    // This lets the player move the tank.
    public void move(Vector3 movement)
    {
        transform.Translate(movement);
        data.noiseLevel = data.moveNoiseLevel;
    }

    // This lets the player rotate the tank.
    public void rotate(Vector3 rotation)
    {
        transform.Rotate(rotation);
        data.noiseLevel = data.rotateNoiseLevel;
    }

    // This checks the cooldown and if its done cooling down then its no longer cooling down.
    bool checkCooldown(float cooldown)
    {
        if (cooldown >= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // This shoots the bullets for the tanks.
    public void ShootBullet()
    {
        // This checks if there is a coold down for the bullet.
        if (checkCooldown(data.bulletCooldownCurrent))
        {
            // This sets the cooldown rate to shooting rate for bullet.
            data.bulletCooldownCurrent = data.bulletCooldownMax;
            //This creates the bullet so player cant shoot.
            var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<ProjectileData>().shooterName = this.gameObject.GetComponent<TankData>();
            // This sets the noise level and plays a noise when the bullet is shot.
            data.noiseLevel = data.bulletNoiseLevel;
            if (data.shellFire != null)
            {
                data.soundSource.clip = data.shellFire;
                data.soundSource.Play();
            }
        }
    }

     // This shoots the missile for the tanks.
    public void ShootMissile()
    {
        // This checks if there is a coold down for the missile.
        if (checkCooldown(data.missileCooldownCurrent))
        {
            // This sets the cooldown rate to shooting rate for missile.
            data.missileCooldownCurrent = data.missileCooldownMax;
            //This creates the missile so player cant shoot.
            var missile = Instantiate(missilePrefab, firePoint.position, firePoint.rotation);
            missile.GetComponent<ProjectileData>().shooterName = this.gameObject.GetComponent<TankData>();
        }
        // This sets the noise level and plays a noise when the missile is shot.
        if (data.cannonFire != null)
        {
            data.soundSource.clip = data.cannonFire;
            data.soundSource.Play();
        }
        data.noiseLevel = data.missileNoiseLevel;
    }

    // This allows the player to rotate in any direction they want to.
    public void rotateTowards(Vector3 targetDirection)
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, data.rotationSpeed * Time.deltaTime);
        data.noiseLevel = data.rotateNoiseLevel;
    }
}
