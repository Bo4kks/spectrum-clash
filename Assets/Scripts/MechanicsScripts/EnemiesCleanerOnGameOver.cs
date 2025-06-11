using UnityEngine;

public class EnemiesCleanerOnGameOver : MonoBehaviour, IEventListener
{
    public void OnEnable()
    {
        EventBus.Subscribe<OnGameOverEvent>(OnGameOver);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnGameOverEvent>(OnGameOver);
    }

    private void OnGameOver(OnGameOverEvent evt)
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        foreach (var enemy in enemies)
        {
            enemy.Kill();
        }
    }
}