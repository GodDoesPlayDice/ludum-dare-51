using System;
using DG.Tweening;
using Sound;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button creditsButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button backButton;

        [Space] [SerializeField] private float groupFadeDuration = .5f;
        [Space] [SerializeField] private CanvasGroup mainGroup;
        [SerializeField] private CanvasGroup settingsGroup;
        [SerializeField] private CanvasGroup creditsGroup;
        [SerializeField] private CanvasGroup backButtonGroup;
        [SerializeField] private CanvasGroup wholeScreenGroup;

        [Space] [SerializeField] private TMP_Dropdown qualitySettingDropDown;
        [SerializeField] private TMP_Dropdown soundSettingDropDown;

        private CanvasGroup _currentGroup;


        public void ToggleWholeScreen(bool isEnable)
        {
            if (wholeScreenGroup == null)
                return;

            if (!wholeScreenGroup.gameObject.activeSelf && isEnable)
                wholeScreenGroup.gameObject.SetActive(true);
            TweenGroup(wholeScreenGroup, isEnable);
            if (isEnable)
                UpdateGroups(mainGroup);
        }

        private void Awake()
        {
            if (playButton != null)
                playButton.onClick.AddListener(OnPlayClicked);
            if (settingsButton != null)
                settingsButton.onClick.AddListener(OnSettingsClicked);
            if (creditsButton != null)
                creditsButton.onClick.AddListener(OnCreditsClicked);
            if (exitButton != null)
                exitButton.onClick.AddListener(OnExitClicked);
            if (backButton != null)
                backButton.onClick.AddListener(OnBackPressed);

            if (qualitySettingDropDown)
            {
                qualitySettingDropDown.onValueChanged.AddListener(SetQuality);
                qualitySettingDropDown.value = QualitySettings.GetQualityLevel();
            }

            if (soundSettingDropDown)
            {
                soundSettingDropDown.onValueChanged.AddListener(SetSoundSettings);
                soundSettingDropDown.value = (int) SoundManager.Instance.CurrentSoundMode;
            }

            UpdateGroups(mainGroup);
        }

        private void UpdateGroups(CanvasGroup enabledGroup)
        {
            if (backButton != null)
                TweenGroup(backButtonGroup, enabledGroup != mainGroup);

            TweenGroup(mainGroup, enabledGroup == mainGroup);
            if (settingsButton != null)
                TweenGroup(settingsGroup, enabledGroup == settingsGroup);
            if (creditsButton != null)
                TweenGroup(creditsGroup, enabledGroup == creditsGroup);
        }

        private void TweenGroup(CanvasGroup group, bool isEnable, Action onComplete = null)
        {
            if (!group.gameObject.activeSelf && isEnable)
                group.gameObject.SetActive(true);
            DOTween.To(() => group.alpha, x => group.alpha = x, isEnable ? 1f : 0f, groupFadeDuration).SetUpdate(true)
                .OnComplete(
                    () =>
                    {
                        if (!isEnable)
                            group.gameObject.SetActive(false);
                        onComplete?.Invoke();
                    }
                );
        }

        private void OnBackPressed()
        {
            UpdateGroups(mainGroup);
        }

        private void OnPlayClicked()
        {
            // if this menu is child of the player then esc pause
            var character = GetComponentInParent<Character>();
            if (character != null)
            {
                var pauseController = character.GetComponent<PauseController>();
                pauseController.TogglePause(false);
            }
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

        public void SetSoundSettings(int soundMode)
        {
            SoundManager.Instance.SetSoundMode(soundMode);
        }
    }
}