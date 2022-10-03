using AI;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    #region Inspector

    [field: SerializeField]
    [field: Range(0, 50f)]
    public float LightAttackDamage { get; private set; } = 5f;

    [field: SerializeField]
    [field: Range(0, 50f)]
    public float HeavyAttackDamage { get; private set; } = 10f;

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
    [field: Range(0, 5f)]
    public float AttackCooldown { get; private set; } = 2f;

    [field: SerializeField]
    [field: Range(0, 30f)]
    public float DistToStartChase { get; private set; } = 10f;

    [Tooltip("The player will be lost if distance to him is greater than distToSpotPlayer + this value")]
    [field: SerializeField]
    [field: Range(0, 20f)]
    public float DistToEndChase { get; private set; } = 5f;

    #endregion

    #region PublicFields

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
            var dirToPlayer = (TargetPosition - transform.position).normalized;
            var dot = Vector3.Dot(transform.forward, dirToPlayer);
            return dot > 0.6f;
        }
    }

    public float DistToTarget => Vector3.Distance(transform.position, TargetPosition);

    public bool ShouldChaseTarget
    {
        get
        {
            if (StateManager == null)
                return false;
            var shouldChase = Player != null && Player.IsAlive &&
                              (DistToTarget <= DistToStartChase &&
                               StateManager.CurrentState != StateManager.ChaseState ||
                               DistToTarget <= DistToStartChase + DistToEndChase &&
                               StateManager.CurrentState == StateManager.ChaseState);
            return shouldChase;
        }
    }

    public bool CanDealDamage
    {
        get
        {
            var result =
                DistToTarget <= AttackDistance
                && IsLookingAtTarget
                && Player != null && Player.IsAlive;
            return result;
        }
    }

    public bool ShouldAttackTarget
    {
        get
        {
            var result = CanDealDamage && Time.time - lastAttackTime > AttackCooldown;
            return result;
        }
    }


    public float lastAttackTime;

    #endregion


    protected override void Awake()
    {
        base.Awake();
        Agent = GetComponent<NavMeshAgent>();
        Anim = GetComponentInChildren<Animator>();
        StateManager = GetComponentInChildren<StateManager>();

        if (Player == null)
            Player = GameObject.FindWithTag("Player").GetComponent<Character>();

        // target for enemy is always player
        TargetPosition = Player.transform.position;
        Player.OnVelocityChange += _ => { TargetPosition = Player.transform.position; };

        OnIsAliveChange += isAlive =>
        {
            if (!isAlive)
            {
                Agent.isStopped = true;
                Agent.enabled = false;
            }
            else if (!IsAlive)
            {
                // revive
                Agent.isStopped = false;
                Agent.enabled = true;
            }
        };
    }
}