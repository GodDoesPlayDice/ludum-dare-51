using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class LoadingScreenController : MonoBehaviour
    {
        public static LoadingScreenController Instance;

        private CanvasGroup _canvasGroup;

        public event Action OnShowEnded;
        public event Action OnHideEnded;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0f;
        }

        public void ToggleScreen(bool isEnable)
        {
            DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, isEnable ? 1f : 0f,
                    isEnable ? 1f : 0.5f)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    if (!isEnable) return;
                    OnShowEnded?.Invoke();
                    OnShowEnded = (Action) Delegate.RemoveAll(OnShowEnded, OnShowEnded);
                });
        }
    }
}