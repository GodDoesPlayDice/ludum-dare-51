using System;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private Slider staminaSlider;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private TextMeshProUGUI healthText;

        private Character _character;
        private Stamina _stamina;

        private UpgradeMenuController _upgradeMenu;

        private void Awake()
        {
            _character = GetComponentInParent<Character>();
            _stamina = _character.GetComponent<Stamina>();
            _upgradeMenu = _character.GetComponentInChildren<UpgradeMenuController>();

            UpdateHealth(_character.Health);
            UpdateMaxHealth(_character.MaxHealth);
            UpdateStamina(_stamina.CurrentStamina);
            UpdateMaxStamina(_stamina.MaxStamina);

            _character.OnHealthChange += UpdateHealth;
            _character.OnMaxHealthChange += UpdateMaxHealth;
            _stamina.OnCurrentStaminaChange += UpdateStamina;
            _stamina.OnMaxStaminaChange += UpdateMaxStamina;
        }

        public void ShowUpgradePanel()
        {
            _upgradeMenu.Show();
        }

        private void UpdateHealth(float health)
        {
            if (_character.MaxHealth != 0)
                healthSlider.value = health / _character.MaxHealth;
            healthText.text = $"{health}/{_character.MaxHealth}";
        }

        private void UpdateMaxHealth(float maxHealth)
        {
            if (maxHealth != 0)
                healthSlider.value = _character.Health / maxHealth;
            healthText.text = $"{_character.Health}/{maxHealth}";
        }

        private void UpdateStamina(float stamina)
        {
            if (_stamina.MaxStamina != 0)

                staminaSlider.value = stamina / _stamina.MaxStamina;
        }

        private void UpdateMaxStamina(float maxStamina)
        {
            if (maxStamina != 0)
                staminaSlider.value = _stamina.CurrentStamina / maxStamina;
        }
    }
}