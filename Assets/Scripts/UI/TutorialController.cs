using System;
using UnityEngine;

namespace UI
{
    public class TutorialController : MonoBehaviour
    {
        private HudController _hudController;


        private void Awake()
        {
            _hudController = GetComponent<HudController>();
            

        }
    }
}