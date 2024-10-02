using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public StatsContainer stats = new();

    private readonly List<Fish> _caughtFish = new();
    public int upgradePoints; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
        _caughtFish.Clear();
        return returnValue;
    }

    public int HowManyFishesHasBeenCaught()
    {
        return _caughtFish.Count;
    }
}
