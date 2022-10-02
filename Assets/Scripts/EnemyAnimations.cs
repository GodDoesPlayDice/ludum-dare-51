using System;
using AI;
using AI.States;
using UnityEngine;

public class EnemyAnimations : CharacterAnimations
{
    private StateManager _stateManager;
    private AttackState _attackState;
    private float _targetSpeed;

    private static readonly int LightAttack = Animator.StringToHash("LightAttack");
    private static readonly int HeavyAttack = Animator.StringToHash("HeavyAttack");
    private static readonly int Speed = Animator.StringToHash("Speed");

    protected override void Awake()
    {
        base.Awake();
        _stateManager = GetComponentInChildren<StateManager>();
        _attackState = _stateManager.GetComponentInChildren<AttackState>();
        _stateManager.OnStateChange += OnStateChange;
    }

    private void Update()
    {
        var current = Animator.GetFloat(Speed);
        Animator.SetFloat(Speed, Mathf.Lerp(current, _targetSpeed, 0.3f * Time.deltaTime));
    }

    private void OnStateChange(State newState)
    {
        if (newState == _stateManager.AttackState)
            Animator.SetTrigger(_attackState.CurrentAttackType == AttackType.Light ? LightAttack : HeavyAttack);
        if (newState == _stateManager.ChaseState)
            _targetSpeed = 6f;
        if (newState == _stateManager.WanderState)
            _targetSpeed = 2f;
        if (newState == _stateManager.IdleState)
            _targetSpeed = 0f;
    }
}