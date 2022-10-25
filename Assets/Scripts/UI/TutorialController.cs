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

        public void StartTutorial()
        {
            // show first message and stop time
            // listen for first input
            
            // wait couple of secs 
            // show second message and stop time 
            // listen for sprint 
            
            // end the tutorial 
        }
    }
}