using System;
using System.Collections;
using Sound;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Screens
{
    public class DeathScreen : ScreenBase
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button exitButton;

        private int _nextSceneIndex;

        protected override void Awake()
        {
            base.Awake();
            playButton.onClick.AddListener(OnPlayClicked);
            exitButton.onClick.AddListener(OnExitClicked);

            LoadingScreenController.Instance.OnShowEnded += () => { SceneManager.LoadScene(_nextSceneIndex); };
            ToggleFullScreen(false);
        } 

        protected override void OnExitClicked()
        {
            SoundManager.Instance.SwapMusicTrack(SoundManager.Instance.menuMusic);
            _nextSceneIndex = 0;
            LoadingScreenController.Instance.ToggleScreen(true);
        }

        protected override void OnPlayClicked()
        {
            SoundManager.Instance.SwapMusicTrack(SoundManager.Instance.gameplayMusic);
            _nextSceneIndex = 1;
            LoadingScreenController.Instance.ToggleScreen(true);
        }
    }
}