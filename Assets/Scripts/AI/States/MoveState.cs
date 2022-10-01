using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace AI.States
{
    public class MoveState : State
    {
        #region Ispector

        #endregion

        private bool _isGoingToRandomPoint;
        private bool _isChasing;

        // for calculations 
        private NavMeshPath _navMeshPath;
        private float _stayAtPointTimer;

        public override State RunCurrentState()
        {
            var distToPlayer = Vector3.Distance(transform.position, Controller.Target);
            var shouldAttack = distToPlayer <= Controller.AttackDistance && Controller.IsAimedAtPlayer;
            if (shouldAttack)
                return StateManager.AttackState;

            var shouldChase = distToPlayer <= Controller.DistToSpotPlayer && !_isChasing ||
                              distToPlayer <= Controller.DistToSpotPlayer + Controller.DistToLoosePlayer && _isChasing;
            if (shouldChase)
            {
                ChaseTarget();
                return this;
            }

            Wander();
            return this;
        }

        private void Wander()
        {
            if (!_isGoingToRandomPoint)
                GoToRandomPoint();
            if (Agent.remainingDistance <= 0.1f && _isGoingToRandomPoint)
            {
                _stayAtPointTimer += Time.deltaTime;
                if (_stayAtPointTimer > 1f)
                {
                    _stayAtPointTimer = 0f;
                    _isGoingToRandomPoint = false;
                }
            }
        }

        private void ChaseTarget()
        {
            Agent.speed = Controller.ChaseSpeed;
            Agent.SetDestination(Controller.Target);
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