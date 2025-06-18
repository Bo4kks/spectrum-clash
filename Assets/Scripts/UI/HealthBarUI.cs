using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour, IEventListener
{
    private Image _healthBar;

    private void Awake()
    {
        _healthBar = GetComponent<Image>();
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

        _healthBar.fillAmount = currentHealth / 100f;
    }
}
