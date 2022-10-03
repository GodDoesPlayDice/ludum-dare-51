using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeApplier : MonoBehaviour
{
    private UpgradeManager _upgradeManager;

    private void Awake()
    {
        _upgradeManager = GetComponent<UpgradeManager>();
    }

    public void ApplyUpgrade(Upgrade upgrade)
    {
        _upgradeManager.UpgradeApplied();
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
                GetCharacter().MaxHealth += action.value;
                break;
            case UpgradeType.HEAL:
                var player = GetCharacter();
                player.Heal(player.MaxHealth / 100f * action.value);
                break;
            case UpgradeType.INC_DAMAGE_PERCENT:
                GameObject.FindGameObjectWithTag("Weapons").GetComponent<WeaponsManager>().
                    IncreaseDamagePercent(action.value);
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

    private Character GetCharacter()
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }
}
