using UnityEngine;

public class PlayerHealth : MonoBehaviour, IEventListener
{
    [SerializeField] private float _health;

    public float Health
    {
        get
        {
            return _health;
        }
        private set
        {
            _health = value;
            EventBus.Invoke(new OnUIHPChanged(_health));
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
        Debug.Log($"Take damage - {@event.Damage}");
    }
}
