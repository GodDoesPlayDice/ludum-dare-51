using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float initCost = 2;
    public float costPerSecond = 1f;
    public float costChangeSpeed = 0.1f;
    public float initThreshold = 2;
    public float thresholdPerSecond = 0.1f;

    public int maxTries = 5;

    public EnemySpawnData[] enemies;

    [SerializeField] private float currentCost;
    [SerializeField] private float currentThreshold;

    private BoxCollider collider;

    private Dictionary<EnemySpawnData, List<GameObject>> enemiesPool = new Dictionary<EnemySpawnData, List<GameObject>>();

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
        currentCost = initCost;
        currentThreshold = initThreshold;
    }
    private void Update()
    {
        currentCost += Time.deltaTime * costPerSecond;
        costPerSecond += Time.deltaTime * costChangeSpeed;
        currentThreshold += Time.deltaTime * thresholdPerSecond;

        if (CheckIfNeedSpawn())
        {
            Spawn();
        }
    }

    private bool CheckIfNeedSpawn()
    {
        return currentCost > currentThreshold;
    }

    private void Spawn()
    {
        int iter = 0;
        while (currentCost >= 1 && iter < maxTries) {
            maxTries++;

            var data = ChooseEnemyData();

            var enemy = GetGameObjectByData(data);

            var fullySet = PrepareAndSetEnemy(enemy);
            if (fullySet)
            {
                currentCost -= data.cost;
            }
        }
    }

    private EnemySpawnData ChooseEnemyData()
    {
        var maxCostEnemy = enemies[0];
        for (int i = 0; i < 3; i ++)
        {
            
            var curEnemy = enemies[(int) Random.Range(0f, enemies.Length)];
            if (curEnemy.cost >= maxCostEnemy.cost && currentCost >= curEnemy.cost)
            {
                maxCostEnemy = curEnemy;
            }
        }
        return maxCostEnemy;
    }

    private GameObject GetGameObjectByData(EnemySpawnData data)
    {
        GameObject result = null;
        enemiesPool.TryGetValue(data, out var list);
        if (list == null || list.Count == 0)
        {
            result = Instantiate(data.prefab, new Vector3(0f, -10f, 0f), Quaternion.identity, transform);
            result.GetComponent<Death>().OnReadyToPool += obj => BackToPool(data, obj);
        } else
        {
            result = list[0];
            list.RemoveAt(0);
        }

        return result;
    }

    private bool PrepareAndSetEnemy(GameObject enemy)
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var position = Vector3.zero;
        for (int i = 0; i < maxTries; i ++)
        {
            var bnds = collider.bounds;
            var x = Random.Range(bnds.min.x, bnds.max.x);
            var z = Random.Range(bnds.min.z, bnds.max.z);
            var curPos = new Vector3(x, 0.4f, z);
            if (enemies.Any(e => Vector3.Distance(curPos, e.transform.position) < 1))
            {
            } else
            {
                position = curPos;
                break;
            }
        }
        if (position != Vector3.zero)
        {
            var controller = enemy.GetComponent<Enemy>();
            controller.Heal(controller.MaxHealth);
            controller.transform.position = position;
            return true;
        } else
        {
            return false;
        }
    }

    private void BackToPool(EnemySpawnData data, GameObject instance)
    {
        if (!enemiesPool.ContainsKey(data))
        {
            enemiesPool[data] = new List<GameObject>();
        }
        enemiesPool[data].Add(instance);
    }
}

public enum EnemyTypes
{
    Snake,
    
}