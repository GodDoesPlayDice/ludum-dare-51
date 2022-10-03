using System;
using System.Collections;
using Sound;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class PauseScreen : ScreenBase
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button backButton;

        [Space] [SerializeField] private TMP_Dropdown qualitySettingDropDown;
        [SerializeField] private TMP_Dropdown soundSettingDropDown;

        private Character _character;
        private PauseController _pauseController;

        protected override void Awake()
        {
            base.Awake();
            playButton.onClick.AddListener(OnPlayClicked);
            settingsButton.onClick.AddListener(OnSettingsClicked);
            exitButton.onClick.AddListener(OnExitClicked);
            backButton.onClick.AddListener(OnBackClicked);

            _character = GetComponentInParent<Character>();
            _pauseController = _character.GetComponent<PauseController>();

            LoadingScreenController.Instance.OnShowEnded += () => { SceneManager.LoadScene(0); };
        }

        private IEnumerator Start()
        {
            qualitySettingDropDown.onValueChanged.AddListener(SetGraphicsQuality);
            qualitySettingDropDown.value = QualitySettings.GetQualityLevel();

            soundSettingDropDown.onValueChanged.AddListener(SetSoundSettings);
            soundSettingDropDown.value = (int) SoundManager.Instance.CurrentSoundMode;

            yield return new WaitForEndOfFrame();
            LoadingScreenController.Instance.ToggleScreen(false);
        }

        protected override void OnExitClicked()
        {
            _pauseController.TogglePause(false);
            SoundManager.Instance.SwapMusicTrack(SoundManager.Instance.menuMusic);
            LoadingScreenController.Instance.ToggleScreen(true);
        }

        protected override void OnPlayClicked()
        {
            _pauseController.TogglePause(false);
        }
    }
}