using UnityEngine;

namespace AI.States
{
    public class AttackState : State
    {
        public override State RunCurrentState()
        {
            if (EnemyController.Player == null)
                return StateManager.ChaseState;
            var player = EnemyController.Player;
            player.Damage(Controller.LightAttackDamage);
            Debug.Log(player.Health);
            return StateManager.ChaseState;
        }
    }
}