using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class LoadingScreenController : MonoBehaviour
    {
        public static LoadingScreenController Instance;

        private CanvasGroup _canvasGroup;

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
            DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, isEnable ? 1f : 0f, .5f).SetUpdate(true);
        }
    }
}