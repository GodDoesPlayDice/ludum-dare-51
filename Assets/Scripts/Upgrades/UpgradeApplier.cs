using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        if (action.type == UpgradeType.INC_MAX_LIFE)
        {
            GetCharacter().MaxHealth += action.value;
        }
        else if (action.type == UpgradeType.HEAL)
        {
            var player = GetCharacter();
            player.Heal(player.MaxHealth / 100f * action.value);
        }
        else if (action.type == UpgradeType.INC_DAMAGE_PERCENT)
        {
            GetWeaponsManager().IncreaseDamagePercent(action.value);
        }
        else if (action.type == UpgradeType.INC_ATTACK_RATE)
        {
        }
        else if (action.type == UpgradeType.INC_MOVESPEED)
        {
        }
        else if (action.type == UpgradeType.MAGIC_MISSILE)
        {
            var weaponManager = GetWeaponsManager();
            weaponManager.AddWeapon(weaponManager.allWeapons.Where(it => it.weaponName == "Default").First());
        }
        else if (action.type == UpgradeType.FIREBALL)
        {
            //var weaponManager = GetWeaponsManager();
            //weaponManager.AddWeapon(weaponManager.allWeapons.Where(it => it.weaponName == "Fireball").First());
        }
        else if (action.type == UpgradeType.INC_CRIT_CHANCE)
        {
        }
        else if (action.type == UpgradeType.INC_CRIT_MULTIPLIYER)
        {
        }
        else if (action.type == UpgradeType.INC_STAMINA)
        {
        }
    }

    private Character GetCharacter()
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

    private WeaponsManager GetWeaponsManager()
    {
        return GameObject.FindGameObjectWithTag("Weapons").GetComponent<WeaponsManager>();
    }
}
