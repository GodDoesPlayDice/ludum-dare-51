using System;
using StarterAssets;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public float MaxStamina
    {
        get => maxStamina;
        set
        {
            if (Mathf.Approximately(value, maxStamina))
                OnMaxStaminaChange?.Invoke(value);
            maxStamina = value;
        }
    }

    public float CurrentStamina
    {
        get => _currentStamina;
        set
        {
            var clamped = Mathf.Clamp(value, 0f, maxStamina);
            if (Mathf.Approximately(clamped, _currentStamina))
                OnCurrentStaminaChange?.Invoke(clamped);
            _currentStamina = clamped;
        }
    }

    [SerializeField] private float maxStamina = 100f;
    private float _currentStamina;

    private void Awake()
    {
        CurrentStamina = maxStamina;
    }

    public event Action<float> OnMaxStaminaChange;
    public event Action<float> OnCurrentStaminaChange;
}