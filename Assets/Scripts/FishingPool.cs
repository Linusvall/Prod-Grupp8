using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPool : MonoBehaviour
{


    public GameObject[] fishInThePool;
    readonly private static System.Random rand = new(); 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public (Fish, GameObject) GetRandomFish()
    {
        GameObject fish = Instantiate<GameObject>(fishInThePool[rand.Next(fishInThePool.Length - 1)]);
        return (fish.GetComponent<Fish>(), fish);
    }
}
