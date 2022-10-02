using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace UI
{
    public class StartMenuController : MonoBehaviour
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button creditsButton;
        [SerializeField] private Button exitGameButton;
        [SerializeField] private Button backButton;

        [Space] [SerializeField] private float groupFadeDuration = .5f;
        [Space] [SerializeField] private CanvasGroup mainGroup;
        [SerializeField] private CanvasGroup settingsGroup;
        [SerializeField] private CanvasGroup creditsGroup;
        [SerializeField] private CanvasGroup backButtonGroup;

        [Space] [SerializeField] private TMP_Dropdown resolutionDropdown;
        private Resolution[] resolutions;

        private CanvasGroup _currentGroup;

        private void Start()
        {
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();
            List<string> options = new List<string>();

            int currentResolutionIndex = 0;
            for (int i = 0; i < resolutions.Length; i++)
            {
                options.Add(resolutions[i].width + " x " + resolutions[i].height);
                if (resolutions[i].width == Screen.currentResolution.width && 
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
        }

        private void Awake()
        {
            startGameButton.onClick.AddListener(OnPlayClicked);
            settingsButton.onClick.AddListener(OnSettingsClicked);
            creditsButton.onClick.AddListener(OnCreditsClicked);
            exitGameButton.onClick.AddListener(OnExitClicked);
            backButton.onClick.AddListener(OnBackPressed);
            UpdateGroups(mainGroup);
        }

        private void UpdateGroups(CanvasGroup enabledGroup)
        {
            TweenGroup(backButtonGroup, enabledGroup != mainGroup);

            TweenGroup(mainGroup, enabledGroup == mainGroup);
            TweenGroup(settingsGroup, enabledGroup == settingsGroup);
            TweenGroup(creditsGroup, enabledGroup == creditsGroup);
        }

        private void TweenGroup(CanvasGroup group, bool isEnable, Action onCompleteCallback = null)
        {
            if (!group.gameObject.activeSelf && isEnable)
                group.gameObject.SetActive(true);
            DOTween.To(() => group.alpha, x => group.alpha = x, isEnable ? 1f : 0f, groupFadeDuration)
                .OnComplete(
                    () =>
                    {
                        if (!isEnable)
                            group.gameObject.SetActive(false);
                    }
                );
        }

        private void OnBackPressed()
        {
            UpdateGroups(mainGroup);
        }

        private void OnPlayClicked()
        {
        }

        private void OnSettingsClicked()
        {
            UpdateGroups(settingsGroup);
        }

        private void OnCreditsClicked()
        {
            UpdateGroups(creditsGroup);
        }

        private void OnExitClicked()
        {
        }

        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }
    }
}