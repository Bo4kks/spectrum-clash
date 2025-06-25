using System.Collections.Generic;
using UnityEngine;

public class RedCurrencyMultiplierUpgrade : StatsUpgrade
{
    [SerializeField] private float _redCurrencyMultiplierPerLvl;

    protected override void ApplyUpgrade()
    {
        var floatStats = new Dictionary<FloatStatType, float>
        {
            { FloatStatType.RedCurrencyMultiplier, _redCurrencyMultiplierPerLvl }
        };

        var intStats = new Dictionary<IntStatType, int>();

        var boolStats = new Dictionary<BoolStatType, bool>();

        EventBus.Invoke(new OnStatsUpgradeEvent(floatStats, intStats, boolStats));
    }
}
