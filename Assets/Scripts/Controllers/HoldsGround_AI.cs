using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldsGround_AI : MonoBehaviour
{
    //Defines the AI controller for this code.
    Controller_AI controller;

    //Defines the differnt states for this AI.
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

    //When AI is in chase state it will hold its ground and shoot at player and then will go into flee state at 50% health and then resume patrolling.
    void stateChase()
    {
        // The AI will hold its ground and shoot at the player by rotating towards the player.
        Vector3 targetLocation = GameManager.instance.players[0].transform.position - transform.position;
        controller.motor.rotateTowards(targetLocation);
        // It will keep firing even during its cooldown.
        controller.motor.ShootMissile();
        //When the health is at 50% it will stop holding its groud and go into its flee state.
        if (controller.data.healthCurrent <= (controller.data.healthMax / 2)) 
        {
            currentState = states.flee;
        }
        //Once it doesn't hear the player or see them it will resume patrolling.
        if (!controller.hearTarget() && !controller.canSeeTarget())
        {
            currentState = states.patrol;
        }
    }

    //When the AI is in the flee state it will run away from the player avoiding obstcales.If not in flee state it will be in chase state.
    void stateFlee()
    {
        float distanceFromTarget = Vector3.Distance(transform.position, GameManager.instance.players[0].transform.position);
        if (controller.distanceToMaintain >= distanceFromTarget)
        {
            // AI will flee from the player and avoid obsctales while its fleeing.
            Vector3 directionToFlee = -(GameManager.instance.players[0].transform.position - transform.position);
            if (controller.canMove())
            {
                controller.obstacleAvoidanceMove();
                controller.motor.rotateTowards(directionToFlee);
            }
            controller.obstacleAvoidanceMove();
       
        }
        else
        {
            //If not in flee sate it will go back to chase state.
            currentState = states.chase;
        }
    }

    //When the AI is in patrol state they will go in between waypoints until they see and hear the player.If they are not in thsi state they will be chaseing the player.
    void statePatrol()
    {
        // The AI will go to the differnt waypoints while in patrol.It will avoid obstcales while it goes through them.
        Vector3 targetPosition = new Vector3(controller.waypoints[controller.currentWaypoint].position.x, transform.position.y, controller.waypoints[controller.currentWaypoint].position.z);
        Vector3 dirToWaypoint = targetPosition - transform.position;
        if (controller.canMove())
        {
            controller.obstacleAvoidanceMove();
            controller.motor.rotateTowards(dirToWaypoint);
        }
        controller.obstacleAvoidanceMove();

        //Once they get to one way point they will contunie to the next one.
        if (Vector3.Distance(transform.position, targetPosition) <= controller.HowClose)
        {
            controller.getNextWaypoint();
        }
        //If the AI hears or see the player then they will go into their chase state.
        if (controller.hearTarget() || controller.canSeeTarget())
        {
            currentState = states.chase;
        }
    }
}
