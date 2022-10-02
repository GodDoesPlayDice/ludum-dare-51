using System;
using AI;
using UnityEngine;

public class EnemyAnimations : CharacterAnimations
{
    private StateManager _stateManager;
    private float _targetSpeed;

    private static readonly int LightAttack = Animator.StringToHash("LightAttack");
    private static readonly int Speed = Animator.StringToHash("Speed");

    protected override void Awake()
    {
        base.Awake();
        _stateManager = GetComponentInChildren<StateManager>();
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
            Animator.SetTrigger(LightAttack);
        if (newState == _stateManager.ChaseState)
            _targetSpeed = 6f;
        if (newState == _stateManager.WanderState)
            _targetSpeed = 2f;
        if (newState == _stateManager.IdleState)
            _targetSpeed = 0f;
    }
}