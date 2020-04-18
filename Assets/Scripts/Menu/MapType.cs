using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapType : MonoBehaviour
{
    // Defines that the text of the Seed select.
    public Text seedSelect;

    //The Game Manager loads Map of The Day when the player selects it.
    public void selectMapOfDay()
    {
        GameManager.instance.mapMaker.maptype = MapGenerator.mapType.mapOfTheDay;
    }
    // The Game Manager loads the Random Map when its selected.
    public void selectRandomMap()
    {
        GameManager.instance.mapMaker.maptype = MapGenerator.mapType.randomMap;
    }
    // The Game Manager loads the Seed Input when its selected.
    public void setSeed()
    {
        GameManager.instance.mapMaker.seedValue = int.Parse(seedSelect.text.ToString());
    }
    // The Game Manager loads the Seeded Map when it is selected.
    public void selectSeededMap()
    {
        GameManager.instance.mapMaker.maptype = MapGenerator.mapType.seededMap;
    }
}
