using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace CharacterControl
{
    public class InputDeviceEventsListener : MonoBehaviour
    {
        public event Action<InputDevice> OnInputDeviceChange;

        private InputDevice _lastUsedDevice;

        private void Awake()
        {
            InputSystem.onEvent += OnInputSystemEvent;
        }

        private void OnInputSystemEvent(InputEventPtr eventPtr, InputDevice device)
        {
            if (!Application.isPlaying)
                return;
            if (_lastUsedDevice == device)
                return;

            _lastUsedDevice = device;
            OnInputDeviceChange?.Invoke(device);
        }
    }
}