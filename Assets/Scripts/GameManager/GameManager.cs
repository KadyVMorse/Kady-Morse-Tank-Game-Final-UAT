using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //States the game objects in the level and the list of players or enemy/spawnpoints for each to be generated,the volume  the differnt game states,spawn point and waypoints,player 1 and player two ,and highscores.
    public static GameManager instance;
    public int score;
    public int numAIToSpawn;
    public int numAICurrent;

    public GameObject playerPrefab;
    public GameObject aiPrefab;

    public Transform charactersHolder;
    public Transform pickupsHolder;

    public List<TankData> players;
    public List<TankData> aiUnits;

    public List<Transform> characterSpawns;
    public List<GameObject> spawnedItems;

    public MapGenerator mapMaker;
    public MenuSettings settingsLoader;

    public float sfxVol;
    public float musicVol;
    public bool isMultiplayer;
    public int seedNum;
    public int mapMode;
    public int highScore;
    public int scorePerKill;

    
    public GameObject player1;
    public GameObject player2;

    public int p1Lives;
    public int p2Lives;
    public int pLivesTotal;

    public int p1Controller = 0;
    public int p2Controller = 1;

    public int p1Score;
    public int p2Score;

    // Use this for initialization
    //When the game is awake it will destroy the game manager when differnt levels are loaded.It will also load the settings adn the high scores of the game.
    void Awake ()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        if (settingsLoader != null)
        {
            settingsLoader.loadSettings();
        }
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        loadSettings();
    }

    // On Update the players,and highscores will load.
    void Update ()
    {
        //If there is no player then it will Respawn the AI.
        if (!characterSpawns.Count.Equals(0))
        {
            respawnAI();
        }
        if (!characterSpawns.Count.Equals(0))
        {
            //If the player loses a life and its greater than zero than the player will respawn.
            if (player1 == null)
            {
                p1Lives -= 1;
                if (p1Lives >= 0)
                {
                    player1 = respawnPlayer();
                    setUpPlayer1();
                } 
            }
            //If the second player loses a life and its greater than zero than the player will respawn.
            if (isMultiplayer == true)
            {
                if (player2 == null)
                {
                    p2Lives -= 1;
                    if (p2Lives >= 0)
                    {
                        player2 = respawnPlayer();
                        setUpPlayer2();
                    }
                }
            }
        }
        //It will restart the High Score until game is over.
        setHighScore();
        gameOver();
    }

    //Loads the Players scores,lives,spawne Ai and Items.
    void loadSettings()
    {
        p1Score = 0;
        p2Score = 0;
        p1Lives = pLivesTotal;
        p2Lives = pLivesTotal;
        characterSpawns.Clear();
        players.Clear();
        aiUnits.Clear();
        spawnedItems.Clear();
        numAICurrent = 0;
    }
    //Defines what game over is in Single Player mode and Multiplayer Mode.
    void gameOver()
    {
        if (isMultiplayer == true)
        {
            //If in multiplayer and the lives are less than zero than it will load the Main Menu Scene.
            if (p1Lives < 0 && p2Lives < 0)
            {
                SceneManager.LoadScene(0);
                loadSettings();
            }
        }
        //If it is single player and the player dies then it will load the Main Menu Scene.
        else
        {
            if (p1Lives < 0)
            {
                SceneManager.LoadScene(0);
                loadSettings();
            }
        }
    }

    //Sets the Player 1 score,lives,and data when its loaded into the game.
    void setUpPlayer1()
    {
        TankData playerData = player1.GetComponent<TankData>();
        playerData.score = p1Score;
        playerData.lives = p1Lives;
        playerData.myName = "Player1";
        Controller_Player playerController = player1.GetComponent<Controller_Player>();
        if (p1Controller == 0)
        {
            playerController.selectedController = Controller_Player.controlType.wasd;
        }
        
    }
    //Sets the high score for both players.
    void setHighScore()
    {
        //For each player that gets a higher score than the high score then it will replace the new high score.
        foreach (TankData person in players)
        {
            if (person.score > highScore)
            {
                PlayerPrefs.SetInt("HighScore", person.score);
                highScore = person.score; 
            }
        }
    }
    //Sets the Player 2 score,lives,and data when its loaded into the game.
    void setUpPlayer2()
    {
        TankData playerData = player2.GetComponent<TankData>();
        playerData.score = p2Score;
        playerData.lives = p2Lives;
        playerData.myName = "Player2";
        Controller_Player playerController = player2.GetComponent<Controller_Player>();
        if (p2Controller == 1)
        {
            playerController.selectedController = Controller_Player.controlType.arrows;
        }

    }



    // This respawns the AI randomly when they are killed to a random waypoint on the map.
    void respawnAI()
    {
        while (numAICurrent < numAIToSpawn)
        {
            int randomNum = Random.Range(0, characterSpawns.Count-1);
            Transform locationToSpawn = characterSpawns[randomNum];
            GameObject newAI = Instantiate(aiPrefab, locationToSpawn);
            newAI.transform.SetParent(charactersHolder);
            setAiWaypoints(newAI, locationToSpawn);
            numAICurrent++;
        }
    }

    // This sets the new waypoints for the new AI that eneter the scene.
    void setAiWaypoints(GameObject aiSpawned, Transform spawnLocation)
    {
        foreach (Transform point in spawnLocation.gameObject.GetComponentInParent<Room>().waypoints)
        {
            aiSpawned.GetComponent<Controller_AI>().waypoints.Add(point);
        }
    }

    // This respwns the player to a spawn point if they lose a life.
    GameObject respawnPlayer()
    {
        int randomNum = Random.Range(0, characterSpawns.Count-1);
        Transform spawnLocation = characterSpawns[randomNum];
        GameObject player = Instantiate(playerPrefab, spawnLocation);
        player.transform.SetParent(charactersHolder);
        return player;
    }
    //When the players lose lives or when they die it will set the highscore and and save the prefs and then destroy them.
    private void OnDestroy()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }
}
