using System;
using UnityEngine;

namespace UI
{
    [Serializable]
    public class ScreenGroup : MonoBehaviour
    {
        [field: SerializeField] public ScreenGroupTypes GroupType { get; private set; }
        
        public CanvasGroup CanvasGroup { get; private set; }

        private void Awake()
        {
            CanvasGroup = GetComponent<CanvasGroup>();
        }
    }

    public enum ScreenGroupTypes
    {
        FullScreen,
        Main,
        Settings,
        Credits,
        Back,
    }
}