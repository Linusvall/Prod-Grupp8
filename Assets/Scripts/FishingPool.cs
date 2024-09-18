using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishingPool : MonoBehaviour
{
    public Fish[] trashInThePool;
    public Fish[] commonFishInThePool;
    public Fish[] rareFishInThePool;
    public Fish[] mythicalFishInThePool;
    public Fish[] funnyFishInThePool;
    //readonly private static System.Random rand = new(); 


    public Fish GetRandomFish()
    {
        Fish fish;

        int x = Random.Range(1, 11);

        if (x == 1)
        {
            fish = Instantiate<Fish>(trashInThePool[Random.Range(0, trashInThePool.Length)]);
            return (fish);
        }

        int n = Random.Range(1,101);

        switch (n)
        {
            case <= 60:
            {
                fish = Instantiate<Fish>(commonFishInThePool[Random.Range(0, commonFishInThePool.Length)]);
                break;
            }

            case < 90:
            {
                fish = Instantiate<Fish>(rareFishInThePool[Random.Range(0, rareFishInThePool.Length)]);
                break;
            }

            case < 99:
            {
                fish = Instantiate<Fish>(mythicalFishInThePool[Random.Range(0, mythicalFishInThePool.Length)]);
                break;
            }
            default:
            {
                fish = Instantiate<Fish>(funnyFishInThePool[Random.Range(0, funnyFishInThePool.Length)]);
                break;
            }
        }

        return (fish);
    }

    public void Move()
    {

    }
}
