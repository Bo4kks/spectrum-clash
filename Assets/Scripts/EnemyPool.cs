using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemyPrefabs;

    private Dictionary<EnemyDataSO, Queue<Enemy>> _pools = new();

    private void Awake()
    {
        foreach (var prefab in _enemyPrefabs)
        {
            var data = prefab.EnemyData;

            if (!_pools.ContainsKey(data))
                _pools[data] = new Queue<Enemy>();

            for (int i = 0; i < 5; i++)
            {
                var enemy = Instantiate(prefab, transform);
                enemy.gameObject.SetActive(false);
                enemy.SetPool(this);
                _pools[data].Enqueue(enemy);
            }
        }
    }

    public Enemy SpawnEnemy(Vector3 position, EnemyDataSO data)
    {
        if (!_pools.ContainsKey(data))
        {
            Debug.LogError($"[EnemyPool] No pool found for: {data.name}");
            return null;
        }

        Enemy enemy;

        if (_pools[data].Count > 0)
        {
            enemy = _pools[data].Dequeue();
        }
        else
        {
            var prefab = GetPrefabByData(data);
            if (prefab == null)
            {
                Debug.LogError($"[EnemyPool] No prefab found for data: {data.name}");
                return null;
            }

            enemy = Instantiate(prefab, transform);
            enemy.SetPool(this);
        }

        enemy.transform.position = position;
        enemy.gameObject.SetActive(true);
        enemy.Initialize(data);

        return enemy;
    }

    public void ReturnToPool(GameObject enemyObj, EnemyDataSO data)
    {
        if (enemyObj.TryGetComponent(out Enemy enemy))
        {
            enemy.gameObject.SetActive(false);

            if (_pools.ContainsKey(data))
            {
                _pools[data].Enqueue(enemy);
            }
            else
            {
                Destroy(enemy.gameObject);
                Debug.LogWarning($"[EnemyPool] Tried to return to unknown pool: {data.name}");
            }
        }
        else
        {
            Destroy(enemyObj);
        }
    }

    private Enemy GetPrefabByData(EnemyDataSO data)
    {
        foreach (var prefab in _enemyPrefabs)
        {
            if (prefab.EnemyData == data)
                return prefab;
        }

        return null;
    }
}
