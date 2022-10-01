using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace AI.States
{
    public class MoveState : State
    {
        #region Ispector

        [SerializeField] private float wanderDistance = 5f;
        [SerializeField] [Range(0f, 3f)] private float stayAtPointDuration = 1f;

        [SerializeField] private float wanderSpeed = 3.5f;
        [SerializeField] private float chaseSpeed = 5.5f;

        #endregion

        private bool _isGoingToRandomPoint;
        private NavMeshAgent _agent;
        private EnemyController _controller;
        private Character Player => EnemyController.Player;

        // for calculations 
        private NavMeshPath _navMeshPath;
        private float _stayAtPointTimer;

        private void Awake()
        {
            _agent = GetComponentInParent<NavMeshAgent>();
        }

        public override State RunCurrentState()
        {
            Wander();
            return this;
        }

        private void Wander()
        {
            if (!_isGoingToRandomPoint)
                GoToRandomPoint();
            if (_agent.remainingDistance <= 0.1f && _isGoingToRandomPoint)
            {
                _stayAtPointTimer += Time.deltaTime;
                if (_stayAtPointTimer > stayAtPointDuration)
                {
                    _stayAtPointTimer = 0f;
                    _isGoingToRandomPoint = false;
                }
            }
        }

        private void ChasePlayer()
        {
        }

        private void GoToRandomPoint()
        {
            _navMeshPath ??= new NavMeshPath();
            var position = transform.position;
            for (var i = 100f; i > 0; i--)
            {
                var offset = Random.insideUnitCircle * (wanderDistance + Random.Range(-2f, 2f));
                var randomPoint = position + new Vector3(offset.x, 0f, offset.y);
                _agent.CalculatePath(randomPoint, _navMeshPath);
                if (_navMeshPath.status == NavMeshPathStatus.PathComplete)
                {
                    _isGoingToRandomPoint = true;
                    _agent.SetDestination(randomPoint);
                    return;
                }
            }

            _isGoingToRandomPoint = false;
        }
    }
}