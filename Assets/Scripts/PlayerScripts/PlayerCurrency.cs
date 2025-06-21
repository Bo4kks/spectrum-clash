    using UnityEngine;

public class PlayerCurrency : MonoBehaviour, IEventListener, IEventPusher
{
    [SerializeField] private int _redCurrency;
    [SerializeField] private int _yellowCurrency;
    [SerializeField] private int _greenCurrency;
    [SerializeField] private int _blueCurrency;

    public int RedCurrency
    {
        get
        {
            return _redCurrency;
        }
        private set
        {
            _redCurrency = value;
            EventBus.Invoke(new OnRedCurrencyValueChanged(RedCurrency));
        }
    }

    public int YellowCurrency
    {
        get
        {
            return _yellowCurrency;
        }
        private set
        {
            _yellowCurrency = value;
            EventBus.Invoke(new OnYellowCurrencyValueChanged(YellowCurrency));
        }
    }

    public int GreenCurrency
    {
        get
        {
            return _greenCurrency;
        }
        private set
        {
            _greenCurrency = value;
            EventBus.Invoke(new OnGreenCurrencyValueChanged(GreenCurrency));
        }
    }

    public int BlueCurrency
    {
        get
        {
            return _blueCurrency;
        }
        private set
        {
            _blueCurrency = value;
            EventBus.Invoke(new OnBlueCurrencyValueChanged(BlueCurrency));
        }
    }

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
            case CurrencyTypes.RedCurrency: RedCurrency++; break;
            case CurrencyTypes.YellowCurrency: YellowCurrency++; break;
            case CurrencyTypes.GreenCurrency: GreenCurrency++; break;
            case CurrencyTypes.BlueCurrency: BlueCurrency++; break;
            default: Debug.LogWarning("Unknown currency type"); break;
        }
    }

    public bool TrySpend(CurrencyTypes type, int amount)
    {
        switch (type)
        {
            case CurrencyTypes.RedCurrency:
                if (RedCurrency >= amount) { RedCurrency -= amount; return true; }
                break;
            case CurrencyTypes.YellowCurrency:
                if (YellowCurrency >= amount) { YellowCurrency -= amount; return true; }
                break;
            case CurrencyTypes.GreenCurrency:
                if (GreenCurrency >= amount) { GreenCurrency -= amount; return true; }
                break;
            case CurrencyTypes.BlueCurrency:
                if (BlueCurrency >= amount) { BlueCurrency -= amount; return true; }
                break;
        }
        return false;
    }
}
    
