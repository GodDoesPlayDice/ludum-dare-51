using System;
using AI.States;
using UnityEngine;

public class EnemyAnimationEventsReceiver : MonoBehaviour
{
    private Enemy _controller;
    private AttackState _attackState;

    private void Awake()
    {
        _controller = GetComponentInParent<Enemy>();
        _attackState = _controller.GetComponentInChildren<AttackState>(); 
    }

    public void DealAttackDamage()
    {
        if (Enemy.Player == null)
            return;
        if (!_controller.CanDealDamage)
            return;

        var damage = _attackState.CurrentAttackType == AttackType.Light
            ? _controller.LightAttackDamage
            : _controller.HeavyAttackDamage;
        Enemy.Player.Damage(damage);
    }

    public void AttackEnded()
    {
        _controller.lastAttackTime = Time.time;
    }
}