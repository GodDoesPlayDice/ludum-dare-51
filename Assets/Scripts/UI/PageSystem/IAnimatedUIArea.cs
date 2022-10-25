using System;
using UnityEngine;

namespace UI.PageSystem
{
    public interface IAnimatedUIArea
    {
        public void ToggleArea(bool isEnable, float duration, Action onStart = null, Action onComplete = null,
            bool useRealtime = true);

        public CanvasGroup CanvasGroup { get; }
    }
}