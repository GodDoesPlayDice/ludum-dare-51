using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class UpgradeManager : MonoBehaviour
{
    public int upgradesPerLvl = 3;

    public int LevelsAvailable
    {
        get => _levelsAvailable;
        set
        {
            if (value != _levelsAvailable)
                OnAvailableLvlChange?.Invoke(value);
            _levelsAvailable = value;
        }
    }

    private Upgrade[] allUpgrades;
    private int[] upgradeWeights;

    private int _levelsAvailable;

    private Upgrade[] currentLvlUpgrades;

    private int appliedUpgrades = 0;
    private Timer _timer;


    public event Action<int> OnAvailableLvlChange;

    void Start()
    {
        _timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        _timer.OnCycleChange += (cycle) => LevelsAvailable = cycle - appliedUpgrades;

        if (allUpgrades == null)
        {
            LoadAllUpgrades();
        }

        if (upgradeWeights == null)
        {
            FillUpgradeWeights();
        }
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

    public void UpgradeApplied()
    {
        appliedUpgrades++;
        currentLvlUpgrades = null;
        LevelsAvailable--;
    }

    private Upgrade GetRandomUpgrade()
    {
        if (allUpgrades == null)
        {
            LoadAllUpgrades();
        }
        if (upgradeWeights == null)
        {
            FillUpgradeWeights();
        }
        var index = upgradeWeights[Random.Range(0, upgradeWeights.Length)];
        return allUpgrades[index];
    }

    private void LoadAllUpgrades()
    {
        allUpgrades = Resources.LoadAll<Upgrade>("Upgrades").Where(it  => it.active).ToArray();
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
