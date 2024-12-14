using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public StatsContainer stats = new();
    public Fish caughtFish;

    private readonly List<Fish> _caughtFish = new();
    public int upgradePoints; 
    
    public static Func<Player> GetInstance = () => null;
    private void Awake()
    {
        GetInstance = () => this;
    }

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < 10; i++)
            {
                AddFish(caughtFish);
            }
        }
         if (Input.GetButtonDown("Inventory"))
        {
            WindowsVoice.addToSpeechQueue("You have " + (upgradePoints + 2) + "upgrade points available");
            WindowsVoice.addToSpeechQueue("You have " + Mathf.FloorToInt(GetReelInPower()) + "reeling modifier");
            WindowsVoice.addToSpeechQueue("You have " + Mathf.FloorToInt(GetRareFishModifer()) + "rarity fish modifier");
            WindowsVoice.addToSpeechQueue("You have " + Mathf.FloorToInt(GetStaminaModifer()) + "stamina modifier");
        }
        
    }

    public float GetReelInPower()
    {
        return stats.PlayerStats.RealInPower;
    }
    public float GetRareFishModifer()
    {
        return stats.PlayerStats.RareFishModifer;
    }
    public float GetStaminaModifer()
    {
        return stats.PlayerStats.StaminaModifer;
    }

    public void AddFish(Fish fish)
    {
        _caughtFish.Add(fish);
    }

    public void RemoveFish(Fish fish)
    {
        _caughtFish.Remove(fish);
    }

    public void AddUpgrade(UpgradeDataContainer upgrade)
    {
        stats.AddUpgrade(upgrade);
    }

    public int SellFish()
    {
        int returnValue = 0;

        foreach (var fish in _caughtFish)
        {
            // should add the fish rarity/value
            returnValue += 1;
            Destroy(fish.gameObject);
        }
        _caughtFish.Clear();
        return returnValue;
    }

    public int HowManyFishesHasBeenCaught()
    {
        return _caughtFish.Count;
    }
}
