
using System.Collections.Generic;

public class StatsContainer 
{

    public struct Stats {
        public float RealInPower;
        public float RareFishModifer;
        public float StaminaModifer;
    }

    private List<UpgradeDataContainer> Upgrades = new ();
    public Stats PlayerStats = new Stats();


    public void AddUpgrade(UpgradeDataContainer upgrade)
    {
        Upgrades.Add(upgrade);
        PlayerStats.RealInPower += upgrade.RealInPower;
        PlayerStats.RareFishModifer += upgrade.RareFishModifer;
        PlayerStats.StaminaModifer += upgrade.StaminaModifer;
    }

    public List<UpgradeDataContainer> GetUpgrades()
    {
        return Upgrades;
    }


}
