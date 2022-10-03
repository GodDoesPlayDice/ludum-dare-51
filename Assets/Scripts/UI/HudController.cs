using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private TextMeshProUGUI healthText;

        private Character _character;

        private void Awake()
        {
            _character = GetComponentInParent<Character>();

            UpdateHealth(_character.Health);
            _character.OnHealthChange += UpdateHealth;
            _character.OnMaxHealthChange += UpdateMaxHealth;
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

        public void ShowUpgradePanel()
        {
            GameObject.FindGameObjectWithTag("UpgradePanel").GetComponent<UpgradeMenuController>().Show();
        }
    }
}