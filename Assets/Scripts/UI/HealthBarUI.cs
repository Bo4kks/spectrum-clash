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
        EventBus.Subscribe<OnUIHPChanged>(RefillHealthBar);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnUIHPChanged>(RefillHealthBar);
    }

    private void RefillHealthBar(OnUIHPChanged @event)
    {
        float currentHealth = @event.HPValue;

        _healthBar.fillAmount = currentHealth / 100f;
    }
}
