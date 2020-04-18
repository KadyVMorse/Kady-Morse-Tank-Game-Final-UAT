using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_CharacterSpawner : MonoBehaviour
{
	//Defines the bool and the room where the character is going to spawn.
    bool inUse;

    public Room section;

	// Use this for initialization
	//When the game starts it will pull up the game manager on the character spawn points and where in the room they spawn.
	void Start ()
    {
        GameManager.instance.characterSpawns.Add(this.transform);
        section = GetComponentInParent<Room>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
