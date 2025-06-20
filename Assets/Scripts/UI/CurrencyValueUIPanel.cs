using TMPro;
using UnityEngine;

public class CurrencyValueUIPanel : MonoBehaviour, IEventListener
{
    [SerializeField] private TextMeshProUGUI _redCurrencyValue;
    [SerializeField] private TextMeshProUGUI _yellowCurrencyValue;
    [SerializeField] private TextMeshProUGUI _greenCurrencyValue;
    [SerializeField] private TextMeshProUGUI _blueCurrencyValue;

    private PlayerCurrency _playerCurrency;

    private void Awake()
    {
        _playerCurrency = FindFirstObjectByType<PlayerCurrency>();
    }

    public void OnEnable()
    {
        EventBus.Subscribe<OnPlayerEarnedCurrencyEvent>(UpdateCurrencyValue);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnPlayerEarnedCurrencyEvent>(UpdateCurrencyValue);
    }

    public void UpdateCurrencyValue(OnPlayerEarnedCurrencyEvent @event)
    {
        _redCurrencyValue.text = _playerCurrency.RedCurrency.ToString();
        _yellowCurrencyValue.text = _playerCurrency.YellowCurrency.ToString();
        _greenCurrencyValue.text = _playerCurrency.GreenCurrency.ToString();
        _blueCurrencyValue.text = _playerCurrency.BlueCurrency.ToString();
    }

    public void UpdateCurrencyValue()
    {
        _redCurrencyValue.text = _playerCurrency.RedCurrency.ToString();
        _yellowCurrencyValue.text = _playerCurrency.YellowCurrency.ToString();
        _greenCurrencyValue.text = _playerCurrency.GreenCurrency.ToString();
        _blueCurrencyValue.text = _playerCurrency.BlueCurrency.ToString();
    }
}
