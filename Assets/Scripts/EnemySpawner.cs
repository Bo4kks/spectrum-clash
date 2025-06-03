using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;

    [SerializeField] private List<EnemyDataSO> _enemyData;

    [SerializeField] private EnemyPool _enemyPool;

    [SerializeField] private float _spawnInterval;

    private void Start()
    {
        StartCoroutine(nameof(SpawnEnemiesRoutine));
    }

    private void SpawnEnemy()
    {
        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];

        _enemyPool.SpawnEnemy(spawnPoint.position, _enemyData[Random.Range(0, _enemyData.Count)]); // test
    }


    private IEnumerator SpawnEnemiesRoutine()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(_spawnInterval);
        }
    }
}
