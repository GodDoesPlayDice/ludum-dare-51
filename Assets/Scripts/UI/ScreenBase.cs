using System;
using System.Collections.Generic;
using DG.Tweening;
using Sound;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public abstract class ScreenBase : MonoBehaviour
    {
        [FormerlySerializedAs("fullScreenGroup")] [SerializeField]
        private ScreenGroup wholeScreenGroup;

        [SerializeField] private ScreenGroup backButtonGroup;
        [SerializeField] private List<ScreenGroup> screenGroups;

        protected virtual void Awake()
        {
            ShowGroup(ScreenGroupTypes.Main);
        }

        protected virtual void OnPlayClicked()
        {
        }

        protected virtual void OnExitClicked()
        {
        }

        protected virtual void OnSettingsClicked()
        {
            ShowGroup(ScreenGroupTypes.Settings);
        }

        protected virtual void OnCreditsClicked()
        {
            ShowGroup(ScreenGroupTypes.Credits);
        }

        protected virtual void OnBackClicked()
        {
            ShowGroup(ScreenGroupTypes.Main);
        }

        public void ToggleWholeScreen(bool isEnable)
        {
            if (wholeScreenGroup == null)
                return;
            if (isEnable && !wholeScreenGroup.gameObject.activeSelf)
                wholeScreenGroup.gameObject.SetActive(true);
            TweenGroup(wholeScreenGroup, isEnable);

            if (isEnable)
                ShowGroup(ScreenGroupTypes.Main);
        }

        protected void ShowGroup(ScreenGroupTypes groupType)
        {
            foreach (var screenGroup in screenGroups)
            {
                if (screenGroup.GroupType == groupType && !screenGroup.gameObject.activeSelf)
                    screenGroup.gameObject.SetActive(true);
                TweenGroup(screenGroup, screenGroup.GroupType == groupType);

                if (backButtonGroup != null)
                {
                    var isBackVisible = groupType != ScreenGroupTypes.Main;
                    TweenGroup(backButtonGroup, isBackVisible);
                }
            }
        }


        protected void TweenGroup(ScreenGroup group, bool isEnable, float duration = .3f, Action onComplete = null)
        {
            if (!group.gameObject.activeSelf && isEnable)
                group.gameObject.SetActive(true);
            DOTween.To(() => group.CanvasGroup.alpha, x => group.CanvasGroup.alpha = x, isEnable ? 1f : 0f, duration)
                .SetUpdate(true)
                .OnComplete(
                    () =>
                    {
                        if (!isEnable)
                            group.gameObject.SetActive(false);
                        onComplete?.Invoke();
                    }
                );
        }

        protected void SetGraphicsQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }

        protected void SetSoundSettings(int soundMode)
        {
            SoundManager.Instance.SetSoundMode(soundMode);
        }
    }
}