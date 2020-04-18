using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    //Defines the prefabs of the pick ups and the spawned pickups.It also defines the time they are spwaned and last.
    public GameObject pickupPrefab;
    public GameObject spawnedPickup;

    public Vector3 offset;
    public float spawnTimeMax;
    public float spawnTimeCurrent;
    private Transform tf;

	// Use this for initialization
    //When the game starts it gets the transform.
	void Start ()
    {
        tf = GetComponent<Transform>();
	}

    //When the powerup is not picked up it will stay at postion.But once picked up then a new powerup will spawn at that point.The game manager then adds that powerup to its spawned pickups.
    public void respawnPickups()
    {
        if (spawnedPickup == null)
        {
            if (spawnTimeCurrent >= 0)
            {
                spawnTimeCurrent -= Time.deltaTime;
            }
        }
        if (spawnTimeCurrent <= 0)
        {
            spawnTimeCurrent = spawnTimeMax;
            spawnedPickup = Instantiate(pickupPrefab, tf.position + offset, pickupPrefab.transform.rotation);
            spawnedPickup.transform.SetParent(GameManager.instance.pickupsHolder);
            GameManager.instance.spawnedItems.Add(spawnedPickup);
        }
    }
	
	// Update is called once per frame
    //On update it spawns the pickups in the level again.
	void Update ()
    {
        respawnPickups();
    }
}
