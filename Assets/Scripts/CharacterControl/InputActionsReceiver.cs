using UnityEngine;
using UnityEngine.InputSystem;

namespace CharacterControl
{
    public class InputActionsReceiver : MonoBehaviour
    {
        [Header("Character Input Values")] public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool esc;
        public bool upgrades;
        public bool sprint;

        [Header("Movement Settings")] public bool analogMovement;

        [Header("Mouse Cursor Settings")] public bool cursorLocked = true;
        public bool cursorInputForLook = true;
        private Stamina _stamina;
        private CharacterPhysics _tps;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED

        public void OnControlsChanged(PlayerInput input)
        {
            Debug.Log(input.currentControlScheme);
        }

        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnEsc(InputValue value)
        {
            EscInput(value.isPressed);
        }

        public void OnUpgrades(InputValue value)
        {
            UpgradesInput(value.isPressed);
        }

        public void OnJump(InputValue value)
        {
            // don't have time for better solution 
            _tps ??= GetComponent<CharacterPhysics>();
            _stamina ??= GetComponent<Stamina>();
            if (_stamina.CurrentStamina < _tps.JumpStaminaCost)
                JumpInput(false);
            else
            {
                if (value.isPressed)
                    _stamina.CurrentStamina -= _tps.JumpStaminaCost;

                JumpInput(value.isPressed);
            }
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }
#endif


        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        private void EscInput(bool newEscState)
        {
            esc = newEscState;
        }

        private void UpgradesInput(bool newUpgradesState)
        {
            upgrades = newUpgradesState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            // SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}