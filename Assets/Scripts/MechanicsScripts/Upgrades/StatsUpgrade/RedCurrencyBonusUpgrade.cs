using System.Collections.Generic;
using UnityEngine;

public class RedCurrencyBonusUpgrade : StatsUpgrade
{
    [SerializeField] private int _redCurrencyBonusPerLvl;

    protected override void ApplyUpgrade()
    {
        var floatStats = new Dictionary<FloatStatType, float>();

        var intStats = new Dictionary<IntStatType, int>()
        {
            { IntStatType.RedCurrencyBonus, _redCurrencyBonusPerLvl }
        };

        var boolStats = new Dictionary<BoolStatType, bool>();

        EventBus.Invoke(new OnStatsUpgradeEvent(floatStats, intStats, boolStats));
    }
}
