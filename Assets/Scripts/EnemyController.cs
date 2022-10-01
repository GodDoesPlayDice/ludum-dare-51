using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Character
{
    public static Character Player { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public Animator Anim { get; private set; }

    public bool IsAimedAtPlayer
    {
        get
        {
            if (Player == null)
                return false;
            var dirToPlayer = (Player.transform.position - transform.position).normalized;
            return false;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        Agent = GetComponent<NavMeshAgent>();
        Anim = GetComponentInChildren<Animator>();

        if (Player == null)
            Player = GameObject.FindWithTag("Player").GetComponent<Character>();

        Player.OnVelocityChange += _ => { CurrentTarget = Player.transform.position; };
    }
}