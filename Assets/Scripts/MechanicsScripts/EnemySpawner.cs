using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawner : MonoBehaviour, IEventListener
{
    [SerializeField] private List<Transform> _spawnPoints;

    [SerializeField] private List<EnemyDataSO> _enemyData;

    [SerializeField] private EnemyPool _enemyPool;

    [SerializeField] private float _spawnInterval;

    private void Start()
    {
        StartCoroutine(nameof(SpawnEnemiesRoutine));
    }

    public void OnEnable()
    {
        EventBus.Subscribe<OnGameOverEvent>(GameOver);
        EventBus.Subscribe<OnGameRestartEvent>(GameRestart);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnGameOverEvent>(GameOver);
        EventBus.Unsubscribe<OnGameRestartEvent>(GameRestart);
    }

    private void SpawnEnemy()
    {
        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];

        _enemyPool.SpawnEnemy(spawnPoint.position, _enemyData[Random.Range(0, _enemyData.Count)]); // test
    }

    private void GameOver(OnGameOverEvent @event) => StopCoroutine(nameof(SpawnEnemiesRoutine));

    private void GameRestart(OnGameRestartEvent @event) => StartCoroutine(nameof(SpawnEnemiesRoutine));

    private IEnumerator SpawnEnemiesRoutine()
    {
        while (true)
        {
            SpawnEnemy();
            Debug.Log("Spawn Enemy!");
            yield return new WaitForSeconds(_spawnInterval);
        }
    }


}
