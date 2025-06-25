using System.Collections.Generic;
using UnityEngine;

public class YellowCurrencyMultiplierUpgrade : StatsUpgrade
{
    [SerializeField] private float _yellowCurrencyMultiplierUpgrade;

    protected override void ApplyUpgrade()
    {
        var floatStats = new Dictionary<FloatStatType, float>
        {
            { FloatStatType.YellowCurrencyMultiplier, _yellowCurrencyMultiplierUpgrade }
        };

        var intStats = new Dictionary<IntStatType, int>();

        var boolStats = new Dictionary<BoolStatType, bool>();

        EventBus.Invoke(new OnStatsUpgradeEvent(floatStats, intStats, boolStats));
    }
}
