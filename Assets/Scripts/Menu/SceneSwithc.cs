using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwithc : MonoBehaviour
{
    //Defines each of the scenes and what it loads.

        //This loads the first scene which is the Main Menu.
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //This loads the second scene which is the Game.
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    //This exits the game when you quit the game.
    public void ExitGame()
    {
        Debug.Log("Exiting Application");
        Application.Quit();
    }
}
