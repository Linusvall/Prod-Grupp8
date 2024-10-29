using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Setting", menuName = "ScriptableObjects/SettingsFile", order = 1)]
public class SettingsObject : ScriptableObject
{

    public float MovementSpeed = 5;

    public int UpgradeCost = 1;
   
    public float StaminaModifer = 0;

    public float ClockVolume = 0.5f;

    public float GameVolume = 0.5f;

    public float WormBucketVolume = 0.5f;

    public float FishSpawnRateModifer = 0; 

}
