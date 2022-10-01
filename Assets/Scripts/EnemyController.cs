using AI;
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
    public float DistToStartChase { get; private set; } = 10f;

    [Tooltip("The player will be lost if distance to him is greater than distToSpotPlayer + this value")]
    [field: SerializeField]
    [field: Range(0, 20f)]
    public float DistToEndChase { get; private set; } = 5f;

    #endregion

    public static Character Player { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public Animator Anim { get; private set; }
    public StateManager StateManager { get; private set; }

    public bool IsLookingAtTarget
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

    public float DistToTarget => Vector3.Distance(transform.position, Target);

    public bool ShouldChaseTarget
    {
        get
        {
            if (StateManager == null)
                return false;
            var shouldChase = DistToTarget <= DistToStartChase &&
                              StateManager.CurrentState != StateManager.ChaseState ||
                              DistToTarget <= DistToStartChase + DistToEndChase &&
                              StateManager.CurrentState == StateManager.ChaseState;
            return shouldChase;
        }
    }

    public bool ShouldAttackTarget => DistToTarget <= AttackDistance && IsLookingAtTarget;

    protected override void Awake()
    {
        base.Awake();
        Agent = GetComponent<NavMeshAgent>();
        Anim = GetComponentInChildren<Animator>();
        StateManager = GetComponentInChildren<StateManager>();

        if (Player == null)
            Player = GameObject.FindWithTag("Player").GetComponent<Character>();

        // target for enemy is always player
        Target = Player.transform.position;
        Player.OnVelocityChange += _ => { Target = Player.transform.position; };
    }
}