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
            Agent.SetDestination(Controller.Target);
            return this;
        }
    }
}