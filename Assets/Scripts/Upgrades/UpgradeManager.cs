using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public int upgradesPerLvl = 3;

    private Upgrade[] allUpgrades;
    private int[] upgradeWeights;

    private Upgrade[] currentLvlUpgrades;

    void Start()
    {
        
    }

    public Upgrade[] GetCurrentAvailableUpgrades()
    {
        if (currentLvlUpgrades == null || currentLvlUpgrades.Length > 0)
        {
            var uniqueUpgrades = new HashSet<Upgrade>();
            while (uniqueUpgrades.Count < upgradesPerLvl)
            {

            }
        }
        return currentLvlUpgrades;
    }

    private Upgrade GetRandomUpgrade()
    {
        var index = upgradeWeights[Random.Range(0, upgradeWeights.Length)];
        return allUpgrades[index];
    }

    private void FillUpgradeWeights()
    {

    }
}
