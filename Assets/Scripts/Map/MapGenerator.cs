using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //defines the integers on the size of the rooms and the map.
    public int rows;
    public int cols;
    public float roomWidth = 50.0f;
    public float roomHeight = 50.0f;
    public List<GameObject> roomPrefabs;
    public Room[,] myMap;

    public int seedValue;

    //Defines the differnt types of maps.
    public enum mapType
    {
        mapOfTheDay,
        randomMap,
        seededMap
    }
    public mapType maptype;

    //When the level loads it creates the map.
    void Start()
    {
        createMap();
    }
    //Creates a map based on what type of map is selected.
    public void createMap()
    {
        switch (maptype)
        {
            //If Map Of The Day is choosen then it will create Map of The Day.
            case mapType.mapOfTheDay:
                {
                    
                    int now = getDateAsInt();
                    UnityEngine.Random.InitState(now);
                    break;
                }
                //If Random Map is chossen then it will create a Random Map.
            case mapType.randomMap:
                {
                    
                    break;
                }
                //If Seed Map is choosen then it will create the Seed Map.
            case mapType.seededMap:
                {
                    UnityEngine.Random.InitState(seedValue);
                    break;
                }
        }
        //This generates the rooms inside each map.
        myMap = new Room[cols, rows];
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                float xPos = roomWidth * j;
                float zPos = roomHeight * i;
                Vector3 newPos = new Vector3(xPos, 0.0f, zPos);

                // This creates rooms at random locations.
                GameObject newRoom = Instantiate(getRandomRoom(), newPos, Quaternion.identity);

                // This sets them as parent rooms.
                newRoom.transform.parent = this.transform;

                // This gives new room name for heirarchy.
                newRoom.name = "Room: " + j + " , " + i;

                // This sets the doors for each room.
                setDoors(newRoom, i, j);
            }
        }
        // This resets the seed.
        UnityEngine.Random.InitState(System.Environment.TickCount);
    }

    // This sets the doors and which door will be open in each room.
    void setDoors(GameObject roomToChange, int currentI, int currentJ)
    {
        Room tempRoom = roomToChange.GetComponent<Room>();
        
        if (currentI == 0)
        {
            tempRoom.northDoor.SetActive(false);
        }
        else if (currentI == rows - 1)
        {
            tempRoom.southDoor.SetActive(false);
        }
        else
        {
            tempRoom.southDoor.SetActive(false);
            tempRoom.northDoor.SetActive(false);
        }
        
        if (currentJ == 0)
        {
            tempRoom.eastDoor.SetActive(false);
        }
        else if (currentJ == cols - 1)
        {
            tempRoom.westDoor.SetActive(false);
        }
        else
        {
            tempRoom.westDoor.SetActive(false);
            tempRoom.eastDoor.SetActive(false);
        }
    }

    // This gets the Day,Month and Year and turns into an integer so it can form into the Map of The Day.
    int getDateAsInt()
    {
        string day = DateTime.Now.Day.ToString();
        string month = DateTime.Now.Month.ToString();
        string year = DateTime.Now.Year.ToString();
        string nowSeed = day + month + year;
        int currentSeed = int.Parse(nowSeed);

        return currentSeed;
    }

    // This gets a random number, then gets that element of the list to use as current room.
    public GameObject getRandomRoom()
    {
        int randomSelection = UnityEngine.Random.Range(0, roomPrefabs.Count);
        GameObject currentRoom = roomPrefabs[randomSelection];
        return currentRoom;
    }
}
