using System;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private Character _character;
    private Animator _animator;


    private float _prevHealth;


    private static readonly int ReceivedDamage = Animator.StringToHash("ReceivedDamage");
    private static readonly int Death = Animator.StringToHash("Death");
    private static readonly int Revive = Animator.StringToHash("Revive");

    private void Awake()
    {
        _character = GetComponent<Character>();
        _animator = GetComponent<Animator>();
        _prevHealth = _character.Health;
    }

    private void Start()
    {
        _character.OnHealthChange += health =>
        {
            if (health < _prevHealth)
                _animator.SetTrigger(ReceivedDamage);

            _prevHealth = _character.Health;
        };

        _character.OnIsAliveChange += isAlive => { _animator.SetTrigger(!isAlive ? Death : Revive); };
    }
}