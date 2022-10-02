using DG.Tweening;
using Sound;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class AnimatedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private AudioClip onClickSound;
        [SerializeField] private AudioClip onPointerEnterSound;

        [SerializeField] private RectTransform textRectTransform;
        [SerializeField] [Range(1f, 2f)] private float scaleMultiplier = 1.2f;

        private Tweener _rotationTweener;
        private Tweener _scaleTweener;

        public void OnPointerEnter(PointerEventData eventData)
        {
            SoundManager.Instance.PlaySfxSimple(onPointerEnterSound, .3f);
            ScaleUp();
        }

        private void ScaleUp()
        {
            _scaleTweener = textRectTransform.DOScale(Vector3.one * scaleMultiplier, .2f).SetUpdate(true);
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
            SoundManager.Instance.PlaySfxSimple(onClickSound, .2f);
            _rotationTweener = textRectTransform.DOPunchRotation(new Vector3(0, 0, 15), 0.4f);
        }
    }
}