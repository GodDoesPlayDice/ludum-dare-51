using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponData data;
    public bool shooting = true;


    private float prevShootTime = 0f;
    //private Vector3 currentTarget; // PRIVATE
    public Transform targetTMP;
    

    void Start()
    {
        
    }

    void Update()
    {
        if (prevShootTime + data.coolDown <= Time.time)
        {
            prevShootTime = Time.time;
            //Shoot(currentTarget);
            Shoot(targetTMP.position);
        }
    }

    public void Shoot(Vector3 target)
    {
        var rotation = Quaternion.LookRotation((target - transform.position).normalized);
        var projectile = Instantiate(data.prefab, transform.position, rotation, transform);
        if (projectile.TryGetComponent<ParticleCollisionInstance>(out var coll))
        {
            coll.weaponController = this;
        }
    }

    public void OnProjectileCollide(GameObject projectile, GameObject target)
    {
        var enemies = new HashSet<EnemyController>();
        var enemy = target.GetComponent<EnemyController>();
        enemies.Add(enemy);
        if (data.aoeArea > 0)
        {
            var colliders = Physics.OverlapSphere(projectile.transform.position, data.aoeArea);
            foreach(var coll in colliders)
            {
                EnemyController enemyInRadius = null;
                if (coll.gameObject.TryGetComponent<EnemyController>(out enemyInRadius))
                {
                    enemies.Add(enemyInRadius);
                }
            }
        }
        
    }
}
