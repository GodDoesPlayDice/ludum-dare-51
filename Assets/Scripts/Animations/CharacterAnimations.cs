using System;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    protected Character Character;
    protected Animator Animator;


    private float _prevHealth;
    private bool _alreadyDead;
    private static readonly int ReceivedDamage = Animator.StringToHash("ReceivedDamage");
    private static readonly int Death = Animator.StringToHash("Death");
    private static readonly int Revive = Animator.StringToHash("Revive");

    protected virtual void Awake()
    {
        Character = GetComponent<Character>();
        Character.gameObject.TryGetComponent(out Animator);
        Animator ??= Character.gameObject.GetComponentInChildren<Animator>();
        _prevHealth = Character.Health;
    }

    protected void Start()
    {
        Character.OnHealthChange += health =>
        {
            if (!Character.IsAlive)
                return;
            if (health < _prevHealth)
                Animator.SetTrigger(ReceivedDamage);

            _prevHealth = Character.Health;
        };

        Character.OnIsAliveChange += isAlive => { Animator.SetTrigger(!isAlive ? Death : Revive); };
    }
}