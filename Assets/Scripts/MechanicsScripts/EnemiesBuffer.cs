using System.Collections;
using UnityEngine;

public class EnemiesBuffer : MonoBehaviour, IEventPusher
{
    private LevelController _levelController;

    private void Awake()
    {
        _levelController = FindFirstObjectByType<LevelController>();
    }

    private void Start()
    {
        StartCoroutine(nameof(BuffEnemiesRoutine));
    }

    private IEnumerator BuffEnemiesRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_levelController.TimeIntervalToBuffEnemies);

            EventBus.Invoke(new OnTimeToBuffEnemies());

            Debug.Log("Enemies buffed!");
        }
    }
}
