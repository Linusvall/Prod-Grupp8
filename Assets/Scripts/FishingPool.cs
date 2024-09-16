using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPool : MonoBehaviour
{


    public Fish[] fishInThePool;
    readonly private static System.Random rand = new(); 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
    }


    public Fish GetRandomFish()
    {
        Fish fish = Instantiate<Fish>(fishInThePool[rand.Next(fishInThePool.Length)]);
        return (fish);
    }
}
