using UnityEngine;

public class PlayerHealth : MonoBehaviour, IEventListener, IEventPusher
{
    [SerializeField] private float _currentHealth;

    [Header("BugFix&Test")]
    [SerializeField] private bool _isCanDie = true;

    private PlayerStats _playerStats;

    public float CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        private set
        {
            if (value < 0f)
            {
                _currentHealth = 0f;
            }
            else
            {
                _currentHealth = value;
            }

            EventBus.Invoke(new OnHPChanged(_currentHealth));

            if (_currentHealth <= 0 && _isCanDie)
            {
                GameOver();
            }
        }
    }

    private void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
    }

    private void Start()
    {
        RefillHealth();
    }

    public void OnEnable()
    {
        EventBus.Subscribe<OnEnemyHitPlayerEvent>(TakeDamage);
        EventBus.Subscribe<OnGameRestartEvent>(UpdateFields);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnEnemyHitPlayerEvent>(TakeDamage);
        EventBus.Unsubscribe<OnGameRestartEvent>(UpdateFields);
    }

    private void TakeDamage(OnEnemyHitPlayerEvent @event)
    {
        CurrentHealth -= @event.Damage;
    }

    private void RefillHealth() => CurrentHealth = _playerStats.GetFloat(FloatStatType.MaxHealth);

    private void GameOver() => EventBus.Invoke(new OnGameOverEvent());

    private void UpdateFields(OnGameRestartEvent @event) => RefillHealth();
}
