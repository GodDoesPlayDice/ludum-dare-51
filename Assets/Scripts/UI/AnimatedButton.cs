using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class AnimatedButton : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private RectTransform textRectTransform;

        [Space] [Header("On Pointer Enter")] [SerializeField]
        private float duration = 1f;

        [SerializeField] private int vibrato = 5;
        [SerializeField] private float elasticity = 2f;

        [Space] private Tweener _pointerEnterTweener;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_pointerEnterTweener is {active: true})
                return;
            _pointerEnterTweener = textRectTransform.DOPunchScale(Vector3.one * 0.1f, duration, vibrato, elasticity).SetUpdate(true);
        }
    }
}