    using UnityEngine;

public class PlayerCurrency : MonoBehaviour, IEventListener, IEventPusher
{
    [SerializeField] private int _redCurrency;
    [SerializeField] private int _yellowCurrency;
    [SerializeField] private int _greenCurrency;
    [SerializeField] private int _blueCurrency;

    public int RedCurrency { get { return _redCurrency; } }

    public int YellowCurrency { get { return _yellowCurrency; } }

    public int GreenCurrency { get { return _greenCurrency; } }

    public int BlueCurrency { get { return _blueCurrency; } }

    public void OnEnable()
    {
        EventBus.Subscribe<OnPlayerEarnedCurrencyEvent>(AddCurrency);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnPlayerEarnedCurrencyEvent>(AddCurrency);
    }

    private void AddCurrency(OnPlayerEarnedCurrencyEvent @event)
    {
        switch (@event.CurrencyType)
        {
            case CurrencyTypes.RedCurrency: _redCurrency++; break;
            case CurrencyTypes.YellowCurrency: _yellowCurrency++; break;
            case CurrencyTypes.GreenCurrency: _greenCurrency++; break;
            case CurrencyTypes.BlueCurrency: _blueCurrency++; break;
            default: Debug.LogWarning("Unknown currency type"); break;
        }
    }

    public bool TrySpend(CurrencyTypes type, int amount)
    {
        switch (type)
        {
            case CurrencyTypes.RedCurrency:
                if (RedCurrency >= amount) { _redCurrency -= amount; return true; }
                break;
            case CurrencyTypes.YellowCurrency:
                if (YellowCurrency >= amount) { _yellowCurrency -= amount; return true; }
                break;
            case CurrencyTypes.GreenCurrency:
                if (GreenCurrency >= amount) { _greenCurrency -= amount; return true; }
                break;
            case CurrencyTypes.BlueCurrency:
                if (BlueCurrency >= amount) { _blueCurrency -= amount; return true; }
                break;
        }
        return false;
    }
}
    
