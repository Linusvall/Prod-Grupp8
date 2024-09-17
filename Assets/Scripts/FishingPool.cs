using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishingPool : MonoBehaviour
{


    public Fish[] fishInThePool;
    //readonly private static System.Random rand = new(); 


    public Fish GetRandomFish()
    {
        Fish fish = Instantiate<Fish>(fishInThePool[Random.Range(0, fishInThePool.Length)]);

        return (fish);
    }
}
