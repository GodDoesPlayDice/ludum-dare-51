using System;
using DG.Tweening;
using UnityEngine;

namespace UI.PageSystem
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadingArea : MonoBehaviour, IAnimatedUIArea
    {
        public CanvasGroup CanvasGroup { get; private set; }

        private void Awake()
        {
            CanvasGroup = GetComponent<CanvasGroup>();
        }

        public void ToggleArea(bool isEnable, float duration, Action onStart = null, Action onComplete = null,
            bool useRealtime = true)
        {
            onStart?.Invoke();
            DOTween.To(() => CanvasGroup.alpha, x => CanvasGroup.alpha = x, isEnable ? 1f : 0f, duration)
                .SetUpdate(useRealtime)
                .OnComplete(
                    () =>
                    {
                        CanvasGroup.interactable = isEnable;
                        onComplete?.Invoke();
                    }
                );
        }
    }
}