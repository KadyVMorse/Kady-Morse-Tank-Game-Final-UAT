using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_AI : MonoBehaviour
{
    //Defines the tank data adn tank motor that is used in this code but hides it in the inspector.It also list the waypoints and the current waypoint.
    [HideInInspector] public TankData data;
    [HideInInspector] public TankMotor motor;
    public List<Transform> waypoints;
    public int currentWaypoint = 0;
//Deines floats about fleeing,distance of every AI,skittish shooting for the Scared Ai and Mesh Render for  each AI.
    public float timeInFlee;
    public float timeToFlee;

    public float HowClose;
    public float distanceToMaintain;
    public float distanceToMaintainHG;
    public float distanceToMaintainAggro;
    public float distanceToMaintainCreeper;
    public float distanceToMaintainScared;

    public float skittishShootingAngle;

    public MeshRenderer topColor;
    public MeshRenderer leftColor;
    public MeshRenderer rightColor;
    public MeshRenderer bottomColor;

    //States the differnt personalities while hidingg it in inspector.
    [HideInInspector]public enum personalities
    {
        creeper,
        scared,
        holdGround,
        aggro
    };
    public personalities personality;

	// Use this for initialization
    //When the game starts its gets the Data,Motor and Game Manager of the AI.
	void Start ()
    {
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();
        GameManager.instance.aiUnits.Add(this.data);

       //On start it will randomly chose a personality to spawn the AI.

       personality = (personalities)Random.Range(0, System.Enum.GetNames(typeof(personalities)).Length);
      
        //Defines the differnt AI and changes the color of the tank depending on on the personality it is.
       switch (personality)
       {
            case personalities.aggro:
                gameObject.AddComponent<Aggro_AI>();
                distanceToMaintain = distanceToMaintainAggro;
                topColor.materials[0].color = Color.blue;
                leftColor.materials[0].color = Color.blue;
                rightColor.materials[0].color = Color.blue;
                bottomColor.materials[0].color = Color.blue;
                break;
            case personalities.scared:
                gameObject.AddComponent<Scared_AI>();
                distanceToMaintain = distanceToMaintainScared;
                topColor.materials[0].color = Color.yellow;
                leftColor.materials[0].color = Color.yellow;
                rightColor.materials[0].color = Color.yellow;
                bottomColor.materials[0].color = Color.yellow;
                break;
            case personalities.creeper:
                gameObject.AddComponent<Creeper_AI>();
                distanceToMaintain = distanceToMaintainCreeper;
                topColor.materials[0].color = Color.magenta;
                leftColor.materials[0].color = Color.magenta;
                rightColor.materials[0].color = Color.magenta;
                bottomColor.materials[0].color = Color.magenta;
                break;
            case personalities.holdGround:
                gameObject.AddComponent<HoldsGround_AI>();
                distanceToMaintain = distanceToMaintainHG;
                topColor.materials[0].color = Color.gray;
                leftColor.materials[0].color = Color.grey;
                rightColor.materials[0].color = Color.grey;
                bottomColor.materials[0].color = Color.grey;
                break;
       }
    }
	
    //When the tank dies it will play a noise and be romeved from the Game Manager.
    private void OnDestroy()
    {
        if (data.died != null)
        {
            AudioSource.PlayClipAtPoint(data.died, transform.position);
        }
    
        GameManager.instance.aiUnits.Remove(this.data);
        GameManager.instance.numAICurrent--;
    }

    //When the AI can move they will send a raycast to detrmine if they can move.
    public bool canMove()
    {
        //hits nothing return true
        if (Physics.Raycast(transform.position, transform.forward, data.wallDetectDistance))
        {
            return false;
        }
        // else return false
        return true;
    }

    //The AI sense the floor based on the raycast ofit.
    public bool floorExists()
    {
       
        if (Physics.Raycast(transform.position + (transform.forward * data.wallDetectDistance), -transform.up, data.wallDetectDistance))
        {
            return true;
        }
      
        return false;
    }
    //The AI can see the player by creatinga vector and angle to target.If they don't see player then they don't see them.
    public bool canSeeTarget()
    {
        
        Vector3 vectorToTarget = (GameManager.instance.players[0].transform.position - transform.position);
      
        float Angle = Vector3.Angle(vectorToTarget, transform.forward);

       
        if (Angle > data.fieldOfView)
        {
            return false;
        }

       
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, vectorToTarget, out hitInfo, data.viewDistance);
        if (hitInfo.collider == null)
        {
            return false;
        }

        
        Collider targetCollider = GameManager.instance.players[0].GetComponent<Collider>();
        if (targetCollider != hitInfo.collider)
        {
            return false;
        }

        
        return true;
    }

    // When they hear the player they will create a vector 3 to player and move towards that postion.But if they don't hear anying then they will return to normal and contunie looking.
    public bool hearTarget()
    {
        float distance = Vector3.Distance(transform.position, GameManager.instance.players[0].transform.position);
        if (distance >= (GameManager.instance.players[0].noiseLevel + data.hearingDistance))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //When the AI sense nothing in the way and a floor then they will move forward.But if they sense neither of those two things then they will not move forward and then rotate till they can move.
    public void obstacleAvoidanceMove()
    {
        // if nothing is in the way move forward
        if (canMove())
        {
            // if floor is detected move forward
            if (floorExists())
            {
                motor.move(Vector3.forward * data.movementSpeed * Time.deltaTime);
            }
            else
            {
                motor.rotate(Vector3.up * data.rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            motor.rotate(Vector3.up * data.rotationSpeed * Time.deltaTime);
        }
    }
    
    //This sets the waypoints for the AI and when they reach each one it will minus one.Onnce they go through all them then they will restart.If they don't reach a waypoint then they will contunie.
    public void getNextWaypoint()
    {
        int maxWaypoints = waypoints.Count - 1;
        if (currentWaypoint < maxWaypoints)
        {
            currentWaypoint++;
        }
        else
        {
            currentWaypoint = 0;
        }
    }
}