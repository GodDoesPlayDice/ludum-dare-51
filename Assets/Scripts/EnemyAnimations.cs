public class EnemyAnimations : CharacterAnimations
{
    private EnemyController _enemyController;

    protected override void Awake()
    {
        base.Awake();
        _enemyController = GetComponent<EnemyController>();
    }

    private void Update()
    {
    }
}