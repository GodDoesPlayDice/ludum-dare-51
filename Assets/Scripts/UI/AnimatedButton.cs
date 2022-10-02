using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
<<<<<<< Updated upstream
    public class AnimatedButton : MonoBehaviour, IPointerEnterHandler
=======
    public class AnimatedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
>>>>>>> Stashed changes
    {
        [SerializeField] private RectTransform textRectTransform;

<<<<<<< Updated upstream
        [Space] [Header("On Pointer Enter")] [SerializeField]
        private float duration = 1f;

        [SerializeField] private int vibrato = 5;
        [SerializeField] private float elasticity = 2f;
=======
        private Tweener _rotationTweener;
        private Tweener _scaleTweener;

        public void OnPointerEnter(PointerEventData eventData)
        {
            ScaleUp();
        }

        private void ScaleUp()
        {
            _scaleTweener = textRectTransform.DOScale(Vector3.one * scaleMultiplier, .2f).SetUpdate(true);
        }
>>>>>>> Stashed changes

        [Space] private Tweener _pointerEnterTweener;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_pointerEnterTweener is {active: true})
                return;
<<<<<<< Updated upstream
            _pointerEnterTweener = textRectTransform.DOPunchScale(Vector3.one * 0.1f, duration, vibrato, elasticity).SetUpdate(true);
=======
            _rotationTweener = textRectTransform.DOPunchRotation(new Vector3(0, 0, 15), 0.4f);
>>>>>>> Stashed changes
        }
    }
}