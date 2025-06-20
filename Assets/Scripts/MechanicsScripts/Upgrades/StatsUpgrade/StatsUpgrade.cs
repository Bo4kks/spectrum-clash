using UnityEngine;

public abstract class StatsUpgrade : MonoBehaviour, IStatsUpgrade
{
    [Header("Upgrade Settings")]
    [SerializeField] private int _maxLevel = 10;
    [SerializeField] private int _basePrice = 10;
    [SerializeField] private int _currentLevel = 0;
    [SerializeField] private CurrencyTypes _currencyType;

    private PlayerCurrency _playerCurrency;

    public int CurrentLevel
    {
        get { return _currentLevel; }
    }

    public int MaxLevel
    {
        get { return _maxLevel; }
    }

    public int CurrentPrice
    {
        get { return _basePrice * (_currentLevel + 1); }
    }

    public CurrencyTypes CurrencyType
    {
        get { return _currencyType; }
    }

    public bool IsMaxed
    {
        get { return _currentLevel >= _maxLevel; }
    }

    protected virtual void Awake()
    {
        _playerCurrency = FindFirstObjectByType<PlayerCurrency>();
    }

    public bool TryPurchase()
    {
        if (IsMaxed)
        {
            Debug.Log("Upgrade already at max level.");
            return false;
        }

        int price = CurrentPrice;

        if (!_playerCurrency.TrySpend(_currencyType, price))
        {
            Debug.Log("Not enough currency to purchase upgrade.");
            return false;
        }

        _currentLevel++;
        ApplyUpgrade();

        return true;
    }

    protected abstract void ApplyUpgrade();
}
