using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float MaxHealth
    {
        get => maxHealth;
        set
        {
            if (!Mathf.Approximately(value, maxHealth))
                OnMaxHealthChange?.Invoke(value);
            maxHealth = value;
        }
    }


    #region State

    public bool IsAlive
    {
        get => _isAlive;
        protected set
        {
            if (value != _isAlive)
                OnIsAliveChange?.Invoke(value);
            _isAlive = value;
        }
    }

    public float Health
    {
        get => _health;
        private set
        {
            if (!Mathf.Approximately(value, _health))
                OnHealthChange?.Invoke(value);
            _health = value;
        }
    }

    public Vector3 Velocity
    {
        get => _velocity;
        set
        {
            OnVelocityChange?.Invoke(value);
            _velocity = value;
        }
    }

    public Vector3 TargetPosition
    {
        get => _targetPosition;
        set
        {
            OnCurrentTargetChange?.Invoke(value);
            _targetPosition = value;
        }
    }

    // private backing fields
    private bool _isAlive = true;
    [SerializeField] private float maxHealth;
    private float _health;
    private Vector3 _velocity;
    private Vector3 _targetPosition;

    #endregion

    #region StateEvents

    public event Action<Vector3> OnVelocityChange;
    public event Action<Vector3> OnCurrentTargetChange;
    public event Action<float> OnHealthChange;
    public event Action<float> OnMaxHealthChange;
    public event Action<bool> OnIsAliveChange;

    #endregion

    protected virtual void Awake()
    {
        Health = MaxHealth;
    }

    public void Damage(float amount)
    {
        Health = Mathf.Clamp(Health - Mathf.Abs(amount), 0f, MaxHealth);
        if (Health <= 0)
            IsAlive = false;
    }

    public void Heal(float amount)
    {
        Health = Mathf.Clamp(Health + Mathf.Abs(amount), 0f, MaxHealth);
        if (Health > 0)
            IsAlive = true;
    }
}