using StarterAssets;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [Range(0, 300f)] public float maxStamina = 100f;
    public float currentStamina { get; private set; }

    private float _maxStamina;
    private float _currentStamina;

    
    private ThirdPersonController _tps;
}