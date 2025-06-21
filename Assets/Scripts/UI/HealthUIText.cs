using System;
using TMPro;
using UnityEngine;

public class HealthUIText : MonoBehaviour, IEventListener
{
    private TextMeshProUGUI _healthText;

    private void Awake()
    {
        _healthText = GetComponent<TextMeshProUGUI>();
    }

    public void OnEnable()
    {
        EventBus.Subscribe<OnHPChanged>(ChangeHpValue);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnHPChanged>(ChangeHpValue);
    }

    private void ChangeHpValue(OnHPChanged @event)
    {
        _healthText.text = @event.HPValue.ToString("#");
    }
}
