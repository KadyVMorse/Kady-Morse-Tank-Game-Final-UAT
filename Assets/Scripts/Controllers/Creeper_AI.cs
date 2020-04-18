using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creeper_AI : MonoBehaviour
{
    //Defines the AI controller for this code and defines a float for the last health known.
    Controller_AI controller;

    public float healthLastKnown;

    //Defines the differnt states for this AI.
    public enum states
    {
        chase,
        flee,
        patrol
    };
    public states currentState;

    // Use this for initialization
    //When the game starts it gets the AI controller,sets the current state to patrol and gets the last known health.
    void Start()
    {
        controller = GetComponent<Controller_AI>();
        currentState = states.patrol;
        healthLastKnown = controller.data.healthCurrent;
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

    //When this Ai is in the chase state it will get the players last location and try to sneal up on them.Once it is behind the player it will shoot the player.Once tthe player hits the AI tank it will flee.
    void stateChase()
    {
        //Will go to the last location of the player and try to sneak up on them and avoids the obstcales.
        Vector3 targetPosition = (-(controller.distanceToMaintain * GameManager.instance.players[0].transform.forward));
        if (Vector3.Distance(transform.position, GameManager.instance.players[0].transform.position) <= controller.distanceToMaintain)
        {
            controller.obstacleAvoidanceMove();
            controller.motor.rotateTowards(targetPosition);
        }
        // If they are behind the player they will shoot the player.
        else
        {
            controller.motor.rotateTowards(GameManager.instance.players[0].transform.position - transform.position);
            controller.motor.ShootMissile();
        }
        // If the current health is less than the last know health the AI will flee.
        if (controller.data.healthCurrent < healthLastKnown)
        {
            healthLastKnown = controller.data.healthCurrent;
            currentState = states.flee;
        }
        // If they don't hear the target and see the target then they will patrol.
        if (!controller.hearTarget() && !controller.canSeeTarget())
        {
            currentState = states.patrol;
        }

    }

    //When the AI is in the flee state it will run away from the player avoiding obstcales.If not in flee state it will be in chase state.
    void stateFlee()
    {
        if (controller.timeInFlee <= controller.timeToFlee)
        {
            // AI will flee from the player and avoid obsctales while its fleeing.
            Vector3 directionToFlee = -(GameManager.instance.players[0].transform.position - transform.position);
            if (controller.canMove())
            {
                controller.obstacleAvoidanceMove();
                controller.motor.rotateTowards(directionToFlee);
            }
            controller.obstacleAvoidanceMove();
            // If the player is stil nerby they will contunie to flee.
            controller.timeInFlee += Time.deltaTime;
        }
        //If they don't see or hear the player then tehy will go into patrol state.
        else
        {
            controller.timeInFlee = 0;
            currentState = states.patrol;
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
        if (controller.canSeeTarget() || controller.hearTarget())
        {
           
            currentState = states.chase;
        }
    }
}


