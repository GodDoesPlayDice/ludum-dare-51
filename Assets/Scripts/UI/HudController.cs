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
            healthSlider.value = health / _character.MaxHealth;
            healthText.text = $"{health}/{_character.MaxHealth}";
        }

        private void UpdateMaxHealth(float maxHealth)
        {
            healthSlider.value = _character.Health / maxHealth;
            healthText.text = $"{_character.Health}/{maxHealth}";
        }
    }
}