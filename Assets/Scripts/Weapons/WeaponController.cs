using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.Rendering.DebugUI;

public class WeaponController : MonoBehaviour
{
    public WeaponData data
    {
        get;
        private set;
    }
    public bool shooting = true;


    private float prevShootTime = 0f;
    //private Vector3 currentTarget; // PRIVATE
    //public Transform targetTMP;
    

    public void SetData(WeaponData data)
    {
        this.data = data;
        var main = GetComponent<ParticleSystem>().main;
        main.startColor = data.orbColor;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (prevShootTime + data.coolDown <= Time.time)
        {
            // TODO: Calculates every frame if not cooldown. Should be reworked!!!
            var target = GetTargetForShoot();
            if (target != null)
            {
                prevShootTime = Time.time;
                //Shoot(currentTarget);
                Shoot(target.transform.position);
            }
        }
    }

    private GameObject GetTargetForShoot()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            return null;
        }

        var closest = enemies[0];
        var closestDist = Vector3.Distance(transform.position, closest.transform.position);
        foreach (var enemy in enemies)
        {
            var dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < closestDist)
            {
                closest = enemy;
                closestDist = dist;
            }
        }
        return closestDist <= data.distance ? closest : null;
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
        var enemies = new HashSet<Enemy>();
        if (target.TryGetComponent<Enemy>(out var enemyCollided))
        {
            enemies.Add(enemyCollided);
        }
        
        if (data.aoeArea > 0)
        {
            var colliders = Physics.OverlapSphere(projectile.transform.position, data.aoeArea);
            foreach (var coll in colliders)
            {
                Enemy enemyInRadius = null;
                if (coll.gameObject.TryGetComponent<Enemy>(out enemyInRadius))
                {
                    enemies.Add(enemyInRadius);
                }
            }
        }
        foreach (var enemy in enemies)
        {
            enemy.Damage(data.damage);
        }
        
    }
}
