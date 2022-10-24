using System;
using DG.Tweening;
using TMPro;
using UI.Screens;
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
        [SerializeField] private GameObject touchControls;

        [SerializeField] private Button pauseButton;

        private Character _character;
        private Stamina _stamina;
        private PauseController _pauseController;

        private UpgradeMenuController _upgradeMenu;

        private void Awake()
        {
            _character = GetComponentInParent<Character>();
            _stamina = _character.GetComponent<Stamina>();
            _pauseController = _character.GetComponent<PauseController>();
            _upgradeMenu = _character.GetComponentInChildren<UpgradeMenuController>();

            UpdateHealth(_character.Health);
            UpdateMaxHealth(_character.MaxHealth);
            UpdateStamina(_stamina.CurrentStamina);
            UpdateMaxStamina(_stamina.MaxStamina);

            _character.OnHealthChange += UpdateHealth;
            _character.OnMaxHealthChange += UpdateMaxHealth;
            _stamina.OnCurrentStaminaChange += UpdateStamina;
            _stamina.OnMaxStaminaChange += UpdateMaxStamina;


            pauseButton.onClick.AddListener(() => { _pauseController.TogglePause(true, true); });


#if UNITY_ANDROID || UNITY_IOS
            touchControls.SetActive(true);
#else
            touchControls.SetActive(false);
#endif
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