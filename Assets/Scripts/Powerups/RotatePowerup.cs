﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePowerup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	//On update the powerup box will rotate in the level.
	void Update ()
    {
        transform.Rotate(new Vector3(Time.deltaTime * 50, 0, 0));
	}
}
