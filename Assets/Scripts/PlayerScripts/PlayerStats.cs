using UnityEngine;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    [Header("Float stats")]
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _maxEnergy = 100f;
    [SerializeField] private float _energyRegenPerSecond = 1f;
    [SerializeField] private float _healtRegenPerSecond = 0f;

    [Header("Int stats")]
    [SerializeField] private int _armor = 0;

    [Header("Bool stats")]
    [SerializeField] private bool _isHealthRegenEnabled = false;

    [Header("RedCurrency")]
    [SerializeField] private int _redCurrencyBonus;
    [SerializeField] private float _redCurrencyMultiplier;

    [Header("YellowCurrency")]
    [SerializeField] private int _yellowCurrencyBonus;
    [SerializeField] private float _yellowCurrencyMultiplier;

    [Header("GreenCurrency")]
    [SerializeField] private int _greenCurrencyBonus;
    [SerializeField] private float _greenCurrencyMultiplier;

    [Header("BlueCurrency")]
    [SerializeField] private int _blueCurrencyBonus;
    [SerializeField] private float _blueCurrencyMultiplier;

    [Header("Coins")]
    [SerializeField] private bool _isPlayerBoughtCoinsPerSecondUpgrade;
    [SerializeField] private int _coinsPerSecond;



    private readonly Dictionary<FloatStatType, float> _floatStats = new();
    private readonly Dictionary<IntStatType, int> _intStats = new();
    private readonly Dictionary<BoolStatType, bool> _boolStats = new();

    public float GetFloat(FloatStatType statType) => _floatStats.TryGetValue(statType, out var value) ? value : 0f;
    public int GetInt(IntStatType statType) => _intStats.TryGetValue(statType, out var value) ? value : 0;
    public bool GetBool(BoolStatType statType) => _boolStats.TryGetValue(statType, out var value) && value;

    private void Awake()
    {
        InitializeBaseStats();
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnStatsUpgradeEvent>(ApplyStatsUpgrade);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnStatsUpgradeEvent>(ApplyStatsUpgrade);
    }

    private void InitializeBaseStats()
    {
        _floatStats[FloatStatType.MaxHealth] = _maxHealth;
        _floatStats[FloatStatType.MaxEnergy] = _maxEnergy;
        _floatStats[FloatStatType.EnergyRegenPerSecond] = _energyRegenPerSecond;
        _floatStats[FloatStatType.HealthRegenPerSecond] = _healtRegenPerSecond;
        _floatStats[FloatStatType.RedCurrencyMultiplier] = _redCurrencyMultiplier;
        _floatStats[FloatStatType.YellowCurrencyMultiplier] = _yellowCurrencyMultiplier;
        _floatStats[FloatStatType.GreenCurrencyMultiplier] = _greenCurrencyMultiplier;
        _floatStats[FloatStatType.BlueCurrencyMultiplier] = _blueCurrencyMultiplier;

        _intStats[IntStatType.Armor] = _armor;
        _intStats[IntStatType.RedCurrencyBonus] = _redCurrencyBonus;
        _intStats[IntStatType.YellowCurrencyBonus] = _yellowCurrencyBonus;
        _intStats[IntStatType.GreenCurrencyBonus] = _greenCurrencyBonus;
        _intStats[IntStatType.BlueCurrencyBonus] = _blueCurrencyBonus;
        _intStats[IntStatType.CoinsPerSecond] = _coinsPerSecond;

        _boolStats[BoolStatType.IsHealthRegenEnabled] = _isHealthRegenEnabled;
        _boolStats[BoolStatType.IsPlayerBoughtCoinsPerSecondUpgrade] = _isPlayerBoughtCoinsPerSecondUpgrade;
    }

    private void ApplyStatsUpgrade(OnStatsUpgradeEvent evt)
    {
        foreach (var kv in evt.FloatStats)
        {
            if (_floatStats.ContainsKey(kv.Key))
                _floatStats[kv.Key] += kv.Value;
            else
                _floatStats[kv.Key] = kv.Value;
        }

        foreach (var kv in evt.IntStats)
        {
            if (_intStats.ContainsKey(kv.Key))
                _intStats[kv.Key] += kv.Value;
            else
                _intStats[kv.Key] = kv.Value;
        }

        foreach (var kv in evt.BoolStats)
        {
            _boolStats[kv.Key] = kv.Value;
        }
    }
}
