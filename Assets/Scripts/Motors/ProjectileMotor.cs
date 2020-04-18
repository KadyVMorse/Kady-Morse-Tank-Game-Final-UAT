using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotor : MonoBehaviour
{
    //Defines the Projecticle Data and Rigidbody for this code and hides it in inspector.
    [HideInInspector] public ProjectileData data;
    [HideInInspector] public Rigidbody rb;

    public GameObject explosionEffect;
    public float deleteDelay;

    // When the game starts it will load the projectile data and the rigidbody.
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        data = GetComponent<ProjectileData>();

        // Destroy this projectile after a amount of time 
        Destroy(this.gameObject, data.projectileLifespan);
        pushForward();
    }

    //When projectile hits target then they will lose health.
    private void OnTriggerEnter(Collider other)
    {
        // This checks if the player or AI tank got hit.
        if (GameManager.instance.players.Contains(other.gameObject.GetComponent<TankData>()) ||
            GameManager.instance.aiUnits.Contains(other.gameObject.GetComponent<TankData>()))
        {
            //If one of them got hit then it will reduce the health of the tank.
            TankHealth taregtHit = other.gameObject.GetComponent<TankHealth>();
            taregtHit.reduceCurrentHealth(data.projectileDamage, data.shooterName);
        }
        //This will play a sound when a tank is hit.
        if (data.Hit != null)
        {
            AudioSource.PlayClipAtPoint(data.Hit, transform.position);
        }
        // This destroys the projectile afetr it has been fired.
        Destroy(this.gameObject);
    }


    // This propels the projectile forward after it has been shot.
    public void pushForward()
    {
        rb.AddForce(transform.forward * data.projectileSpeed);
    }
}
