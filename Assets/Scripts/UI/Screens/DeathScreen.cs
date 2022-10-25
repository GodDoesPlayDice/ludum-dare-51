using System;
using System.Collections;
using Mono.Cecil;
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

        [SerializeField] private TMP_Text playTime;
        [SerializeField] private Timer timer;

        private int _nextSceneIndex;
        private Character _character;

        protected override void Awake()
        {
            base.Awake();
            playButton.onClick.AddListener(OnPlayClicked);
            exitButton.onClick.AddListener(OnExitClicked);
            _character = GetComponentInParent<Character>();

            LoadingScreenController.Instance.OnShowEnded += () => { SceneManager.LoadScene(_nextSceneIndex); };
            ToggleWholeScreen(false);
        }


        private void Update()
        {
            if (_character.IsAlive)
                playTime.text = $"Survived for: {Math.Round(timer.fullTime)} seconds";
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