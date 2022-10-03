using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float cycleDuration = 10f;
    private int _cycle;
    public float fullTime;
    private float _currentCycleTime;
    public float startTime = 0f;

    public int Cycle
    {
        get => _cycle;
        set {
            if (value != _cycle) {
                OnCycleChange?.Invoke(value);
                _cycle = value;
            }
        }
    }

    public event Action<int> OnCycleChange;
    public event Action<float> OnCurrentCycleTimeChange;


    void Start()
    {
        fullTime = 0f;
    }

    void Update()
    {
        fullTime += Time.deltaTime;
        _currentCycleTime = fullTime % cycleDuration;
        OnCurrentCycleTimeChange?.Invoke(_currentCycleTime);
        Cycle = (int)(fullTime / cycleDuration);
    }
}
