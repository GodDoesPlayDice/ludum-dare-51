using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace AI.States
{
    public class WanderState : State
    {
        private bool _isGoingToRandomPoint;

        // for calculations 
        private NavMeshPath _navMeshPath;
        private float _stayAtPointTimer;

        public override State RunCurrentState()
        {
            if (Controller.ShouldAttackTarget)
                return StateManager.AttackState;

            if (Controller.ShouldChaseTarget)
                return StateManager.ChaseState;

            if (Agent.remainingDistance <= 0.1f && _isGoingToRandomPoint)
            {
                _isGoingToRandomPoint = false;
                return StateManager.IdleState;
            }

            if (!_isGoingToRandomPoint)
                GoToRandomPoint();
            return this;
        }

        private void GoToRandomPoint()
        {
            _navMeshPath ??= new NavMeshPath();
            var position = transform.position;
            for (var i = 100f; i > 0; i--)
            {
                var offset = Random.insideUnitCircle * (Controller.WanderDistance + Random.Range(-2f, 2f));
                var randomPoint = position + new Vector3(offset.x, 0f, offset.y);
                Agent.CalculatePath(randomPoint, _navMeshPath);
                if (_navMeshPath.status == NavMeshPathStatus.PathComplete)
                {
                    _isGoingToRandomPoint = true;
                    Agent.speed = Controller.WanderSpeed;
                    Agent.SetDestination(randomPoint);
                    return;
                }
            }

            _isGoingToRandomPoint = false;
        }
    }
}