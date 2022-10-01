using System;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    protected Character Character;
    protected Animator Animator;


    private float _prevHealth;
    private static readonly int ReceivedDamage = Animator.StringToHash("ReceivedDamage");
    private static readonly int Death = Animator.StringToHash("Death");
    private static readonly int Revive = Animator.StringToHash("Revive");

    protected virtual void Awake()
    {
        Character = GetComponent<Character>();
        Animator = GetComponent<Animator>();
        _prevHealth = Character.Health;
    }

    private void Start()
    {
        Character.OnHealthChange += health =>
        {
            if (health < _prevHealth)
                Animator.SetTrigger(ReceivedDamage);

            _prevHealth = Character.Health;
        };

        Character.OnIsAliveChange += isAlive => { Animator.SetTrigger(!isAlive ? Death : Revive); };
    }
}