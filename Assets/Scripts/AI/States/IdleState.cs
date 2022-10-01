using UnityEngine;

namespace AI.States
{
    public class IdleState : State
    {
        private float _idlingTimer;

        public override State RunCurrentState()
        {
            if (Controller.ShouldAttackTarget)
                return StateManager.AttackState;

            if (Controller.ShouldChaseTarget)
                return StateManager.ChaseState;

            _idlingTimer += Time.deltaTime;
            if (_idlingTimer > 1f)
            {
                _idlingTimer = 0f;
                return StateManager.WanderState;
            }

            return this;
        }
    }
}