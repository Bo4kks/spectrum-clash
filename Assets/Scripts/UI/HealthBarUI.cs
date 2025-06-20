using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour, IEventListener
{
    private Image _healthBar;
    private PlayerStats _playerStats;

    private void Awake()
    {
        _healthBar = GetComponent<Image>();
        _playerStats = FindFirstObjectByType<PlayerStats>();
    }

    public void OnEnable()
    {
        EventBus.Subscribe<OnHPChanged>(RefillHealthBar);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnHPChanged>(RefillHealthBar);
    }

    private void RefillHealthBar(OnHPChanged @event)
    {
        float currentHealth = @event.HPValue;

        _healthBar.fillAmount = currentHealth / _playerStats.GetFloat(FloatStatType.MaxHealth);
    }
}
