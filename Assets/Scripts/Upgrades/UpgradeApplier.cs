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
            case UpgradeType.INC_MAX_LIFE:
                GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().MaxHealth += action.value;
                break;
            case UpgradeType.HEAL:

                break;
            case UpgradeType.INC_DAMAGE_PERCENT:

                break;
            case UpgradeType.INC_ATTACK_RATE:

                break;

            case UpgradeType.INC_MOVESPEED:

                break;
            case UpgradeType.MAGIC_MISSILE:

                break;
            case UpgradeType.FIREBALL:

                break;
            case UpgradeType.INC_CRIT_CHANCE:
                
                break;
            case UpgradeType.INC_CRIT_MULTIPLIYER:
                
                break;
            case UpgradeType.INC_STAMINA:
                
                break;
        }
    }
}
