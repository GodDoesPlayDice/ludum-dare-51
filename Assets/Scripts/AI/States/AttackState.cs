using UnityEngine;

namespace AI.States
{
    public class AttackState : State
    {
        public AttackType CurrentAttackType => Random.Range(0, 3) == 0 ? AttackType.Light : AttackType.Heavy;

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