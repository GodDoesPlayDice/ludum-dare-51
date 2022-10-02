using UnityEngine.AI;

namespace AI.States
{
    public class ChaseState : State
    {
        // for calculations 
        private NavMeshPath _navMeshPath;

        public override State RunCurrentState()
        {
            if (Controller.ShouldAttackTarget)
                return StateManager.AttackState;
            if (!Controller.ShouldChaseTarget)
                return StateManager.WanderState;

            Agent.speed = Controller.ChaseSpeed;
            var targetPos = Controller.TargetPosition +
                            (transform.position - Controller.TargetPosition).normalized; 
            Agent.SetDestination(targetPos);
            return this;
        }
    }
}