using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartMenuController : MonoBehaviour
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button creditsButton;
        [SerializeField] private Button exitGameButton;
        [SerializeField] private Button backButton;

        private void Awake()
        {
            startGameButton.onClick.AddListener(OnPlayClicked);
            settingsButton.onClick.AddListener(OnSettingsClicked);
            creditsButton.onClick.AddListener(OnCreditsClicked);
            exitGameButton.onClick.AddListener(OnExitClicked);
            backButton.onClick.AddListener(OnBackPressed);
        }

        private void OnBackPressed()
        {
        }

        private void OnPlayClicked()
        {
        }

        private void OnSettingsClicked()
        {
        }

        private void OnCreditsClicked()
        {
        }

        private void OnExitClicked()
        {
        }
    }
}