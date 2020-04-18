using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scared_AI : MonoBehaviour
{
    //Defines the AI controller and Vector 3 for this code.
    Controller_AI controller;

    Vector3 lastKnownPosition;

    // Skittish AI is supposed to hit and run the player 1 shot at a time. 
    
        //Defines the differnt types of personalities of the AI.
    public enum states
    {
        chase,
        flee,
        patrol
    };
    public states currentState;

    // Use this for initialization
    //When the game starts it gets the AI controller and sets the current state to patrol.
    void Start()
    {
        controller = GetComponent<Controller_AI>();
        currentState = states.patrol;
    }

    // Update is called once per frame
    //When the game updates it states the differnt cases such as Chase,Flee,Patrol of all AI.
    void Update()
    {
        switch (currentState)
        {
            case states.chase:
                stateChase();
                break;
            case states.flee:
                stateFlee();
                break;
            case states.patrol:
                statePatrol();
                break;
        }
    }

    //When this AI is in chase state it will chase the postion of the player and when its not in chase its in patrol.
    void stateChase()
    {
        // The AI goes to the last location they heard and saw the player while avoiding obstcales if they don't currently see or here them.
        if (!controller.canSeeTarget() && !controller.hearTarget())
        {
            if (Vector3.Distance(transform.position, lastKnownPosition) >= controller.HowClose)
            {
                controller.motor.rotateTowards(lastKnownPosition - transform.position);
                controller.obstacleAvoidanceMove();
            }
            else
            {
                currentState = states.patrol;
            }
        }
        //This AI will shoot the player and then flee when they are in sight.
        else
        {
            controller.motor.rotateTowards(GameManager.instance.players[0].transform.position - transform.position);
            if (Vector3.Angle(transform.position, GameManager.instance.players[0].transform.position) < controller.skittishShootingAngle)
            {
                controller.motor.ShootMissile();
                lastKnownPosition = GameManager.instance.players[0].transform.position;
                currentState = states.flee;
            }
        }
    }

    //When this AI flees it will get as far as it can from the player and once the player is out of sight and is not heard it will go into chase state.
    void stateFlee()
    {
        if (controller.timeInFlee <= controller.timeToFlee)
        {
            // The AI runs away from the player.
            Vector3 directionToFlee = -(GameManager.instance.players[0].transform.position - transform.position);
            if (controller.canMove())
            {
                controller.obstacleAvoidanceMove();
                controller.motor.rotateTowards(directionToFlee);
            }
            //The AI avoids obstcales while fleeing and if the player is still within rnge then they will contunie to flee.
            controller.obstacleAvoidanceMove();
            controller.timeInFlee += Time.deltaTime;
        }
        else
        {
            //When the AI is no longer fleeing it will go back to its chase state.
            controller.timeInFlee = 0;
            currentState = states.chase;
        }
    }

    //When thsi AI is in Patrol it will rember the last noise and sighting of the player and go to it.If it is not patrolling it goes into flee state.
    void statePatrol()
    {
       
        controller.motor.rotate(Vector3.up * controller.data.rotationSpeed * Time.deltaTime);
       
        if (controller.canSeeTarget() || controller.hearTarget())
        {
            lastKnownPosition = GameManager.instance.players[0].transform.position;
            currentState = states.flee;
        }
    }
}

