using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Powerup : MonoBehaviour
{
    //Defines the Tank Data and makes a list of the  buffs the duration of them.
    public TankData data;
    public List<Powerup> buffs;
    public List<Powerup> buffDuration;

	// Use this for initialization
    //When the game starts it deines the list of powerups,data,and the buff duration.
	void Start ()
    {
        buffs = new List<Powerup>();
        data = GetComponent<TankData>();
        buffDuration = new List<Powerup>();
	}

    //If the powerup is not permenat then it will activate it for a short amount of time to the player.
    public void add(Powerup pu) //pu=powerup
    {
        pu.OnActivated(data);
        pu.buffDurationCurrent = pu.buffDurationMax;
        if (pu.isPerm == false)
        {
            buffs.Add(pu);
        }
    }

    // Update is called once per frame
    //On every update for each buff it will add a buff to the player for a certain amount of time.For each expired buff it will deactivate it and clear it.
    void Update ()
    {
        foreach (Powerup buff in buffs)
        {
            buff.buffDurationCurrent -= Time.deltaTime;
            if (buff.buffDurationCurrent <= 0)
            {
                buffDuration.Add(buff);
            }
        }
        foreach (Powerup expiredBuff in buffDuration)
        {
            buffs.Remove(expiredBuff);
            expiredBuff.OnDeactivated(data);
        }
        buffDuration.Clear();
	}

}
