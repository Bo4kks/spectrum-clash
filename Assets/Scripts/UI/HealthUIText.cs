using System;
using TMPro;
using UnityEngine;

public class HealthUIText : MonoBehaviour
{
    private TextMeshProUGUI _healthText;

    private void Awake()
    {
        _healthText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnUIHPChanged>(ChangeHpValue);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnUIHPChanged>(ChangeHpValue);
    }

    private void ChangeHpValue(OnUIHPChanged @event)
    {
        _healthText.text = @event.HPValue.ToString();
    }
}
