using UnityEngine;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    [Header("Base stats")]
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _maxEnergy = 100f;
    [SerializeField] private float _energyRegenPerSecond = 1f;
    [SerializeField] private bool _isHealthRegenEnabled = false;
    [SerializeField] private int _armor = 0;

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

        _intStats[IntStatType.Armor] = _armor;

        _boolStats[BoolStatType.IsHealthRegenEnabled] = _isHealthRegenEnabled;
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
