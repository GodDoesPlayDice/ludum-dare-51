using UnityEngine;

namespace AI.States
{
    public class AttackState : State
    {
        private float _lastAttackTime;

        public override State RunCurrentState()
        {
            if (Time.time - _lastAttackTime > 1f)
            {
                _lastAttackTime = Time.time;
                Debug.Log("Attacked");
            }

            return StateManager.MoveState;
        }
    }
}