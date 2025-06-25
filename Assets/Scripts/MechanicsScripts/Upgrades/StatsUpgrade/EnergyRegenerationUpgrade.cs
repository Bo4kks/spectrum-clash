using System.Collections.Generic;
using UnityEngine;

public class EnergyRegenerationUpgrade : StatsUpgrade
{
    [SerializeField] private float _energyRegenPerSecond;

    protected override void ApplyUpgrade()
    {
        var floatStats = new Dictionary<FloatStatType, float>
        {
            { FloatStatType.EnergyRegenPerSecond, _energyRegenPerSecond }
        };

        var intStats = new Dictionary<IntStatType, int>();


        var boolStats = new Dictionary<BoolStatType, bool>();

        EventBus.Invoke(new OnStatsUpgradeEvent(floatStats, intStats, boolStats));
    }
}
