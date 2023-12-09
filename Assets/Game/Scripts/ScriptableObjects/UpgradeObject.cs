using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class UpgradeObject : ScriptableObject
{
    public string upgradeName;
    public int upgradeLevel;
    public int maxLevel;
    public int upgradeCost;
    public int costMultiplier;

    public int GetCost()
    {
        return upgradeCost * (upgradeLevel * costMultiplier);
    }
}
