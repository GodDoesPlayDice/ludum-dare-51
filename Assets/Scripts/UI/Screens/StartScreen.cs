using System.Collections;
using Sound;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class StartScreen : ScreenBase
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button creditsButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button backButton;

        [Space] [SerializeField] private TMP_Dropdown qualitySettingDropDown;
        [SerializeField] private TMP_Dropdown soundSettingDropDown;

        protected override void Awake()
        {
            base.Awake();
            playButton.onClick.AddListener(OnPlayClicked);
            settingsButton.onClick.AddListener(OnSettingsClicked);
            creditsButton.onClick.AddListener(OnCreditsClicked);
            exitButton.onClick.AddListener(OnExitClicked);
            backButton.onClick.AddListener(OnBackClicked);

            LoadingScreenController.Instance.OnShowEnded += () => { SceneManager.LoadScene(1); };
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
#if !UNITY_EDITOR
                Application.Quit();
#endif
        }

        protected override void OnPlayClicked()
        {
            SoundManager.Instance.SwapMusicTrack(SoundManager.Instance.gameplayMusic);
            LoadingScreenController.Instance.ToggleScreen(true);
        }
    }
}