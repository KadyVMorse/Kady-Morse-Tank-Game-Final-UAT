using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Player : MonoBehaviour
{
    //Defines the tank data and tank motor in this code but hied it in inspector.
    [HideInInspector] private TankData data;
    [HideInInspector] private TankMotor motor;

    //States teh differnt controller types for the players.
    public enum controlType
    {
        wasd,
        arrows,
    }
    public controlType selectedController;

    //When the game starts it gets the tank data and the motor.It will add the player to the game manager.
    void Start()
    {
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();

        GameManager.instance.players.Add(this.data);

    }
    //On every update it will assign the controls to the players depending on which player they are.
    void Update()
    {
        switch (selectedController)
        {
            case controlType.wasd:
                wasdControls();
                break;
            case controlType.arrows:
                arrowsControls();
                break;
        }
    }
    //Defines the controls using WASD.
    private void wasdControls()
    {
        if (Input.GetButton("Fire1"))
        {
            //When the player puts in the input for fire 1 then it will shoot a bullet.
            motor.ShootBullet();
        }
        if (Input.GetButton("Fire2"))
        {
            // When the player puts in the input for fire 2 then it will shoot a missile.
            motor.ShootMissile();
        }
        if (Input.GetKey(KeyCode.W))
        {
            //When the player presses W then the tank will move forward.
            
            Vector3 movementVector = (Vector3.forward * data.movementSpeed * Time.deltaTime);
            motor.move(movementVector);
        }
        if (Input.GetKey(KeyCode.S))
        {
            // When the player presses S then the tank will move backward.
            Vector3 movementVector = (Vector3.forward * data.movementSpeed * Time.deltaTime);
            motor.move(-movementVector);
        }
        if (Input.GetKey(KeyCode.D))
        {
            // When the player presses D then the tank will move right.
            Vector3 vectorRotation = Vector3.up * data.rotationSpeed * Time.deltaTime;
            motor.rotate(vectorRotation);
        }
        if (Input.GetKey(KeyCode.A))
        {
            // When the player presses A then the tank will move left.
            Vector3 vectorRotation = Vector3.up * data.rotationSpeed * Time.deltaTime;
            motor.rotate(-vectorRotation);
        }
    }

    //Defines the controls using the arrow keys.
    private void arrowsControls()
    {
        if (Input.GetButton("Fire1_P2"))
        {
            // When player two presses the button for fire 1 then it will shoot a bullet.
            motor.ShootBullet();
        }
        if (Input.GetButton("Fire2_P2"))
        {
            //When player two presses the button for fire 2 then it will shoot a missile.
            motor.ShootMissile();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // When the player presses the up arrow then the tank will move forward.
            Vector3 movementVector = (Vector3.forward * data.movementSpeed * Time.deltaTime);
            motor.move(movementVector);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            // When the player presses the down arrow then the tank will move backwards.
            Vector3 movementVector = (Vector3.forward * data.movementSpeed * Time.deltaTime);
            motor.move(-movementVector);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // When the player presses the right arrow then the tank will move right.
            Vector3 vectorRotation = Vector3.up * data.rotationSpeed * Time.deltaTime;
            motor.rotate(vectorRotation);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // When the player presses the left arrow then teh tank will move left.
            Vector3 vectorRotation = Vector3.up * data.rotationSpeed * Time.deltaTime;
            motor.rotate(-vectorRotation);
        }
    }


    //When the player dies it will play the died clip and then removes there data from the game manager.
    private void OnDestroy()
    {
        if (data.died != null)
        {
            AudioSource.PlayClipAtPoint(data.died, transform.position);
        }
      
        GameManager.instance.players.Remove(this.data);
    }
}