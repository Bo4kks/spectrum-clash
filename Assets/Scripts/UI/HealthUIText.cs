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
        EventBus.Subscribe<OnUIHPChanged>(ChangeHpValue);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnUIHPChanged>(ChangeHpValue);
    }

    private void ChangeHpValue(OnUIHPChanged @event)
    {
        _healthText.text = @event.HPValue.ToString();
    }
}
