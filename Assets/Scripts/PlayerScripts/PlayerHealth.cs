using UnityEngine;

public class PlayerHealth : MonoBehaviour, IEventListener, IEventPusher
{
    [SerializeField] private float _health;

    [Header("BugFix&Test")]
    [SerializeField] private bool _isCanDie = true;

    public float Health
    {
        get
        {
            return _health;
        }
        private set
        {
            if (value < 0f)
            {
                _health = 0f;
            }
            else
            {
                _health = value;
            }

            EventBus.Invoke(new OnUIHPChanged(_health));

            if (_health <= 0 && _isCanDie)
            {
                GameOver();
            }
        }
    }

    public void OnEnable()
    {
        EventBus.Subscribe<OnEnemyHitPlayerEvent>(TakeDamage);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnEnemyHitPlayerEvent>(TakeDamage);
    }

    private void TakeDamage(OnEnemyHitPlayerEvent @event)
    {
        Health -= @event.Damage;
    }

    private void GameOver() => EventBus.Invoke(new OnGameOverEvent());
}
