using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade", order = 1)]
public class Upgrade : ScriptableObject
{
    public Texture2D icon;
    public string upgradeName;
    public string description;
    public int weight;
    public UpgradeAction[] upgradeAction;
}
