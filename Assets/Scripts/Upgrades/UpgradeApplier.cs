using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeApplier : MonoBehaviour
{
    public void ApplyUpgrade(Upgrade upgrade)
    {
        foreach (var action in upgrade.upgradeAction)
        {
            ApplyAction(action);
        }
    }

    private void ApplyAction(UpgradeAction action)
    {
        switch (action.type)
        {
            case UpgradeType.MAX_LIFE:
                GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().maxHealth += action.value;
                break;
            case UpgradeType.HEAL:

                break;
            case UpgradeType.ADD_DAMAGE_PERCENT:

                break;
            case UpgradeType.COOLDOWN:

                break;

            case UpgradeType.MOVESPEED:

                break;
            case UpgradeType.MAGIC_MISSILE:

                break;
            case UpgradeType.FIREBALL:

                break;
        }
    }
}
