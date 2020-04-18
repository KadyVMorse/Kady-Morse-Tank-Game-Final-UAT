using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : MonoBehaviour
{
    //Defines the Tank Data for this code but it is hidden in the inspector.
    [HideInInspector] public TankData data;

	// Use this for initialization
    //On start it loads the Tank Data.
	void Start ()
    {
        data = GetComponent<TankData>();
	}
    // When tank takes damage it reduces there health.
    public void reduceCurrentHealth(float damage, TankData attacker)
    {
        data.healthCurrent -= damage;
        checkDeath(attacker);
    }

    // This checks if the tank has died.
    public void checkDeath(TankData Dmg)
    {
        if (data.healthCurrent <= 0)
        {
            Dmg.score += GameManager.instance.scorePerKill;
            Destroy(this.gameObject);
        }
    }

    // This resets the current health evry time a tank dies.
    public void resetHealth()
    {
        data.healthCurrent = data.healthMax;
    }

}
