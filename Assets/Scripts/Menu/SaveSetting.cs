using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSetting : MonoBehaviour
{
    // Defines the UI volume,and the differnt maps in the game.
    public Slider musicVolume;
    public Slider sfxVolume;
    public Toggle randomMap;
    public Toggle mapOfDay;
    public Toggle seededMap;
    public Toggle isMultiEnabled;
    public Text seedNumber;
    public int number;

    //When the player adjusts these settings the game will save them for the player.
    public void saveSettings()
    {
        // When the player adjust the Music Volume ihe game manager saves it.
        GameManager.instance.musicVol = musicVolume.value;
        PlayerPrefs.SetFloat("musicVolume", musicVolume.value);

        // When the player adjusts the SFX Volume the game manager daves it. 
        GameManager.instance.sfxVol = sfxVolume.value;
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume.value);

        // If the player selects multiplayer then it will save multiplayer mod for the game.If the player doesn't chose it then it will load single player mode.
        GameManager.instance.isMultiplayer = isMultiEnabled.isOn;
        if (isMultiEnabled.isOn == false)
        {
            PlayerPrefs.SetInt("isMultiplayer", 0);
        }
        else
        {
            PlayerPrefs.SetInt("isMultiplayer", 1);
        }

        // When the player adjust the seed number the Game manager will save it..
        number = int.Parse(seedNumber.text.ToString());
        GameManager.instance.seedNum = number;
        PlayerPrefs.SetInt("seedNumber", number);

        // If the player selects Map of The Day then the Game Manager will save it and load it.
        if (mapOfDay.isOn == true)
        {
            GameManager.instance.mapMode = 0;
            PlayerPrefs.SetInt("mapMode", 0);
        }
        //If the player doesn't select Map of The Day and selects Random map the the game manager will save it and load it.
        else if (randomMap.isOn == true)
        {
            GameManager.instance.mapMode = 1;
            PlayerPrefs.SetInt("mapMode", 1);
        }
        //If the player does not select Map of The Day or Random Map and chooses seeded map then the Game Manager will save it and load it.
        else if (seededMap.isOn == true)
        {
            GameManager.instance.mapMode = 2;
            PlayerPrefs.SetInt("mapMode", 2);
        }
    }
}
