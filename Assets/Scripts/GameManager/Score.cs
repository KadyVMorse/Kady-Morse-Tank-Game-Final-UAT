using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    //Defines the Scores text and data.
    public Text score;
    public TankData data;

    // Use this for initialization
    //When the game starts it loads the Tank Data and text of score.
    void Start()
    {
        score = GetComponent<Text>();
        data = GetComponentInParent<TankData>();
        OverLayScore();
    }

    // Update is called once per frame
    //On update it updates  the score if the tank has scored.
    void Update()
    {
        OverLayScore();
    }

    // This sets the Players score to the score on screen.
    void OverLayScore()
    {
        score.text = data.score.ToString();
    }
}