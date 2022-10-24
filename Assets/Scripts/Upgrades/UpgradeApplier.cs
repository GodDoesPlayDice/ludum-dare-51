using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TPS;
using UnityEngine;

public class UpgradeApplier : MonoBehaviour
{
    private UpgradeManager _upgradeManager;

    // Workaround to not change ThirdPersonController code
    private float defaultMoveSpeed = 0;
    private float defaultRunSpeed = 0;
    private float defaultStaminaPerSecond = 0;

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
            var tpController = GetTPController();
            if (defaultMoveSpeed == 0)
            {
                defaultMoveSpeed = tpController.MoveSpeed;
                defaultRunSpeed = tpController.SprintSpeed;
            }
            tpController.MoveSpeed += defaultMoveSpeed / 100f * action.value;
            tpController.SprintSpeed += defaultRunSpeed / 100f * action.value;
        }
        else if (action.type == UpgradeType.MAGIC_MISSILE)
        {
            var weaponManager = GetWeaponsManager();
            weaponManager.AddWeapon(weaponManager.allWeapons.Where(it => it.weaponName == "Default").First());
        }
        else if (action.type == UpgradeType.FIREBALL)
        {
            var weaponManager = GetWeaponsManager();
            weaponManager.AddWeapon(weaponManager.allWeapons.Where(it => it.weaponName == "Fireball").First());
        }
        else if (action.type == UpgradeType.INC_CRIT_CHANCE)
        {
            GetWeaponsManager().IncreaseCritChance(action.value);
        }
        else if (action.type == UpgradeType.INC_CRIT_MULTIPLIYER)
        {
            GetWeaponsManager().IncreaseCritMultiplierPercent(action.value);
        }
        else if (action.type == UpgradeType.INC_STAMINA)
        {
            GetStamina().MaxStamina += action.value;
        } else if (action.type == UpgradeType.INC_STAMINA_RECOVERY)
        {
            var tpController = GetTPController();
            if (defaultStaminaPerSecond == 0)
            {
                defaultStaminaPerSecond = tpController.staminaRegenPerSecond;
            }
            tpController.staminaRegenPerSecond += defaultStaminaPerSecond / 100f * action.value;
        }
    }

    private Character GetCharacter()
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

    private CharacterPhysics GetTPController()
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterPhysics>();
    }

    private Stamina GetStamina()
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<Stamina>();
    }

    private WeaponsManager GetWeaponsManager()
    {
        return GameObject.FindGameObjectWithTag("Weapons").GetComponent<WeaponsManager>();
    }
}
