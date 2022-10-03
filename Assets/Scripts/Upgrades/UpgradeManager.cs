using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public int upgradesPerLvl = 3;

    public int levelsAvailable;

    private Upgrade[] allUpgrades;
    private int[] upgradeWeights;

    private Upgrade[] currentLvlUpgrades;

    void Start()
    {
        // TODO: LOAD ALL UPGRADES
        allUpgrades = Resources.LoadAll<Upgrade>("Upgrades");

        Debug.Log(allUpgrades.Length);

        FillUpgradeWeights();
    }

    public Upgrade[] GetCurrentAvailableUpgrades()
    {
        if (currentLvlUpgrades == null || currentLvlUpgrades.Length > 0)
        {
            var uniqueUpgrades = new HashSet<Upgrade>();
            while (uniqueUpgrades.Count < upgradesPerLvl)
            {
                uniqueUpgrades.Add(GetRandomUpgrade());
            }
            currentLvlUpgrades = uniqueUpgrades.ToArray();
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
        var tmpWeightList = new List<int>();
        for (int ind = 0; ind < allUpgrades.Length; ind ++)
        {
            var upgrade = allUpgrades[ind];
            for (int i = 0; i < upgrade.weight; i ++)
            {
                tmpWeightList.Add(ind);
            }
        }
        upgradeWeights = tmpWeightList.ToArray();
    }
}
