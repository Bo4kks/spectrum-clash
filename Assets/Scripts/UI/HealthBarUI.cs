using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private Image _healthBar;

    private void Awake()
    {
        _healthBar = GetComponent<Image>();
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnUIHPChanged>(RefillHealthBar);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnUIHPChanged>(RefillHealthBar);
    }

    private void RefillHealthBar(OnUIHPChanged @event)
    {
        float currentHealth = @event.HPValue;

        _healthBar.fillAmount = currentHealth / 100f;
    }
}
