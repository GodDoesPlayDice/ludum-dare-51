using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CharacterControl
{
    public class UIInput : MonoBehaviour
    {
        private InputActionsReceiver _inputsReceiver;

        private void Awake()
        {
            _inputsReceiver = GetComponentInParent<InputActionsReceiver>();
        }

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            _inputsReceiver.MoveInput(virtualMoveDirection);
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
            _inputsReceiver.LookInput(virtualLookDirection);
        }

        public void VirtualJumpInput(bool virtualJumpState)
        {
            _inputsReceiver.JumpInput(virtualJumpState);
        }

        public void VirtualSprintInput(bool virtualSprintState)
        {
            _inputsReceiver.SprintInput(virtualSprintState);
        }
    }
}