using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour

{
    // Defines the UI varabils such as volume and the differnt type of maps.
    public Slider musicVolume;
    public Slider sfxVolume;
    public Toggle randomMap;
    public Toggle mapOfDay;
    public Toggle seededMap;
    public Toggle isMultiEnabled;
    public InputField seedNumber;
    public int number;
    public int mapType;

    //This loads the settings of the SFX Volume,Music Volume,Multiplayer and teh differnt maps.
    public void loadSettings()
    {
        // Loads the SFX Volume from the game manager that the player seleted.
        sfxVolume.value = PlayerPrefs.GetFloat("sfx", 0);
        GameManager.instance.sfxVol = PlayerPrefs.GetFloat("sfx", 0);

        // Loads the Music Volume from the game manager that the player selected.
        musicVolume.value = PlayerPrefs.GetFloat("music", 0);
        GameManager.instance.musicVol = PlayerPrefs.GetFloat("music", 0);

        // If the player diden't select multiplayer then it will not load multiplayer from the game manager.
        if (PlayerPrefs.GetInt("Multiplayer", 0) == 0)
        {
            isMultiEnabled.isOn = false;
            GameManager.instance.isMultiplayer = false;
        }

        // Sets the seed number from the game manager based on what the player selected.
        number = PlayerPrefs.GetInt("seed", number);
        GameManager.instance.seedNum = number;
        seedNumber.text = number.ToString();

        // Loads the map that the player chose.
        mapType = PlayerPrefs.GetInt("mapMode", 0);
        if (mapType == 0)
        {
            //If the player selected the Map Of The day then it will play.
            mapOfDay.isOn = true;
            randomMap.isOn = false;
            seededMap.isOn = false;
        }
        else if (mapType == 1)
        {
            //If the player selected the Random Map then it will play.
            mapOfDay.isOn = false;
            randomMap.isOn = true;
            seededMap.isOn = false;
        }
        else
        {
            //If the player selected the Seeded Map then it will play.
            mapOfDay.isOn = false;
            randomMap.isOn = false;
            seededMap.isOn = true;
        }
        //Game Manger will load what the player selects by getting the intger of the map.
        GameManager.instance.mapMode = PlayerPrefs.GetInt("mapMode", 0);
    }
}
