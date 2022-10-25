using UnityEngine;

namespace UI.PageSystem
{
    public class Page : MonoBehaviour
    {
        [field: SerializeField] public IAnimatedUIArea FadingArea { get; private set; }
        [HideInInspector] public Page parentPage;
    }
}