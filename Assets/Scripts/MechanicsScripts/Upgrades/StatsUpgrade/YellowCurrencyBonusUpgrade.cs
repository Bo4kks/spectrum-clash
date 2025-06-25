using System.Collections.Generic;
using UnityEngine;

public class YellowCurrencyBonusUpgrade : StatsUpgrade
{
    [SerializeField] private int _yellowCurrencyBonusPerLvl;

    protected override void ApplyUpgrade()
    {
        var floatStats = new Dictionary<FloatStatType, float>();

        var intStats = new Dictionary<IntStatType, int>()
        {
            { IntStatType.YellowCurrencyBonus, _yellowCurrencyBonusPerLvl }
        };

        var boolStats = new Dictionary<BoolStatType, bool>();

        EventBus.Invoke(new OnStatsUpgradeEvent(floatStats, intStats, boolStats));
    }
}
