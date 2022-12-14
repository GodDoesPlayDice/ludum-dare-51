using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States
{
    public class ChaseState : State
    {
        // for calculations 
        private NavMeshPath _navMeshPath;
        private Vector3 _targetPos;
        private Quaternion _targetRotation;

        public override State RunCurrentState()
        {
            if (!Controller.IsAlive)
                return StateManager.IdleState;

            Agent.speed = Controller.ChaseSpeed;
            _targetPos = Controller.TargetPosition +
                         (transform.position - Controller.TargetPosition).normalized *
                         (Controller.AttackDistance - .5f);

            Agent.SetDestination(_targetPos);
            // rotation towards player 
            if (Controller.DistToTarget < Controller.AttackDistance && Controller.IsAlive)
            {
                _targetRotation =
                    Quaternion.LookRotation((Controller.TargetPosition - Agent.transform.position).normalized);
                Agent.transform.rotation =
                    Quaternion.Slerp(Agent.transform.rotation, _targetRotation, 10f * Time.deltaTime);
            }


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