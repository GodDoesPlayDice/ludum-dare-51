using System;
using AI.States;
using UnityEngine;

public class EnemyAnimationEventsReceiver : MonoBehaviour
{
    private EnemyController _controller;
    private AttackState _attackState;

    private void Awake()
    {
        _controller = GetComponentInParent<EnemyController>();
        _attackState = _controller.GetComponentInChildren<AttackState>();
    }

    public void DealAttackDamage()
    {
        if (EnemyController.Player == null)
            return;
        if (!_controller.CanDealDamage)
            return;

        var damage = _attackState.CurrentAttackType == AttackType.Light
            ? _controller.LightAttackDamage
            : _controller.HeavyAttackDamage;
        EnemyController.Player.Damage(damage);
    }

    public void AttackEnded()
    {
        _controller.lastAttackTime = Time.time;
    }
}