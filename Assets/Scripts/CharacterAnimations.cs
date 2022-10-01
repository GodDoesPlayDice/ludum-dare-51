using System;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private Character _character;
    private Animator _animator;


    private float _prevHealth;


    private static readonly int ReceivedDamage = Animator.StringToHash("ReceivedDamage");

    private void Awake()
    {
        _character = GetComponent<Character>();
        _animator = GetComponent<Animator>();
        _prevHealth = _character.Health;

        _character.OnHealthChange += health =>
        {
            if (health < _prevHealth)
                _animator.SetTrigger(ReceivedDamage);

            _prevHealth = _character.Health;
        };
    }
}