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
        }

        private void UpdateHealth(float health)
        {
            healthSlider.value = health / _character.maxHealth;
            healthText.text = $"{health}/{_character.maxHealth}";
        }
    }
}