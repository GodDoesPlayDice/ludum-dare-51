using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region Inspector

    public float maxHealth = 100f;

    #endregion

    #region State

    public bool IsAlive
    {
        get => _isAlive;
        private set
        {
            OnIsAliveChange?.Invoke(value);
            _isAlive = value;
        }
    }

    public float Health
    {
        get => _health;
        private set
        {
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
    private float _health;
    private Vector3 _velocity;
    private Vector3 _targetPosition;

    #endregion

    #region StateEvents

    public event Action<Vector3> OnVelocityChange;
    public event Action<Vector3> OnCurrentTargetChange;
    public event Action<float> OnHealthChange;
    public event Action<bool> OnIsAliveChange;

    #endregion

    protected  virtual void Awake()
    {
        Health = maxHealth;
    }

    public void Damage(float amount)
    {
        Health = Mathf.Clamp(Health - Mathf.Abs(amount), 0f, maxHealth);
        if (Health <= 0)
            IsAlive = false;
    }

    public void Heal(float amount)
    {
        Health = Mathf.Clamp(Health + Mathf.Abs(amount), 0f, maxHealth);
        if (Health > 0)
            IsAlive = true;
    }
}