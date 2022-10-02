using System;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States
{
    public class ChaseState : State
    {
        // for calculations 
        private NavMeshPath _navMeshPath;
        private Vector3 _targetPos;

        public override State RunCurrentState()
        {
            Agent.speed = Controller.ChaseSpeed;
            _targetPos = Controller.TargetPosition +
                         (transform.position - Controller.TargetPosition).normalized *
                         (Controller.AttackDistance - .5f);

            Agent.SetDestination(_targetPos);
            // rotation towards player 
            // var targetRotation =
            //     Quaternion.LookRotation((Controller.TargetPosition - Agent.transform.position).normalized);
            // Agent.transform.rotation =
            //     Quaternion.Slerp(Agent.transform.rotation, targetRotation, 5f * Time.deltaTime);


            if (Controller.ShouldAttackTarget)
                return StateManager.AttackState;
            if (!Controller.ShouldChaseTarget)
                return StateManager.WanderState;

            return this;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_targetPos, 0.2f);
        }
#endif
    }
}