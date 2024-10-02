using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/UpgradeDataContainer", order = 1)]
public class UpgradeDataContainer : ScriptableObject
{
    public string UpgradeName;
    public float RealInPower;
    public float RareFishModifer;
    public float StaminaModifer;
    public int CostOfUpgrade;
}
