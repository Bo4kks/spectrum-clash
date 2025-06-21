using System.Collections.Generic;
using UnityEngine;

public class HealthRegenUpgrade : StatsUpgrade
{
    [SerializeField] private bool _isHealthRegenActive = true;
    [SerializeField] private float _heatlhRegenPerSecond;

    protected override void ApplyUpgrade()
    {
        var boolStats = new Dictionary<BoolStatType, bool>
        {
            { BoolStatType.IsHealthRegenEnabled, _isHealthRegenActive },
        };

        var floatStats = new Dictionary<FloatStatType, float>
        {
            { FloatStatType.HealthRegenPerSecond, _heatlhRegenPerSecond }
        };

        var intStats = new Dictionary<IntStatType, int>();

        EventBus.Invoke(new OnStatsUpgradeEvent(floatStats, intStats, boolStats));
    }
}
