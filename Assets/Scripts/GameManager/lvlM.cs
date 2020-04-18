using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvlM : MonoBehaviour
{
    //Defines the camera for player one and two and the high score.
    public Camera camP1;
    public Camera camP2;
    public HighScore highScoreDisplay;

    // This gets the camera for player one and two.
    void Update()
    {
    
        // This gets camera of player 1 and player 2.If one player dies then it goes into single player mode.
        if (GameManager.instance.player1 != null)
        {
            camP1 = GameManager.instance.player1.GetComponentInChildren<Camera>();
        }
        if (GameManager.instance.player2 != null)
        {
            camP2 = GameManager.instance.player2.GetComponentInChildren<Camera>();
        }
        // If multiplayer is enabled and both players are alive set the screen to split screen.
        if (GameManager.instance.isMultiplayer == true)
        {
            if (p1Alive() == true && p2Alive() == true)
            {
                setCameraSplitscreen();
            }
            else
            {
                //If not multiplayer or one player died then set the camera to full scrren with the High Score.
                setCameraFullscreen(PlayerCamera());
                highScoreDisplay = FindObjectOfType<HighScore>();
                highScoreDisplay.setHighScoreSinglePlayer();
            }
        }
        //Sets the camera to player one.
        else
        {
            setCameraFullscreen(camP1);
        }
    }
    //If Player one is alive set the camera for that player until they die.

    bool p1Alive()
    {
        if (GameManager.instance.p1Lives >= 0)
        {
            return true;
        }
        return false;
    }

    //if player two is alive set the camera for them until they die.
    bool p2Alive()
    {
        if (GameManager.instance.p2Lives >= 0)
        {
            return true;
        }
        return false;
    }

    //Sets the players camera for both players.
    Camera PlayerCamera()
    {
        if (p1Alive() == true)
        {
            return camP1;
        }
        if (p2Alive() == true)
        {
            return camP2;
        }
        return null;
    }

    // This sets up splitscreen by setting the cameras location.
    void setCameraSplitscreen()
    {
        if (camP1 != null)
        {
            camP1.rect = new Rect(0, 0.5f, 1, 0.5f);
        }
        if (camP2 != null)
        {
            camP2.rect = new Rect(0, 0, 1, 0.5f);
        }
    }

    // This sets up full screen.
    void setCameraFullscreen(Camera cam)
    {
        cam.rect = new Rect(0, 0, 1, 1);
    }
}
