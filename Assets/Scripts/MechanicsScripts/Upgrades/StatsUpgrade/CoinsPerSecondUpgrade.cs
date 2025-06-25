using System.Collections.Generic;
using UnityEngine;

public class CoinsPerSecondUpgrade : StatsUpgrade
{
    [SerializeField] private bool _isPlayerBoughtUpgrade;

    [SerializeField] private int _coinsPerSecond;

    protected override void ApplyUpgrade()
    {
        var floatStats = new Dictionary<FloatStatType, float>();

        var intStats = new Dictionary<IntStatType, int>()
        {
            { IntStatType.CoinsPerSecond, _coinsPerSecond }
        };

        var boolStats = new Dictionary<BoolStatType, bool>()
        {
            { BoolStatType.IsPlayerBoughtCoinsPerSecondUpgrade, _isPlayerBoughtUpgrade }
        };

        EventBus.Invoke(new OnStatsUpgradeEvent(floatStats, intStats, boolStats));
    }
}
