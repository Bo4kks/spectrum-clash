using System.Collections;
using UnityEngine;

public class EnemiesBuffer : MonoBehaviour, IEventPusher, IEventListener
{
    private LevelController _levelController;

    private void Awake()
    {
        _levelController = FindFirstObjectByType<LevelController>();
    }

    public void OnEnable()
    {
        EventBus.Subscribe<OnGameOverEvent>(StopCoroutine);
        EventBus.Subscribe<OnGameRestartEvent>(StartCoroutine);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnGameOverEvent>(StopCoroutine);
        EventBus.Unsubscribe<OnGameRestartEvent>(StartCoroutine);
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

    private void StartCoroutine(OnGameRestartEvent ev) => StartCoroutine(nameof(BuffEnemiesRoutine));

    private void StopCoroutine(OnGameOverEvent ev) => StopCoroutine(nameof(BuffEnemiesRoutine));
}
