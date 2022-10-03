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
        [SerializeField] private Button showUpgradesButton;
        [SerializeField] private Slider timeSlider;

        private Character _character;
        private UpgradeManager _upgradeManager;
        private Timer _timer;

        private void Awake()
        {
            _character = GetComponentInParent<Character>();
            _upgradeManager = GameObject.FindGameObjectWithTag("Upgrade").GetComponent<UpgradeManager>();
            _timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();

            UpdateHealth(_character.Health);
            _character.OnHealthChange += UpdateHealth;
            _character.OnMaxHealthChange += UpdateMaxHealth;
            _upgradeManager.OnAvailableLvlChange += UpdateUpgradeButtonLevel;
            _timer.OnCurrentCycleTimeChange += UpdateTimer;
        }

        private void OnEnable()
        {
            //UpdateUpgradeButtonLevel(_upgradeManager.LevelsAvailable);
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

        private void UpdateUpgradeButtonLevel(int lvlAvailable)
        {
            showUpgradesButton.GetComponentInChildren<TMP_Text>().text = lvlAvailable.ToString();
            var available = lvlAvailable > 0;
            showUpgradesButton.interactable = available;
            //var colors = showUpgradesButton.colors;
            //colors.normalColor = available ? new Color(144, 183, 125) : Color.gray;
        }

        private void UpdateTimer(float time)
        {
            timeSlider.value = time;
        }

        public void ShowUpgradePanel()
        {
            GameObject.FindGameObjectWithTag("UpgradePanel").GetComponent<UpgradeMenuController>().Show();
        }
    }
}