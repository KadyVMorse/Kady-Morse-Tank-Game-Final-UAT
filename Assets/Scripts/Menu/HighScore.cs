using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    //Defines the vectors of the camera based on player and the loaction of the highscore based on players.
    public RectTransform highScoreLoc;
    public Text highScoreNum;
    public Vector3 singlePlayerPos = new Vector3(-767, 390, 0);
    public Vector3 multiPlayerPos = new Vector3(0, 0, 0);

    // Use this for initialization
    //When the game starts it sets the location of the high score based on players.
    void Start()
    {
        // If Multiplayer is selected then it will load a high score for both player at a certain postion on the screen.
        highScoreLoc = GetComponent<RectTransform>();
        if (GameManager.instance.isMultiplayer == true)
        {
            setHighScoreMultiplayer();
        }
        //If it is not multiplayer then it will load the High Score postion in single player mode on the screen.
        else
        {
            setHighScoreSinglePlayer();
        }
        //The High Score will be loaded by the Game Manager.
        highScoreNum.text = GameManager.instance.highScore.ToString();
    }

    //The postion of a single player high score is anchored to that postion.
    public void setHighScoreSinglePlayer()
    {
        highScoreLoc.anchoredPosition = singlePlayerPos;
    }

    //The postion of multiplayer high score is anchored to the postion of two player screens.
    public void setHighScoreMultiplayer()
    {
        highScoreLoc.anchoredPosition = multiPlayerPos;
    }
  
    // Update is called once per frame
    //On update it will display the score based on the amount of players.
    void Update()
    {
        if (GameManager.instance.highScore > int.Parse(highScoreNum.text.ToString()))
        {
            highScoreNum.text = GameManager.instance.highScore.ToString();
        }
    }
}
