using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Setting", menuName = "ScriptableObjects/SettingsFile", order = 1)]
public class SettingsObject : ScriptableObject
{

    public float MovementSpeed = 5;

    public int UpgradeCost = 1;
   
    public float StaminaModifer = 0;

}
