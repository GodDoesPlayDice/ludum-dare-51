using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UI
{
    public class AnimatedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler,
        IPointerMoveHandler
    {
        [SerializeField] private RectTransform textRectTransform;
        [SerializeField] [Range(1f, 2f)] private float scaleMultiplier = 1.2f;

        private Tweener _rotationTweener;
        private Tweener _scaleTweener;
        private bool _isScaledUp;

        public void OnPointerEnter(PointerEventData eventData)
        {
            ScaleUp();
        }

        private void ScaleUp()
        {
            _scaleTweener = textRectTransform.DOScale(Vector3.one * scaleMultiplier, .2f).SetUpdate(true)
                .OnComplete(() => _isScaledUp = true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_scaleTweener is {active: true})
                _scaleTweener.Kill();
            textRectTransform.DOScale(Vector3.one, 0.1f).SetUpdate(true);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_rotationTweener is {active: true})
                return;
            _rotationTweener = textRectTransform.DOPunchRotation(new Vector3(0, 0, 15), 0.4f)
                .OnComplete(() => _isScaledUp = false);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            Debug.Log("pointer move");
            if (!_isScaledUp)
                ScaleUp();
        }
    }
}