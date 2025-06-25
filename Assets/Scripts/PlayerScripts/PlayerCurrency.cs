using System.Linq.Expressions;
using UnityEngine;

public class PlayerCurrency : MonoBehaviour, IEventListener, IEventPusher
{
    [SerializeField] private int _redCurrency;
    [SerializeField] private int _yellowCurrency;
    [SerializeField] private int _greenCurrency;
    [SerializeField] private int _blueCurrency;

    private PlayerStats _playerStats;
    [SerializeField] private float _timer;

    private bool _isGameStarted;

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

    private void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (_playerStats.GetBool(BoolStatType.IsPlayerBoughtCoinsPerSecondUpgrade) && _isGameStarted)
        {
            GivePlayerCurrencyPerSecond();
        }
    }

    public void OnEnable()
    {
        EventBus.Subscribe<OnPlayerEarnedCurrencyEvent>(AddCurrency);
        EventBus.Subscribe<OnGameRestartEvent>(SetIsGameStartedValue);
        EventBus.Subscribe<OnGameStartedEvent>(SetIsGameStartedValue);
        EventBus.Subscribe<OnGameOverEvent>(SetIsGameStartedValue);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnPlayerEarnedCurrencyEvent>(AddCurrency);
        EventBus.Unsubscribe<OnGameRestartEvent>(SetIsGameStartedValue);
        EventBus.Unsubscribe<OnGameStartedEvent>(SetIsGameStartedValue);
        EventBus.Unsubscribe<OnGameOverEvent>(SetIsGameStartedValue);
    }

    private void AddCurrency(OnPlayerEarnedCurrencyEvent @event)
    {
        switch (@event.CurrencyType)
        {
            case CurrencyTypes.RedCurrency:
                RedCurrency += GetFinalCurrencyValue(FloatStatType.RedCurrencyMultiplier, IntStatType.RedCurrencyBonus);
                break;
            case CurrencyTypes.YellowCurrency:
                YellowCurrency += GetFinalCurrencyValue(FloatStatType.YellowCurrencyMultiplier, IntStatType.YellowCurrencyBonus);
                break;
            case CurrencyTypes.GreenCurrency:
                GreenCurrency += GetFinalCurrencyValue(FloatStatType.GreenCurrencyMultiplier, IntStatType.GreenCurrencyBonus);
                break;
            case CurrencyTypes.BlueCurrency:
                BlueCurrency += GetFinalCurrencyValue(FloatStatType.BlueCurrencyMultiplier, IntStatType.BlueCurrencyBonus);
                break;
        }
    }

    private int GetFinalCurrencyValue(FloatStatType currencyMultiplier, IntStatType currencyBonus)
    {
        float finalCurrency = (1f + _playerStats.GetInt(currencyBonus)) * _playerStats.GetFloat(currencyMultiplier);
        return Mathf.RoundToInt(finalCurrency);
    }

    private int GetFinalCurrencyValue(FloatStatType currencyMultiplier, IntStatType currencyBonus, IntStatType coinsPerSecond)
    {
        float finalCurrency = (1f + _playerStats.GetInt(IntStatType.CoinsPerSecond) + _playerStats.GetInt(currencyBonus)) * _playerStats.GetFloat(currencyMultiplier);
        return Mathf.RoundToInt(finalCurrency);
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

    private void GivePlayerCurrencyPerSecond()
    {
        _timer += Time.deltaTime;

        if (_timer >= 1)
        {
            RedCurrency += GetFinalCurrencyValue(FloatStatType.RedCurrencyMultiplier, IntStatType.RedCurrencyBonus, IntStatType.CoinsPerSecond);
            YellowCurrency += GetFinalCurrencyValue(FloatStatType.YellowCurrencyMultiplier, IntStatType.YellowCurrencyBonus, IntStatType.CoinsPerSecond);
            GreenCurrency += GetFinalCurrencyValue(FloatStatType.GreenCurrencyMultiplier, IntStatType.GreenCurrencyBonus, IntStatType.CoinsPerSecond);
            BlueCurrency += GetFinalCurrencyValue(FloatStatType.BlueCurrencyMultiplier, IntStatType.BlueCurrencyBonus, IntStatType.CoinsPerSecond);

            _timer = 0;
        }
    }

    private void SetIsGameStartedValue(OnGameRestartEvent @event) => _isGameStarted = true;

    private void SetIsGameStartedValue(OnGameStartedEvent @event) => _isGameStarted = true;

    private void SetIsGameStartedValue(OnGameOverEvent @event) => _isGameStarted = false;
}
    
