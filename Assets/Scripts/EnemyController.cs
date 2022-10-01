using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Character
{
    #region Inspector

    [field: SerializeField]
    [field: Range(0, 30f)]
    public float WanderDistance { get; private set; } = 5f;

    [field: SerializeField]
    [field: Range(0, 6f)]
    public float WanderSpeed { get; private set; } = 2.5f;

    [field: SerializeField]
    [field: Range(0, 10f)]
    public float ChaseSpeed { get; private set; } = 4f;

    [field: SerializeField]
    [field: Range(0, 30f)]
    public float AttackDistance { get; private set; } = 2f;

    [field: SerializeField]
    [field: Range(0, 30f)]
    public float DistToSpotPlayer { get; private set; } = 10f;

    [Tooltip("The player will be lost if distance to him is greater than distToSpotPlayer + this value")]
    [field: SerializeField]
    [field: Range(0, 20f)]
    public float DistToLoosePlayer { get; private set; } = 5f;

    #endregion

    public static Character Player { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public Animator Anim { get; private set; }

    public bool IsAimedAtPlayer
    {
        get
        {
            if (Player == null)
                return false;
            var dirToPlayer = (Target - transform.position).normalized;
            var dot = Vector3.Dot(transform.forward, dirToPlayer);
            return dot > 0.5f;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        Agent = GetComponent<NavMeshAgent>();
        Anim = GetComponentInChildren<Animator>();

        if (Player == null)
            Player = GameObject.FindWithTag("Player").GetComponent<Character>();

        // target for enemy is always player
        Target = Player.transform.position;
        Player.OnVelocityChange += _ => { Target = Player.transform.position; };
    }
}