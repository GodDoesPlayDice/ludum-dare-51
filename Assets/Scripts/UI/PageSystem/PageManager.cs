using UnityEngine;

namespace UI.PageSystem
{
    public class PageManager : MonoBehaviour
    {
        [SerializeField] private Page initialPage;
        [SerializeField] private float pagesFadeDuration;

        private Page _currentPage;

        public void ShowPage(Page page)
        {
            if (page == null)
                return;

            if (_currentPage != null)
            {
                _currentPage.FadingArea.ToggleArea(false, pagesFadeDuration,
                    () => { _currentPage.FadingArea.CanvasGroup.interactable = false; },
                    () => { }
                );
            }

            page.FadingArea.ToggleArea(true, pagesFadeDuration,
                () => { _currentPage = page; });
        }

        public void GoBack()
        {
        }
    }
}