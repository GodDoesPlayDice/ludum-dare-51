using UnityEngine;

namespace AI.States
{
    public class AttackState : State
    {
        public AttackType CurrentAttackType => AttackType.Light; 

        public override State RunCurrentState()
        {
            return StateManager.ChaseState;
        }
    }

    public enum AttackType
    {
        Light,
        Heavy
    }
}