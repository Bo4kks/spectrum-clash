using UnityEngine;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    private Dictionary<FloatStatType, float> _floatStats = new();
    private Dictionary<IntStatType, int> _intStats = new();
    private Dictionary<BoolStatType, bool> _boolStats = new();

    public float GetFloat(FloatStatType stat) => _floatStats.ContainsKey(stat) ? _floatStats[stat] : 0f;
    public int GetInt(IntStatType stat) => _intStats.ContainsKey(stat) ? _intStats[stat] : 0;
    public bool GetBool(BoolStatType stat) => _boolStats.ContainsKey(stat) && _boolStats[stat];

    private void Awake()
    {
        _floatStats[FloatStatType.MaxHealth] = 100f;
        _floatStats[FloatStatType.MaxEnergy] = 100f;
        _floatStats[FloatStatType.EnergyRegenPerSecond] = 1f;
        _boolStats[BoolStatType.IsHealthRegenEnabled] = false;
        _intStats[IntStatType.Armor] = 0;
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnStatsUpgradeEvent>(ApplyStatsUpgrade);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnStatsUpgradeEvent>(ApplyStatsUpgrade);
    }

    private void ApplyStatsUpgrade(OnStatsUpgradeEvent evt)
    {
        foreach (var kv in evt.FloatStats)
            _floatStats[kv.Key] = GetFloat(kv.Key) + kv.Value;

        foreach (var kv in evt.IntStats)
            _intStats[kv.Key] = GetInt(kv.Key) + kv.Value;

        foreach (var kv in evt.BoolStats)
            _boolStats[kv.Key] = kv.Value;
    }
}
