using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggro_AI : MonoBehaviour
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
    //When the game starts it gets the AI controller and sets the current state to patrol .
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

    //When this AI is in the state chase it will follow the player and once it is range it will contunie to shoot them and follow them while avoiding obstcales.But once this AI reaches 50% health it will start to flle until it can go into patrol state.
    void stateChase()
    {
        // If the player is nerby they will begin to chase the player and avoid obstcales.
        Vector3 targetLocation = GameManager.instance.players[0].transform.position - transform.position;

        if (Vector3.Distance(GameManager.instance.players[0].transform.position, transform.position) >= controller.distanceToMaintain)
        {
            if (controller.canMove())
            {
                controller.obstacleAvoidanceMove();
                controller.motor.rotateTowards(targetLocation);
            }
            controller.obstacleAvoidanceMove();
            controller.motor.ShootMissile();
        }
        //If they are in range they will shoot the player.
        else
        {
            controller.motor.rotateTowards(targetLocation);
            controller.motor.ShootMissile();
        }
        //If the AI's health gets its health down to 50% it will go into its flee state.
        if (controller.data.healthCurrent <= (controller.data.healthMax / 2)) 
        {
            currentState = states.flee;
        }
        //If the AI doesn't hear or see the player then it will go into its patrol state.
        if (!controller.hearTarget() && !controller.canSeeTarget())
        {
            currentState = states.patrol;
        }
    }

    //When the AI is in the flee state it will run away from the player avoiding obstcales.If not in flee state it will be in patrol state.
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
            //AI will contuine to flee if they sense the player.
            controller.timeInFlee += Time.deltaTime;
        }
        else
        {
            //If AI is no longer fleeing then it will go into patrol state.
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


