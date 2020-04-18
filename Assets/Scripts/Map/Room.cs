using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //This state the differnt dorrs to each rooms,the powerup spawner,character spawner,and teh lsit of waypoints.
    public GameObject northDoor;
    public GameObject southDoor;
    public GameObject eastDoor;
    public GameObject westDoor;

    public GameObject powerupSpawner;
    public GameObject characterSpawner;

    public List<Transform> waypoints;
}
