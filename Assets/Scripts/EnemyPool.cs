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

            for (int i = 0; i < _enemyPrefabs.Length; i++)
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
            Debug.LogError($"[EnemyPool] No prefab registered for data: {data.name}");
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
        enemyObj.SetActive(false);

        if (_pools.ContainsKey(data) && enemyObj.TryGetComponent(out Enemy enemy))
        {
            _pools[data].Enqueue(enemy);
        }
        else
        {
            Destroy(enemyObj); // fallback
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
